
#define ENABLE_DEBUG_INPUT

#if DEBUG
#else
#undef ENABLE_DEBUG_INPUT
#endif

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

using AkiVACO;
using XBoxInput;

public class UserEntrySceneCtrl : MonoBehaviour
{
    private enum State
    {
        StartWait,
        AssignWait,
        SubmitWait,
    }

    [SerializeField]
    private XBKeyCode.Button m_submitKey = XBKeyCode.Button.A;

    [SerializeField]
    private float m_startWaitTime = 1.0f;

    [SerializeField]
    private float m_submitWaitTime = 1.0f;

    [SerializeField]
    private UserEntryBkScreenCtrl[] m_bkScreenCtrl = null;

    [SerializeField]
    private GameObject[] m_mesh = null;

    private State m_sts = State.StartWait;
    private float m_time = 0.0f;
    private int m_nextUserId = 0;
    private bool[] m_isSubmitedUser = new bool[4] { false, false, false, false };

    // Use this for initialization
    void Start()
    {
        XVInput.ClearInterface();
        m_nextUserId = 0;
        m_time = m_startWaitTime;
        EnableMeshAnimation(0, false);
        EnableMeshAnimation(1, false);
        EnableMeshAnimation(2, false);
        EnableMeshAnimation(3, false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_sts)
        {
            case State.StartWait:
                m_time -= Time.deltaTime;
                if (m_time <= 0.0f)
                {
                    m_time = 0.0f;
                    InitializeAssignController();
                    m_sts = State.AssignWait;
                }
                break;
            case State.AssignWait:
                AssignController();
                if (IsAllAssignController())
                {
                    m_time = m_submitWaitTime;
                    // TODO SubmitWaitEvent
                    m_sts = State.SubmitWait;
                }
                break;
            case State.SubmitWait:
                m_time -= Time.deltaTime;
                if (m_time <= 0.0f)
                {
                    m_time = 0.0f;
                    if (XBGamePad.IsTriggered(XBKeyCode.Button.Start, XBKeyCode.UserCode.Any)
                        || Input.GetKeyDown(KeyCode.Return))
                    {
                        XLogger.Log("GoTo NextScene");
                        UnityEngine.SceneManagement.SceneManager.LoadScene("AlphaGameMain"); // HACK
                    }
                }
                break;
        }
    }

    private void InitializeAssignController()
    {
        m_isSubmitedUser[0] = false;
        m_isSubmitedUser[1] = false;
        m_isSubmitedUser[2] = false;
        m_isSubmitedUser[3] = false;
        m_nextUserId = 0;
    }

    /// <summary>
    /// コントローラのAssign
    /// </summary>
    private void AssignController()
    {
        UnityAction<XBKeyCode.UserCode, UserID> act = (code, id) =>
        {
            if (IsInputEvent(code))
            {
                int idx = (int)id;
                if (!(m_isSubmitedUser[idx]))
                {
                    m_isSubmitedUser[idx] = true;
                    XVInput.CreateXBoxInterface((UserID)m_nextUserId, code);
                    EnableUser(m_nextUserId);
                    ++m_nextUserId;
                }
            }
        };

        act.Invoke(XBKeyCode.UserCode.User1, UserID.User1);
        act.Invoke(XBKeyCode.UserCode.User2, UserID.User2);
        act.Invoke(XBKeyCode.UserCode.User3, UserID.User3);
        act.Invoke(XBKeyCode.UserCode.User4, UserID.User4);
    }

    /// <summary>
    /// 全てのコントローラがassignされたか？
    /// </summary>
    private bool IsAllAssignController()
    {
        return (m_nextUserId > 3);
    }

    /// <summary>
    /// ユーザーの表示
    /// </summary>
    /// <param name="userId">ユーザーID</param>
    private void EnableUser(int userId)
    {
        m_bkScreenCtrl[userId].Open();
        EnableMeshAnimation(userId, true);
    }

    private void EnableMeshAnimation(int userId, bool bl)
    {
        Animator anim = m_mesh[userId].GetComponent<Animator>();
        anim.enabled = bl;
        anim.SetTrigger("Starting");
        UnityChan.FaceUpdate faceUpdate = m_mesh[userId].GetComponent<UnityChan.FaceUpdate>();
        faceUpdate.enabled = bl;
        m_mesh[userId].GetComponent<UnityChan.AutoBlinkforSD>().enabled = bl;

        m_mesh[userId].transform.parent.GetComponent<UserCharMenuCtrl>().enabled = bl;
    }

    /// <summary>
    /// Inputのラッパー
    /// </summary>
    private bool IsInputEvent(XBKeyCode.UserCode code)
    {
#if ENABLE_DEBUG_INPUT
        switch (code)
        {
            case XBKeyCode.UserCode.User1:
                return Input.GetKeyDown(KeyCode.Alpha1) || XBGamePad.IsTriggered(m_submitKey, code);
            case XBKeyCode.UserCode.User2:
                return Input.GetKeyDown(KeyCode.Alpha2) || XBGamePad.IsTriggered(m_submitKey, code);
            case XBKeyCode.UserCode.User3:
                return Input.GetKeyDown(KeyCode.Alpha3) || XBGamePad.IsTriggered(m_submitKey, code);
            case XBKeyCode.UserCode.User4:
                return Input.GetKeyDown(KeyCode.Alpha4) || XBGamePad.IsTriggered(m_submitKey, code);
        }
        return false;
#else
        return XBGamePad.IsTriggered(m_submitKey, code);
#endif
    }
}
