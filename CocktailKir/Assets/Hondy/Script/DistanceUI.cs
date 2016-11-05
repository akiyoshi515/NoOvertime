using UnityEngine;
using System.Collections;
using UnityEngine.UI;
///
/// <summary>   距離UI .  </summary>
///
/// <remarks>   Hondy, 2016/11/02.  </remarks>
///
/// <seealso cref="T:MonoBehaviour"/>
///

public class DistanceUI : MonoBehaviour {

    Image m_distanceImage;

    ///
    /// <summary>   パレードフロートのアイコン. </summary>
    ///

    [SerializeField]
    Image m_floatIcon;

    ///
    /// <summary>   コース距離. </summary>
    ///

    [SerializeField]
    float m_distanceOfCourse;

    ///
    /// <summary>   現在の距離.  </summary>
    ///

    [SerializeField]
    float m_currrentDistance;

    ///
    /// <summary>   キャンバスでのアイコンの位置.  </summary>
    ///

    Vector3 m_iconPosition;

    ///
    /// <summary>   距離の情報を取得するためのナビライン.   </summary>
    ///

    [SerializeField]
    NavLines m_navLines;

    ///
    /// <summary>   パレードフロートオブジェクト（走行距離を算出）.    </summary>
    ///

    [SerializeField]
    GameObject m_ParadeFloatObject;

    ///
    /// <summary>   フロートの今の位置.   </summary>
    ///

    Vector3 m_currentPositionOfParadeFloat;

    ///
    /// <summary>   フロートの前の位置.   </summary>
    ///

    Vector3 m_prePositionOfParadeFloat;

    ///
    /// <summary>   レーダーのフレーム 被ったらワープ.  </summary>
    ///

    [SerializeField]
    Image m_radarFrameImage;

    // Use this for initialization
    void Start ()
    {
        // ナビラインから地点の情報をもらってくる
        m_distanceOfCourse = m_navLines.table[m_navLines.table.Length -1].y;
        m_distanceImage = GetComponent<Image>();
        m_iconPosition = new Vector3(0, -m_distanceImage.rectTransform.sizeDelta.y * 0.5f, 0);
        m_floatIcon.rectTransform.localPosition = m_iconPosition;
        m_currentPositionOfParadeFloat = m_ParadeFloatObject.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        m_prePositionOfParadeFloat = m_currentPositionOfParadeFloat;
        m_currentPositionOfParadeFloat = m_ParadeFloatObject.transform.position;

        m_currrentDistance += Vector3.Distance(m_prePositionOfParadeFloat, m_currentPositionOfParadeFloat);
        
        m_iconPosition = new Vector3(0, -(m_distanceImage.rectTransform.sizeDelta.y ) * 0.5f + (m_currrentDistance / m_distanceOfCourse) * (m_distanceImage.rectTransform.sizeDelta.y - m_radarFrameImage.rectTransform.sizeDelta.y), 0);
       
        if ( - m_radarFrameImage.rectTransform.sizeDelta.y*0.5f < m_iconPosition.y )
        {
            m_iconPosition = new Vector3(0, m_radarFrameImage.rectTransform.sizeDelta.y * 0.5f - (m_distanceImage.rectTransform.sizeDelta.y) * 0.5f + m_radarFrameImage.rectTransform.sizeDelta.y*0.5f + (m_currrentDistance / m_distanceOfCourse) * (m_distanceImage.rectTransform.sizeDelta.y - m_radarFrameImage.rectTransform.sizeDelta.y), 0);

        }
        m_floatIcon.rectTransform.localPosition = m_iconPosition;
       
    }
}
