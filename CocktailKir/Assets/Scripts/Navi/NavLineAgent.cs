using UnityEngine;
using System.Collections;

public class NavLineAgent : MonoBehaviour {

    public NavLines m_navLines = null;

    public Vector3 m_vecTarget = Vector3.zero;
    
    private int m_index = 0;

    public bool NextIndex()
    {
        if (m_index < m_navLines.m_table.Length)
        {
            SetTrans(m_navLines.m_table[m_index]);
            ++m_index;
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetTrans(Vector3 position)
    {
        position.y = this.transform.position.y;
        m_vecTarget = position;
    }

}
