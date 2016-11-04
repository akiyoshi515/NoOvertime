using UnityEngine;
using System.Collections;
using UnityEngine.UI;
///
/// <summary>   距離UI ここで情報を受け取ってパーツを制御.  </summary>
///
/// <remarks>   Hondy, 2016/11/02.  </remarks>
///
/// <seealso cref="T:MonoBehaviour"/>
///

public class DistanceUI : MonoBehaviour {

    [SerializeField]
    Image m_floatIcon;
    [SerializeField]
    float m_distanceOfCourse;
    [SerializeField]
    float m_currrentDistance;

    Vector3 pos;

    [SerializeField]
    Canvas testCanvas;

    [SerializeField]
    NavLines nav;

    [SerializeField]
    GameObject m_float;

    Vector3 m_current;
    Vector3 m_pre;
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < nav.table.Length-1;i++)
        {
            m_distanceOfCourse += Vector3.Distance(nav.table[i], nav.table[i + 1]);
        }
        
        Vector2 size = GetComponent<Image>().rectTransform.sizeDelta;
        pos = new Vector3(0, -size.y * 0.5f, 0);
        m_floatIcon.rectTransform.localPosition = pos;
        m_current = m_float.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        m_pre = m_current;
        m_current = m_float.transform.position;

        m_currrentDistance += Vector3.Distance(m_pre, m_current);

        Vector2 size = GetComponent<Image>().rectTransform.sizeDelta;
        pos = new Vector3(0, -size.y * 0.5f + (m_currrentDistance / m_distanceOfCourse) * size.y, 0);
        m_floatIcon.rectTransform.localPosition = pos;
    }
}
