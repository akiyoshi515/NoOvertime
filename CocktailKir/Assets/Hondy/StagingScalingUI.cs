using UnityEngine;
using System.Collections;

public class StagingScalingUI : IStagingUI {




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



    ///
    /// <summary>   拡縮するかのフラグ. </summary>
    ///

    [SerializeField]
    bool m_isScaling;

    ///
    /// <summary>   The scaling speed.  </summary>
    ///

    [SerializeField]
    Vector3 m_scalingSpeed;

    ///
    /// <summary>   初期拡縮率.    </summary>
    ///

    [SerializeField]
    Vector3 m_startScale;

    ///
    /// <summary>   最終拡縮率.  </summary>
    ///

    [SerializeField]
    Vector3 m_endScale;

    ///
    /// <summary>   Use this for initialization.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/18.  </remarks>
    ///

    void Start ()
    {
    }

    ///
    /// <summary>   Update is called once per frame.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/18.  </remarks>
    ///

	void Update ()
    {
        Scaling();

    }

    void Scaling()
    {
        if (m_isScaling)
        {
            m_controlUIImage.rectTransform.localScale += m_scalingSpeed * AkiVACO.XTime.deltaTime;
        }
    }

}
