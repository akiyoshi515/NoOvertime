using UnityEngine;
using System.Collections;

public abstract class BHEventBase : MonoBehaviour
{
    protected abstract void OnHitEvent(GameObject obj);

    [SerializeField, Header("再開までの時間")]
    private float m_reentryTime = 1.0f;

    private float m_time = 0.0f;
    public float time
    {
        get { return m_time; }
    }

    void OnCollisionEnter(Collision col)
    {
        if (m_time <= 0.0f)
        {
            if ((col.gameObject.tag == "Bullet") || (col.gameObject.tag == "BulletTrigger"))
            {
                OnHitEvent(col.gameObject);
                m_time = m_reentryTime;
            }
        }
        else
        {
            m_time -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (m_time <= 0.0f)
        {
            if ((col.gameObject.tag == "Bullet") || (col.gameObject.tag == "BulletTrigger"))
            {
                OnHitEvent(col.gameObject);
                m_time = m_reentryTime;
            }
        }
        else
        {
            m_time -= Time.deltaTime;
        }
    }

    public void Restart()
    {
        m_time = 0.0f;
    }
}