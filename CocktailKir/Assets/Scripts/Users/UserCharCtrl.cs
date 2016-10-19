using System;
using UnityEngine;

[RequireComponent(typeof(UserData))]
[RequireComponent(typeof(CharAnimateCtrl))]
public class UserCharCtrl : MonoBehaviour, AkiVACO.IXObjLabelEx
{
    private CharAnimateCtrl m_charCtrl = null;
    private UserData m_userdata = null;
    private Transform m_camera = null;
    private bool m_isJump = false;
    private bool m_isWalk = false;

    void Start()
    {
        m_camera = Camera.main.transform;
        m_charCtrl = this.GetComponent<CharAnimateCtrl>();
        m_userdata = this.GetComponent<UserData>();
    }

    void Update()
    {
        if (!m_isJump)
        {
            m_isJump = m_userdata.input.IsJump();
        }
        m_isWalk = m_userdata.input.IsWalk();
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        Vector3 camForward = Vector3.zero;

        // Input
        Vector2 vec = m_userdata.input.Move();
        
        // MoveCamera
        camForward = Vector3.Scale(m_camera.forward, new Vector3(1, 0, 1)).normalized;
        move = vec.y * camForward + vec.x * m_camera.right;
        
        // Walk
        if (m_isWalk) 
        {
            move *= 0.5f; 
        }

        m_charCtrl.Move(move, m_isJump, m_isWalk);
        m_isJump = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ballet")
        {
            TestBalletCtrl ctrl = col.gameObject.GetComponent<TestBalletCtrl>();
            if (ctrl.userID != m_userdata.userID)
            {
                ctrl.SendHit();
            }
        }
    }

    public string GetLabelString()
    {
        return "Sts " + (
            m_userdata.input.IsLauncherStance() ? 
            ":Ready" : (m_isWalk ? ":Walking" : "None"));
    }
}

