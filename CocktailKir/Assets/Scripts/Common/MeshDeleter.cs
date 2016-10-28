using UnityEngine;
using System.Collections;

public class MeshDeleter : MonoBehaviour
{

    public bool m_isAwake = false;

    void Awake()
    {
#if DEBUG
        if (m_isAwake)
        {
            GameObject.Destroy(this.GetComponent<MeshRenderer>());
            GameObject.Destroy(this.GetComponent<MeshFilter>());
            GameObject.Destroy(this);
        }
#else
        GameObject.Destroy(this.GetComponent<MeshRenderer>());
        GameObject.Destroy(this.GetComponent<MeshFilter>());
        GameObject.Destroy(this);
#endif
    }

}
