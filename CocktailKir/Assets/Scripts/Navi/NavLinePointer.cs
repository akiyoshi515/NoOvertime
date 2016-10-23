using UnityEngine;
using System.Collections;

public class NavLinePointer : MonoBehaviour
{
    [SerializeField]
    protected bool m_isLoop = false;
    public bool isLoop
    {
        get { return m_isLoop; }
    }

}
