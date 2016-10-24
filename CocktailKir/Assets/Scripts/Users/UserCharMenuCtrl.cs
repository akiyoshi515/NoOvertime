﻿using System;
using UnityEngine;

using AkiVACO;

[RequireComponent(typeof(UserData))]
[RequireComponent(typeof(CharAnimateMenuCtrl))]
public class UserCharMenuCtrl : MonoBehaviour, AkiVACO.IXObjLabelEx
{
    [SerializeField]
    private Camera m_camera = null;

    [SerializeField]
    private float m_faceNormalTime = 1.0f;

    private CharAnimateMenuCtrl m_charCtrl = null;
    private UserData m_userdata = null;
    private bool m_isJump = false;
    private bool m_isWalk = false;

    void Start()
    {
        XLogger.LogValidObject(m_camera == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Camera"), gameObject);

        m_charCtrl = this.GetComponent<CharAnimateMenuCtrl>();
        m_userdata = this.GetComponent<UserData>();
    }

    void Update()
    {
        if (m_faceNormalTime > 0.0f)
        {
            m_faceNormalTime -= Time.deltaTime;
            if (m_faceNormalTime <= 0.0f)
            {
                this.transform.GetChild(0).GetComponent<UnityChan.FaceUpdate>().isKeepFace = false;
            }
            return;
        }

        if (!m_isJump)
        {
//            m_isJump = m_userdata.input.IsJump();
        }
        m_isWalk = m_userdata.input.IsWalk();
    }

    void FixedUpdate()
    {
        if (m_faceNormalTime > 0.0f)
        {
            return;
        }

        Vector3 move = Vector3.zero;
        Vector3 camForward = Vector3.zero;

        // Input
        Vector2 vec = m_userdata.input.Move();
        
        // MoveCamera
        camForward = Vector3.Scale(m_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        move = vec.y * camForward + vec.x * m_camera.transform.right;
        
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
            m_userdata.input.IsLauncherStance() ? 
            ":Ready" : (m_isWalk ? ":Walking" : "None"));
    }
}
