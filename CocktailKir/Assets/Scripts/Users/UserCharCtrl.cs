using System;
using UnityEngine;

[RequireComponent(typeof(UserData))]
[RequireComponent(typeof(CharAnimateCtrl))]
public class UserCharCtrl : MonoBehaviour, AkiVACO.IXObjLabelEx
{
    private CharAnimateCtrl m_charCtrl = null;
    private IXVInput m_input = null;
    private Transform m_camera = null;
    private bool m_isJump = false;
    private bool m_isWalk = false;

    void Start()
    {
        m_camera = Camera.main.transform;
        m_charCtrl = this.GetComponent<CharAnimateCtrl>();
        m_input = this.GetComponent<UserData>().input;
    }

    void Update()
    {
        if (!m_isJump)
        {
            m_isJump = m_input.IsJump();
        }
        m_isWalk = m_input.IsWalk();
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        Vector3 camForward = Vector3.zero;

        // Input
        Vector2 vec = m_input.Move();
        
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

    public string GetLabelString()
    {
        return "Sts " + (
            m_input.IsLauncherStance() ? 
            ":Ready" : (m_isWalk ? ":Walking" : "None"));
    }
}

