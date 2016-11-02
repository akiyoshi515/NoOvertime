using UnityEngine;
using System.Collections;

public class NavLineWaitPointer : MonoBehaviour
{
    [SerializeField]
    protected float m_waitTime = 1.0f;
    public float waitTime
    {
        get { return m_waitTime; }
    }

}
