using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///
/// <summary>   距離UIの部分的なもの（上下で組み合わせて表現）.  </summary>
///
/// <remarks>   Hondy, 2016/11/02.  </remarks>
///
/// <seealso cref="T:MonoBehaviour"/>
///

public class DistancePartialUI : MonoBehaviour
{

    Image m_imageCompornent;

    ///
    /// <summary>   The float icon. </summary>
    ///
    [SerializeField]
    Image m_floatIcon;
    public UnityEngine.UI.Image FloatIcon
    {
        get { return m_floatIcon; }
        set { m_floatIcon = value; }
    }

    float m_startDistance;
    float m_endDistance;

    float m_currrentDistance;

    Vector3 m_diffPosition;

    // Use this for initialization
    void Start()
    {
        m_imageCompornent = GetComponent<Image>();
        m_diffPosition = m_imageCompornent.rectTransform.position;
        m_diffPosition.y -= m_imageCompornent.rectTransform.sizeDelta.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_floatIcon)
        {
            m_floatIcon.rectTransform.position = new UnityEngine.Vector3(
                m_floatIcon.rectTransform.position.x,
                0,
                m_floatIcon.rectTransform.position.x
                );
        }

    }
}
