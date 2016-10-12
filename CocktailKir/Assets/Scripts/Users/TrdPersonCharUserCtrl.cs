using System;
using UnityEngine;

[RequireComponent(typeof(TrdPersonCharCtrl))]
public class TrdPersonCharUserCtrl : MonoBehaviour, AkiVACO.IXObjLabelEx
{
    private TrdPersonCharCtrl m_charCtrl = null;
    private Transform m_camera = null;
    private bool m_isJump = false;
    private bool m_isWalk = false;

    void Start()
    {
        m_camera = Camera.main.transform;
        m_charCtrl = this.GetComponent<TrdPersonCharCtrl>();
    }

    void Update()
    {
        if (!m_isJump)
        {
            m_isJump = XVInput.GetInterface(UserID.User1).IsJump();
        }
        m_isWalk = XVInput.GetInterface(UserID.User1).IsWalk();
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        Vector3 camForward = Vector3.zero;

        // Input
        Vector2 vec = GetInputState();
        
        // MoveCamera
        camForward = Vector3.Scale(m_camera.forward, new Vector3(1, 0, 1)).normalized;
        move = vec.y * camForward + vec.x * m_camera.right;
        
        // Walk
        if (m_isWalk) 
        {
            move *= 0.5f; 
        }

        m_charCtrl.Move(move, m_isJump);
        m_isJump = false;
    }

    private Vector2 GetInputState()
    {
        return XVInput.GetInterface(UserID.User1).Move();
    }

    public string GetLabelString()
    {
        return "Sts " + (
            XVInput.GetInterface(UserID.User1).IsLauncherStance() ? 
            ":Ready" : (m_isWalk ? ":Walking" : "None"));
    }
}

