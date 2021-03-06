﻿using UnityEngine;
using System.Collections;

using AkiVACO;

// TODO
public class UserCameraViewCtrl : MonoBehaviour
{
    [SerializeField]
    private UserCameraAutoCtrl m_ctrl = null;

    private UserLegionCtrl m_legionCtrl = null;
    private UserID m_userID = UserID.User1;

    void Start()
    {
        m_legionCtrl = m_ctrl.GetComponentInParent<UserLegionCtrl>();
        XLogger.LogValidObject(m_legionCtrl == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("UserLegionCtrl"), gameObject);
    
        m_userID = m_ctrl.targetUser.GetComponent<UserData>().userID;
    }

    void OnPreCull()
    {
        // TODO
        m_legionCtrl.SetEnables(m_userID, this.GetComponent<Camera>());
    }
    

}