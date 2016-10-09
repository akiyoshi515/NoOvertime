using UnityEngine;
using System.Collections;

using AkiVACO;

public class UserCameraCtrl : MonoBehaviour {

    [SerializeField]
    private Transform m_pivot = null;

    [SerializeField]
    private Transform m_target = null;

    [SerializeField]
    private float m_rotateSpeed = 20.0f;

    void Awake()
    {
        XLogger.LogValidObject(m_pivot == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Pivot"), gameObject);
        XLogger.LogValidObject(m_target == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Target"), gameObject);

    }
	
    void Update()
    {
        float delta = Time.deltaTime;
        float angle = 0.0f;

        if (Input.GetKey(KeyCode.Q))
        {
            angle -= m_rotateSpeed * delta;
        }
        if (Input.GetKey(KeyCode.E))
        {
            angle += m_rotateSpeed * delta;
        }

        UpdatePosition();
        if (angle != 0.0f)
        {
            m_pivot.Rotate(Vector3.up, angle);
        }
    }

    private void UpdatePosition()
    {
        m_pivot.position = m_target.position;
    }

}
