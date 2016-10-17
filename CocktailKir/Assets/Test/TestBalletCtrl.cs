using UnityEngine;
using System.Collections;

public class TestBalletCtrl : MonoBehaviour
{
    [SerializeField]
    private int m_cost = 1;
    public int cost
    {
        get { return m_cost; }
    }

    [SerializeField]
    private GameObject m_onDestroyedObject = null;

    private bool m_isDead = false;

    void OnCollisionEnter(Collision col)
    {
        if (m_isDead)
        {
            return;
        }

        if (col.collider.tag != "Ballet")
        {
            if (m_onDestroyedObject != null)
            {
                GameObject.Instantiate(m_onDestroyedObject, this.transform.position, Quaternion.identity);
            }
            m_isDead = true;
            Destroy(this.gameObject);
        }
    }
}
