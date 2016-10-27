using UnityEngine;
using System.Collections;

public class UserLegionCtrl : MonoBehaviour
{
    [SerializeField]
    private ISwitchViewCtrl m_uiUserNo1 = null;
    [SerializeField]
    private ISwitchViewCtrl m_uiUserNo2 = null;
    [SerializeField]
    private ISwitchViewCtrl m_uiUserNo3 = null;
    [SerializeField]
    private ISwitchViewCtrl m_uiUserNo4 = null;

    [SerializeField]
    private ISwitchViewCtrl m_uiMagazine1 = null;
    [SerializeField]
    private ISwitchViewCtrl m_uiMagazine2 = null;
    [SerializeField]
    private ISwitchViewCtrl m_uiMagazine3 = null;
    [SerializeField]
    private ISwitchViewCtrl m_uiMagazine4 = null;

    public void SetEnables(UserID id, bool isEnable)
    {
        switch(id)
        {
            case UserID.User1:
                SetUser1(isEnable);
                break;
            case UserID.User2:
                SetUser2(isEnable);
                break;
            case UserID.User3:
                SetUser3(isEnable);
                break;
            case UserID.User4:
                SetUser4(isEnable);
                break;
        }
    }

    private void SetUser1(bool isEnable)
    {
        m_uiUserNo1.EnableView(isEnable);
        m_uiMagazine1.EnableView(isEnable);

        m_uiUserNo2.EnableView(false);
        m_uiMagazine2.EnableView(false);

        m_uiUserNo3.EnableView(false);
        m_uiMagazine3.EnableView(false);

        m_uiUserNo4.EnableView(false);
        m_uiMagazine4.EnableView(false);
    }

    private void SetUser2(bool isEnable)
    {
        m_uiUserNo2.EnableView(isEnable);
        m_uiMagazine2.EnableView(isEnable);

        m_uiUserNo1.EnableView(false);
        m_uiMagazine1.EnableView(false);

        m_uiUserNo3.EnableView(false);
        m_uiMagazine3.EnableView(false);

        m_uiUserNo4.EnableView(false);
        m_uiMagazine4.EnableView(false);
    }

    private void SetUser3(bool isEnable)
    {
        m_uiUserNo3.EnableView(isEnable);
        m_uiMagazine3.EnableView(isEnable);

        m_uiUserNo2.EnableView(false);
        m_uiMagazine2.EnableView(false);

        m_uiUserNo1.EnableView(false);
        m_uiMagazine1.EnableView(false);

        m_uiUserNo4.EnableView(false);
        m_uiMagazine4.EnableView(false);
    }

    private void SetUser4(bool isEnable)
    {
        m_uiUserNo4.EnableView(isEnable);
        m_uiMagazine4.EnableView(isEnable);

        m_uiUserNo2.EnableView(false);
        m_uiMagazine2.EnableView(false);

        m_uiUserNo3.EnableView(false);
        m_uiMagazine3.EnableView(false);

        m_uiUserNo1.EnableView(false);
        m_uiMagazine1.EnableView(false);
    }

}
