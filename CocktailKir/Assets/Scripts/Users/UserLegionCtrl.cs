using UnityEngine;
using System.Collections;

// TODO
public class UserLegionCtrl : MonoBehaviour
{
    [SerializeField]
    private UserNoUI m_uiUserNo1 = null;
    [SerializeField]
    private UserNoUI m_uiUserNo2 = null;
    [SerializeField]
    private UserNoUI m_uiUserNo3 = null;
    [SerializeField]
    private UserNoUI m_uiUserNo4 = null;

    [SerializeField]
    private Billboard m_uiMagazine1 = null;
    [SerializeField]
    private Billboard m_uiMagazine2 = null;
    [SerializeField]
    private Billboard m_uiMagazine3 = null;
    [SerializeField]
    private Billboard m_uiMagazine4 = null;

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
