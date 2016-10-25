using UnityEngine;
using System.Collections;

using AkiVACO;

public class BalletCtrl : MonoBehaviour
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
    private int m_charm = 1;
    public int charm
    {
        get { return m_charm; }
    }

    [SerializeField]
    private GameObject m_hitTrigger = null;

    [SerializeField]
    private GameObject[] m_onDestroyedObject = null;

    public UserID userID
    {
        get;
        protected set;
    }

    private bool m_isDead = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag != "Ballet")
        {
            SendHit();
        }
    }

    public void SetUserID(UserID id)
    {
        userID = id;
    }

    public void AddBonusCharm(int val)
    {
        m_charm += val;
    }

    public void SendHit()
    {
        if (m_isDead)
        {
            return;
        }

        if (m_hitTrigger != null)
        {
            GameObject obj = GameObject.Instantiate(m_hitTrigger, this.transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<BalletTrigger>().SetParam(userID, m_charm, m_hitRadius);
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
