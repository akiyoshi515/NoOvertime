using UnityEngine;
using System.Collections;

public class BulletTrigger : MonoBehaviour
{
    protected UserID m_userID = UserID.User1;
    public UserID userID
    {
        get { return m_userID; }
    }

    protected int m_charm = 0;
    public int charm
    {
        get { return m_charm; }
    }

    private bool m_flag = false;

    // Update is called once per frame
    void Update()
    {
        if (m_flag)
        {
            Destroy(this.gameObject);
        }
        m_flag = true;
    }

    public void SetParam(UserID id, int charm, float hitRadius)
    {
        m_userID = id;
        m_charm = charm;

        SphereCollider col = this.GetComponent<SphereCollider>();
        col.radius = hitRadius;
    }
}
