using UnityEngine;
using System.Collections;

public class NavLines : MonoBehaviour
{
    [SerializeField]
    protected Vector3[] m_table = null;
    public Vector3[] table
    {
        get { return m_table; }
        set { m_table = value; }
    }

    [SerializeField]
    protected int m_resolution = 4;
    public int resolution
    {
        get { return m_resolution; }
    }

    [SerializeField]
    protected float m_lineViewHeight = 1.0f;
    public float lineViewHeight
    {
        get { return m_lineViewHeight; }
    }

    [SerializeField]
    protected int m_loopPoint = -1;
    public int loopPoint
    {
        get { return m_loopPoint; }
    }

    public bool isLoop
    {
        get { return (m_loopPoint != -1); }
    }

    public int GetPointerCount()
    {
        return this.transform.childCount -1;
    }

}
