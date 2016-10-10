using UnityEngine;
using System.Collections;

using AkiVACO;

public class UserCameraAutoCtrl : MonoBehaviour {

    [SerializeField]
    private Transform m_pivot = null;

    [SerializeField]
    private Transform m_target = null;

    [SerializeField]
    private float m_rotateSpeed = 10.0f;

    void Awake()
    {
        XLogger.LogValidObject(m_pivot == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Pivot"), gameObject);
        XLogger.LogValidObject(m_target == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Target"), gameObject);

    }
	
    void Update()
    {
        m_pivot.rotation = Quaternion.Slerp(m_pivot.rotation, m_target.rotation, m_rotateSpeed * Time.deltaTime);
        m_pivot.position = m_target.position;
    }

}
