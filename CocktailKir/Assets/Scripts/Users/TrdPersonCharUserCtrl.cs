using System;
using UnityEngine;

[RequireComponent(typeof(TrdPersonCharCtrl))]
public class TrdPersonCharUserCtrl : MonoBehaviour
{
    private TrdPersonCharCtrl m_charCtrl = null;
    private Transform m_camera = null;
    private bool m_isJump = false;

    void Start()
    {
        m_camera = Camera.main.transform;
        m_charCtrl = this.GetComponent<TrdPersonCharCtrl>();
    }

    void Update()
    {
        if (!m_isJump)
        {
            m_isJump = Input.GetKeyDown(KeyCode.Space);
        }
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        Vector3 camForward = Vector3.zero;

        // Input
        float h = GetInputStateH();
        float v = GetInputStateV();
        
        // MoveCamera
        camForward = Vector3.Scale(m_camera.forward, new Vector3(1, 0, 1)).normalized;
        move = v * camForward + h * m_camera.right;
        
        // Walk
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            move *= 0.5f; 
        }

        m_charCtrl.Move(move, m_isJump);
        m_isJump = false;
    }

    private float GetInputStateV()
    {
        return ((Input.GetKey(KeyCode.W) ? 1.0f : 0.0f) + (Input.GetKey(KeyCode.S) ? -1.0f : 0.0f));
    }

    private float GetInputStateH()
    {
        return ((Input.GetKey(KeyCode.D) ? 1.0f : 0.0f) + (Input.GetKey(KeyCode.A) ? -1.0f : 0.0f));
    }
}

