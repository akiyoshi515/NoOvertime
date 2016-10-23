using UnityEngine;
using System.Collections;

public class UserCameraPivotSetupper : MonoBehaviour
{
    [System.Serializable]
    public class Point
    {
        public Quaternion rotation = Quaternion.identity;
        public Vector3 position = Vector3.zero;
    }

    public Point m_stdPivot = new Point();
    public Point m_shotPivot = new Point();

    private Transform m_camera = null;

    private float m_lerpRate = 1.0f;
    private float m_time = 0.0f;
    private float m_rate = 0.0f;    // 1.0f or -1.0f or 0.0f

    void Start()
    {
        m_camera = this.transform.GetChild(0);
        m_camera.localPosition = m_stdPivot.position;
        m_camera.localRotation = m_stdPivot.rotation;
        m_rate = 0.0f;
    }

    void LateUpdate()
    {
        if (m_rate == 0.0f)
        {
            return;
        }

        m_time += m_rate * (m_lerpRate * Time.deltaTime);
        if (m_time >= 1.0f)
        {
            m_time = 1.0f;
            m_rate = 0.0f;
        }
        else if (m_time <= 0.0f)
        {
            m_time = 0.0f;
            m_rate = 0.0f;
        }

        m_camera.localPosition = Vector3.Lerp(m_stdPivot.position, m_shotPivot.position, m_time);
        m_camera.localRotation = Quaternion.Slerp(m_stdPivot.rotation, m_shotPivot.rotation, m_time);
    }

    public void SetLerpTime(float time)
    {
        m_lerpRate = 1.0f / time;
    }

    public void SetStdPivot()
    {
        m_rate = -1.0f;
    }

    public void SetShotPivot()
    {
        m_rate = 1.0f;
    }
}
