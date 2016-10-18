using UnityEngine;
using System.Collections;

public class BalletTrigger : MonoBehaviour
{
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
}
