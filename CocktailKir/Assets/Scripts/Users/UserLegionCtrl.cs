﻿using UnityEngine;
using System.Collections;

// TODO
public class UserLegionCtrl : MonoBehaviour
{
    [SerializeField]
    private UIBillboard m_uiUserNo1 = null;
    [SerializeField]
    private UIBillboard m_uiUserNo2 = null;
    [SerializeField]
    private UIBillboard m_uiUserNo3 = null;
    [SerializeField]
    private UIBillboard m_uiUserNo4 = null;

    [SerializeField]
    private UIBillboard m_uiMagazine1 = null;
    [SerializeField]
    private UIBillboard m_uiMagazine2 = null;
    [SerializeField]
    private UIBillboard m_uiMagazine3 = null;
    [SerializeField]
    private UIBillboard m_uiMagazine4 = null;

    public void SetEnables(UserID myid, Camera target)
    {
        switch (myid)
        {
            case UserID.User1:
                SetUser1(target);
                break;
            case UserID.User2:
                SetUser2(target);
                break;
            case UserID.User3:
                SetUser3(target);
                break;
            case UserID.User4:
                SetUser4(target);
                break;
        }
    }

    // User1用の表示非表示設定
    private void SetUser1(Camera target)
    {
        m_uiUserNo1.EnableView(false);
        m_uiMagazine1.EnableView(true);

        m_uiUserNo2.EnableView(true);
        m_uiUserNo2.TargetCamera = target;
        m_uiMagazine2.EnableView(false);

        m_uiUserNo3.EnableView(true);
        m_uiUserNo3.TargetCamera = target;
        m_uiMagazine3.EnableView(false);

        m_uiUserNo4.EnableView(true);
        m_uiUserNo4.TargetCamera = target;
        m_uiMagazine4.EnableView(false);
    }

    // User2用の表示非表示設定
    private void SetUser2(Camera target)
    {
        m_uiUserNo2.EnableView(false);
        m_uiMagazine2.EnableView(true);

        m_uiUserNo1.EnableView(true);
        m_uiUserNo1.TargetCamera = target;
        m_uiMagazine1.EnableView(false);

        m_uiUserNo3.EnableView(true);
        m_uiUserNo3.TargetCamera = target;
        m_uiMagazine3.EnableView(false);

        m_uiUserNo4.EnableView(true);
        m_uiUserNo4.TargetCamera = target;
        m_uiMagazine4.EnableView(false);
    }

    // User3用の表示非表示設定
    private void SetUser3(Camera target)
    {
        m_uiUserNo3.EnableView(false);
        m_uiMagazine3.EnableView(true);

        m_uiUserNo2.EnableView(true);
        m_uiUserNo2.TargetCamera = target;
        m_uiMagazine2.EnableView(false);

        m_uiUserNo1.EnableView(true);
        m_uiUserNo1.TargetCamera = target;
        m_uiMagazine1.EnableView(false);

        m_uiUserNo4.EnableView(true);
        m_uiUserNo4.TargetCamera = target;
        m_uiMagazine4.EnableView(false);
    }

    // User4用の表示非表示設定
    private void SetUser4(Camera target)
    {
        m_uiUserNo4.EnableView(false);
        m_uiMagazine4.EnableView(true);

        m_uiUserNo2.EnableView(true);
        m_uiUserNo2.TargetCamera = target;
        m_uiMagazine2.EnableView(false);

        m_uiUserNo3.EnableView(true);
        m_uiUserNo3.TargetCamera = target;
        m_uiMagazine3.EnableView(false);

        m_uiUserNo1.EnableView(true);
        m_uiUserNo1.TargetCamera = target;
        m_uiMagazine1.EnableView(false);
    }
    
}
