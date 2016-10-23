
#define ENABLE_DEBUG_INPUT

#if DEBUG
#else
#undef ENABLE_DEBUG_INPUT
#endif

using UnityEngine;
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
    private bool m_isSubmitedUser1 = false;
    private bool m_isSubmitedUser2 = false;
    private bool m_isSubmitedUser3 = false;
    private bool m_isSubmitedUser4 = false;

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
                        UnityEngine.SceneManagement.SceneManager.LoadScene("TestGameMain");
                    }
                }
                break;
        }
    }

    private void InitializeAssignController()
    {
        m_isSubmitedUser1 = false;
        m_isSubmitedUser2 = false;
        m_isSubmitedUser3 = false;
        m_isSubmitedUser4 = false;
        m_nextUserId = 0;
    }

    private void AssignController()
    {
        if (IsInputEvent(XBKeyCode.UserCode.User1))
        {
            if (!m_isSubmitedUser1)
            {
                m_isSubmitedUser1 = true;
                XVInput.CreateXBoxInterface((UserID)m_nextUserId, XBKeyCode.UserCode.User1);
                EnableUser(m_nextUserId);
                ++m_nextUserId;
            }
        }
        if (IsInputEvent(XBKeyCode.UserCode.User2))
        {
            if (!m_isSubmitedUser2)
            {
                m_isSubmitedUser2 = true;
                XVInput.CreateXBoxInterface((UserID)m_nextUserId, XBKeyCode.UserCode.User2);
                EnableUser(m_nextUserId);
                ++m_nextUserId;
            }
        }
        if (IsInputEvent(XBKeyCode.UserCode.User3))
        {
            if (!m_isSubmitedUser3)
            {
                m_isSubmitedUser3 = true;
                XVInput.CreateXBoxInterface((UserID)m_nextUserId, XBKeyCode.UserCode.User3);
                EnableUser(m_nextUserId);
                ++m_nextUserId;
            }
        }
        if (IsInputEvent(XBKeyCode.UserCode.User4))
        {
            if (!m_isSubmitedUser4)
            {
                m_isSubmitedUser4 = true;
                XVInput.CreateXBoxInterface((UserID)m_nextUserId, XBKeyCode.UserCode.User4);
                EnableUser(m_nextUserId);
                ++m_nextUserId;
            }
        }
    }

    private bool IsAllAssignController()
    {
        return (m_nextUserId > 3);
    }

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
