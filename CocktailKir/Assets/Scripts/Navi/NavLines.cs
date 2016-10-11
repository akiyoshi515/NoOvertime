using UnityEngine;
using System.Collections;

public class NavLines : MonoBehaviour
{
    public Vector3[] m_table = null;
    public int m_resolution = 4;
    public float m_lineViewHeight = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetPointerCount()
    {
        return this.transform.childCount -1;
    }

}
