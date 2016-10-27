using System;
using UnityEngine;

using AkiVACO;

[RequireComponent(typeof(UserData))]
[RequireComponent(typeof(CharAnimateCtrl))]
public class UserCharCtrl : MonoBehaviour, AkiVACO.IXObjLabelEx
{
    [SerializeField]
    private Camera m_camera = null;

    private CharAnimateCtrl m_charCtrl = null;
    private UserData m_userdata = null;
    private bool m_isJump = false;
    private bool m_isLauncherStance = false;

    public bool isLauncherStance 
    {
        get { return m_isLauncherStance; }
    }

    public bool isJumping
    {
        get { return m_charCtrl.isJumping; }
    }

    public UserID userID
    {
        get { return m_userdata.userID; }
    }

    void Start()
    {
        XLogger.LogValidObject(m_camera == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Camera"), gameObject);

        m_charCtrl = this.GetComponent<CharAnimateCtrl>();
        m_userdata = this.GetComponent<UserData>();
    }

    void Update()
    {
        if (!m_isJump)
        {
            if (!m_charCtrl.isJumping)
            {
                if (m_userdata.input.IsLauncherStance())
                {
                    m_isLauncherStance = true;
                }
                else
                {
                    m_isLauncherStance = false;
                    m_isJump = m_userdata.input.IsJump();
                }
            }
            else
            {
                m_isLauncherStance = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        Vector3 camForward = Vector3.zero;

        // Input
        Vector2 vec = Vector2.zero;
        if (!isLauncherStance)
        {
            vec = m_userdata.input.Move();
        }

        // MoveCamera
        camForward = Vector3.Scale(m_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        move = vec.y * camForward + vec.x * m_camera.transform.right;
        
        m_charCtrl.Move(move, m_isJump);
        m_isJump = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ballet")
        {
            BalletCtrl ctrl = col.gameObject.GetComponent<BalletCtrl>();
            if (ctrl.userID != m_userdata.userID)
            {
                ctrl.SendHit();
            }
        }
    }

    public void Edit_SetCamera(Camera camera)
    {
#if UNITY_EDITOR
        m_camera = camera;
#endif
    }

    public string GetLabelString()
    {
        return "Sts " + (
            m_userdata.input.IsLauncherStance() ? 
            ":Ready" : "None");
    }
}

