/// 
// file:	Assets\Hondy\StagingRotatingUI.cs
//
// summary:	Implements the staging rotating user interface class
/// 

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///
/// <summary>   A staging rotating user interface.  </summary>
///
/// <remarks>   Hondy, 2016/10/19.  </remarks>
///
/// <seealso cref="T:IStagingUI"/>
///

public class StagingRotatingUI : IStagingUI {

    ///
    /// <summary>   回転するかのフラグ.  </summary>
    ///

    [SerializeField, Header("回転するかのフラグ")]
    bool m_isRotation;
    ///
    /// <summary>   回転停止のフラグ設定.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/12.  </remarks>
    ///

    public enum ROTATE_STOP_SETTING_ENUM
    {
        /// <summary>   ストップしない.  </summary>
        NOT_STOP,
        /// <summary>   回転量をしてしてで停止. </summary>
        AMOUNT_ROTATE_STOP,
        /// <summary>   絶対数の回転量をしてしてで停止. </summary>
        ABSOLUTE_AMOUNT_ROTATE_STOP,
        /// <summary>   指定角度で停止. </summary>
        SPECIFIED_ANGLE_STOP,
        /// <summary>   時間経過で停止. </summary>
        TIME_STOP
    }

    ///
    /// <summary>   回転停止設定.    </summary>
    ///
    [SerializeField]
    ROTATE_STOP_SETTING_ENUM m_rotateStopSetting;

    ///
    /// <summary>   回転速度(秒速). </summary>
    ///

    [SerializeField]
    float m_rotationSpeedPerSecond;

    ///
    /// <summary>   開始角度.    </summary>
    ///

    [SerializeField]
    float m_startAngle;

    ///
    /// <summary>   最終角度.  </summary>
    ///

    [SerializeField]
    float m_endAngle;

    ///
    /// <summary>   総回転量.    </summary>
    ///

    float m_totalAmountOfRotate;

    ///
    /// <summary>   The stoppped total amount of rotate.    </summary>
    ///

    float m_stopppedTotalAmountOfRotate;


    ///
    /// <summary>   回転絶対量.    </summary>
    ///

    float m_totalAbsoluteAmountOfRotate;

    ///
    /// <summary>   停止するための回転絶対量.    </summary>
    ///

    float m_stoppedTotalAbsoluteAmountOfRotate;

    ///
    ///
    /// <summary>   Use this for initialization.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/19.  </remarks>
    ///

    void Start ()
    {
        if (m_controlUIImage == null)
        {
            m_controlUIImage = this.gameObject.GetComponent<Image>();
        }
        if (m_controlUIImage)
        {
            m_controlUIImage.rectTransform.eulerAngles = new Vector3(0, 0, m_startAngle);

        }
    }

    ///
    /// <summary>   Update is called once per frame.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/19.  </remarks>
    ///

    void Update ()
    {

        Rotation();
    }

    ///
    /// <summary>   Rotations this object.  </summary>
    ///
    /// <remarks>   Hondy, 2016/10/19.  </remarks>
    ///

    void Rotation()
    {

        if (m_isRotation)
        {
            // 回転量増加
            m_totalAmountOfRotate += m_rotationSpeedPerSecond * AkiVACO.XTime.deltaTime;

            // 回転量増加
            m_totalAbsoluteAmountOfRotate += Mathf.Abs( m_rotationSpeedPerSecond * AkiVACO.XTime.deltaTime);

            // 回転
            if (m_controlUIImage != null)
            {
                m_controlUIImage.rectTransform.eulerAngles = new Vector3(0, 0, m_startAngle + m_totalAmountOfRotate);
            } 
            else
            {
                transform.transform.eulerAngles = new Vector3(0, 0, m_startAngle + m_totalAmountOfRotate);
            }
        }
        CheckDoRotation();
    }

    bool CheckDoRotation()
    {
        switch (m_rotateStopSetting)
        {
            case ROTATE_STOP_SETTING_ENUM.NOT_STOP:
                return true;
                break;
            case ROTATE_STOP_SETTING_ENUM.AMOUNT_ROTATE_STOP:
                if (m_totalAmountOfRotate < m_stopppedTotalAmountOfRotate)
                {
                    return true;
                }
                break;
            case ROTATE_STOP_SETTING_ENUM.ABSOLUTE_AMOUNT_ROTATE_STOP:
                if (m_totalAbsoluteAmountOfRotate < m_stoppedTotalAbsoluteAmountOfRotate)
                {
                    return true;
                }
                break;
            case ROTATE_STOP_SETTING_ENUM.SPECIFIED_ANGLE_STOP:
                if (m_rotationSpeedPerSecond < 0)
                {
                    // 回転方向がマイナス

                    // 最終回転差分がマイナス
                    if (m_endAngle - m_startAngle < 0)
                    {

                        if (m_endAngle < m_startAngle + m_totalAmountOfRotate)
                        {
                            m_isRotation = true;
                            return true;
                        }

                    }
                    // 最終回転差分がプラス
                    else
                    {
                        if (m_startAngle + m_totalAmountOfRotate < m_endAngle)
                        {
                            m_isRotation = true;
                            return true;
                        }
                    }
                } 
                else
                {
                    // 最終回転差分がマイナス
                    if (m_endAngle - m_startAngle < 0)
                    {

                        if (m_endAngle < m_startAngle + m_totalAmountOfRotate)
                        {
                            m_isRotation = true;
                            return true;
                        }

                    }
                    // 最終回転差分がプラス
                    else
                    {
                        if (m_startAngle + m_totalAmountOfRotate < m_endAngle)
                        {
                            m_isRotation = true;
                            return true;
                        }
                    }
                }

                break;
            case ROTATE_STOP_SETTING_ENUM.TIME_STOP:
                if (m_deltaTime < m_stopTime)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        m_isRotation = false;
        return false;
    }
}
