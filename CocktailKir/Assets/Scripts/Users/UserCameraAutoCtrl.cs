using UnityEngine;
using System.Collections;

using AkiVACO;

public class UserCameraAutoCtrl : MonoBehaviour
{
    [SerializeField]
    private Transform m_pivot = null;

    [SerializeField]
    private Transform m_targetUser = null;

    [SerializeField]
    private float m_rotateSpeed = 10.0f;

    [SerializeField]
    private float m_pivotLerpTime = 0.50f;

    private IXVInput m_input = null;
    private UserCharCtrl m_charCtrl = null;
    private bool m_prevLauncherStance = false;

    void Awake()
    {
        XLogger.LogValidObject(m_pivot == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Pivot"), gameObject);
        XLogger.LogValidObject(m_targetUser == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Target"), gameObject);
    }

    void Start()
    {
        UserCameraPivotSetupper setupper = m_pivot.GetComponent<UserCameraPivotSetupper>();
        setupper.SetLerpTime(m_pivotLerpTime);

        m_input = m_targetUser.GetComponent<UserData>().input;
        m_charCtrl = m_targetUser.GetComponent<UserCharCtrl>();
    }

    void Update()
    {
        bool bl = m_charCtrl.isLauncherStance;

        if (m_prevLauncherStance ^ bl)
        {
            if (bl)
            {
                // Start
                m_pivot.GetComponent<UserCameraPivotSetupper>().SetShotPivot();
            }
            else
            {
                // Stop
                m_pivot.GetComponent<UserCameraPivotSetupper>().SetStdPivot();
            }
            m_prevLauncherStance = bl;
        }
    }

    void LateUpdate()
    {
        m_pivot.rotation = Quaternion.Slerp(m_pivot.rotation, m_targetUser.rotation, m_rotateSpeed * Time.deltaTime);
        m_pivot.position = m_targetUser.position;
    }

    public void Edit_SetTargetUser(Transform user)
    {
#if UNITY_EDITOR
        m_targetUser = user;
#endif
    }
}
