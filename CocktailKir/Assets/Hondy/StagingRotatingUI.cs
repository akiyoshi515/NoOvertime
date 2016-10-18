using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StagingRotatingUI : IStagingUI {

    /// <summary>   回転するかのフラグ. </summary>
    ///

    [SerializeField, Header("回転するかのフラグ")]
    bool m_isRotation;

    /// <summary>   The rotation speed. </summary>
    [SerializeField]
    float m_rotationSpeed;





    /// <summary>   The start angle.    </summary>
    [SerializeField]
    float m_startAngle;

    /// <summary>   The end angle.  </summary>
    ///

    [SerializeField]
    float m_endAngle;

    
    RectTransform m_UICanvas;
    // Use this for initialization
    void Start () {
        

        if (m_UICanvas == null)
        {
            m_UICanvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        }


    }

    // Update is called once per frame
    void Update () {

        Rotation();
    }
    void Rotation()
    {

        if (m_isRotation)
        {
            m_controlUIImage.rectTransform.eulerAngles += new Vector3(0, 0, m_rotationSpeed * AkiVACO.XTime.deltaTime);
        }
    }
}
