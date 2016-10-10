using UnityEngine;
using System.Collections;

using AkiVACO;
using AkiVACO.XValue;

public class UserCameraCtrl : MonoBehaviour {

    [SerializeField]
    private Transform m_pivot = null;

    [SerializeField]
    private Transform m_target = null;

    [SerializeField]
    private float m_rotateSpeed = 20.0f;

    [SerializeField]
    private float m_autoRotateSpeed = 5.0f;

    [SerializeField]
    private float m_autoRotateTime = 1.0f;

    private float m_slerpTime = 0.0f;   // value >= m_autoRotateTime -> end
    
    void Awake()
    {
        XLogger.LogValidObject(m_pivot == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Pivot"), gameObject);
        XLogger.LogValidObject(m_target == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Target"), gameObject);

        m_slerpTime = m_autoRotateTime;
    }
	
    void Update()
    {
        float delta = Time.deltaTime;
        float angle = 0.0f;

        IXVInput input = XVInput.GetInterface(UserID.User1);
        
        UpdatePosition();
        if (m_slerpTime >= m_autoRotateTime)
        {
            angle = input.RotateCameraH() * m_rotateSpeed * delta;
            if (angle != 0.0f)
            {
                m_pivot.Rotate(Vector3.up, angle);
            }

            if (Input.GetKeyDown(KeyCode.U))    // TODO
            {
                m_slerpTime = 0.0f;
            }
        }
        else
        {
            m_slerpTime = (m_slerpTime + Time.deltaTime);
            m_pivot.rotation = Quaternion.Slerp(m_pivot.rotation, m_target.rotation, m_autoRotateSpeed * Time.deltaTime);
        }
    }

    private void UpdatePosition()
    {
        m_pivot.position = m_target.position;
    }

}
