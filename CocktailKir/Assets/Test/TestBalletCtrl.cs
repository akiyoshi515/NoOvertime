using UnityEngine;
using System.Collections;

using AkiVACO;

public class TestBalletCtrl : MonoBehaviour
{
    [SerializeField]
    private float m_hitRadius = 1;

    [SerializeField]
    private int m_cost = 1;
    public int cost
    {
        get { return m_cost; }
    }

    [SerializeField]
    private GameObject m_hitTrigger = null;

    [SerializeField]
    private GameObject[] m_onDestroyedObject = null;

    private bool m_isDead = false;

    void Start()
    {
        if (m_hitTrigger != null)
        {
            SphereCollider col = m_hitTrigger.GetComponent<SphereCollider>();
            col.radius = m_hitRadius;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag != "Ballet")
        {
            SendHit();
        }
    }

    public void SendHit()
    {
        if (m_isDead)
        {
            return;
        }

        if (m_hitTrigger != null)
        {
            GameObject.Instantiate(m_hitTrigger, this.transform.position, Quaternion.identity);
        }

        if (m_onDestroyedObject != null)
        {
            foreach (GameObject obj in m_onDestroyedObject)
            {
                GameObject.Instantiate(obj, this.transform.position, Quaternion.identity);
            }
        }
        m_isDead = true;
        Destroy(this.gameObject);
        
    }
}
