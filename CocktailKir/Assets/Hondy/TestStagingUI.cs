using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AkiVACO;
///
/// <summary>   演出UI(試用).  </summary>
///
/// <remarks>   Hondy, 2016/10/11.  </remarks>
///
/// <seealso cref="T:UnityEngine.MonoBehaviour"/>
///


public class TestStagingUI : MonoBehaviour
{


    ///
    /// <summary>   スクロールするかのフラグ. </summary>
    ///

    [SerializeField,Header("スクロールするかのフラグ")]
    bool m_isScroll;

    



    ///
    /// <summary>   スクロール速度(秒速).   </summary>
    ///

    [SerializeField, Header("スクロール速度(秒速)")]
    float m_scrollSpeed;

    ///
    /// <summary>   スクロールに開始位置を使用するかのフラグ.  </summary>
    ///

    [SerializeField, Header("スクロール開始位置を使用するかのフラグ(falseの場合インスペクタで設定した位置からスクロール)")]
    bool m_isUseStartPosition;

    ///
    /// <summary>   スクロール開始位置.  </summary>
    ///

    [SerializeField, Header("スクロール開始位置")]
    Vector3 m_scrollStartPosition;

    ///
    /// <summary>   スクロール開始位置.  </summary>
    ///

    [SerializeField, Header("スクロール停止位置")]
    Vector3 m_scrollStopPosition;


    ///
    /// <summary>   スクロール方向.   </summary>
    ///

    [SerializeField, Header("スクロール方向")]
    Vector3 m_scrollDirection;


    ///
    /// <summary>   スクロールのフラグ管理.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/12.  </remarks>
    ///

    public enum SCROLL_STOP_FLAG_ENUM
    {
        /// <summary>   スクロールストップしない.  </summary>
        NOT_STOP,
        /// <summary>   距離計算で停止. </summary>
        DISTANCE_STOP,
        /// <summary>   時間経過で停止. </summary>
        TIME_STOP,
        /// <summary>   画面のYの中心で停止.    </summary>
        HEIGHT_CENTER_STOP,
        /// <summary>   画面のXの中心で停止. </summary>
        WIDTH_CENTER_STOP,
        /// <summary>   画面外に完全に出た瞬間停止.  </summary>
        SCREEEN_OUT_STOP
    }

    ///
    /// <summary>   The scroll stop flag.   </summary>
    ///
    [SerializeField]
    SCROLL_STOP_FLAG_ENUM m_scrollStopFlag;

    ///
    /// <summary>   The scroll stop distance.   </summary>
    ///

    float m_scrollStopDistance;

    ///
    /// <summary>   The scroll delta position.  </summary>
    ///

    Vector3 m_scrollDeltaPosition;

    ///
    /// <summary>   The scroll stop time.   </summary>
    ///

    float m_scrollStopTime;

    ///
    /// <summary>   The scroll delta time.  </summary>
    ///

    float m_scrollDeltaTime;


    ///
    /// <summary>   回転するかのフラグ. </summary>
    ///

    [SerializeField, Header("回転するかのフラグ")]
    bool m_isRotation;

    ///
    /// <summary>   The rotation speed. </summary>
    ///

    [SerializeField]
    float m_rotationSpeed;

    ///
    /// <summary>   The start angle.    </summary>
    ///

    [SerializeField]
    float m_startAngle;

    ///
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
    /// <summary>   透過するかのフラグ. </summary>
    ///

    [SerializeField]
    bool m_isTransmission;

    ///
    /// <summary>   透過速度(秒速). </summary>
    ///

    [SerializeField]
    float m_transmisssionSpeedPerSecond;

    ///
    /// <summary>   初期透過率.    </summary>
    ///

    [SerializeField]
    float m_startTransmittance;

    ///
    /// <summary>   最終透過率.  </summary>
    ///

    [SerializeField]
    float m_endTransmittance;

    ///
    /// <summary>   透過するかのフラグ. </summary>
    ///

    [SerializeField]
    bool m_isColoring;

    ///
    /// <summary>   透過速度(秒速). </summary>
    ///
    [SerializeField]
    Color m_changeColoringSpeedPerSecond;

    ///
    /// <summary>   初期透過率.    </summary>
    ///

    [SerializeField]
    float m_startColor;

    ///
    /// <summary>   最終透過率.  </summary>
    ///

    float m_endColor;

    
    ///
    /// <summary>   UI画像.  </summary>
    ///

    [SerializeField]
    Image m_controlUIImage;

    RectTransform m_UICanvas;
    
    ///
    /// <summary>   初期化.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/11.  </remarks>
    ///

    void Start ()
    {
//         Vector3 p, s, e;
//         p = new Vector3(0.5f, 1.5f, 0);
//         s = new Vector3(0, 2, 0);
//         e = new Vector3(1.0f, 2, 0);
//         CheckIfExistPointToRightOfVector(p, s, e
        if (m_controlUIImage == null)
        {
            m_controlUIImage = this.gameObject.GetComponent<Image>();
        }

        if (m_UICanvas == null)
        {
            m_UICanvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        }

        if (m_controlUIImage)
        {
            if (m_isUseStartPosition)
            {
                m_controlUIImage.rectTransform.position = m_scrollStartPosition;
            }
            else
            {
                m_scrollStartPosition = m_controlUIImage.rectTransform.position;

            }

        }

    }

    ///
    /// <summary>   フレームごとの更新処理.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/11.  </remarks>
    ///

    void Update ()
    {
        Scroll();
        Scaling();
        Rotation();
        Transmission();
    }


    void Reset()
    {

    }

    void Scroll()
    {
        /// フラグが立ってたらスクロール処理
        if (m_isScroll)
        {
            if (CheckDoScroollFlag())
            {
                m_scrollDeltaTime += AkiVACO.XTime.deltaTime;
                
                m_controlUIImage.rectTransform.position = m_scrollStartPosition + m_scrollDirection * m_scrollSpeed * m_scrollDeltaTime;
                 
            }
        }
    }


    bool CheckDoScroollFlag()
    {
        bool result = false;
        switch (m_scrollStopFlag)
        {
            case SCROLL_STOP_FLAG_ENUM.NOT_STOP:
                result = true;
                break;
            case SCROLL_STOP_FLAG_ENUM.DISTANCE_STOP:
                if (Vector3.Distance(m_scrollDeltaPosition,m_scrollStartPosition) < m_scrollStopDistance)
                {
                    result = true;
                }
                break;
            case SCROLL_STOP_FLAG_ENUM.TIME_STOP:
                if (m_scrollDeltaTime < m_scrollStopTime)
                {
                    result = true;
                }
                break;
            case SCROLL_STOP_FLAG_ENUM.HEIGHT_CENTER_STOP:
                if (m_scrollDirection.y <= 0)
                {
                    if (m_controlUIImage.rectTransform.position.y > Screen.height * 0.5f)
                    {
                        result = true;
                    }
                }
                else
                {
                    if (m_controlUIImage.rectTransform.position.y < Screen.height * 0.5f)
                    {
                        result = true;
                    }
                }
                break;
            case SCROLL_STOP_FLAG_ENUM.WIDTH_CENTER_STOP:
                if (m_scrollDirection.x <= 0)
                {
                    if (m_controlUIImage.rectTransform.position.x > Screen.width * 0.5f)
                    {
                        result = true;
                    }
                }
                else
                {
                    if (m_controlUIImage.rectTransform.position.x < Screen.width * 0.5f)
                    {
                        result = true;
                    }
                }
                break;
            case SCROLL_STOP_FLAG_ENUM.SCREEEN_OUT_STOP:
                // 回転した矩形同士で衝突判定
                if (CheckCollision2DOfRectangles(m_controlUIImage.rectTransform.anchoredPosition, m_controlUIImage.rectTransform.sizeDelta.x, m_controlUIImage.rectTransform.sizeDelta.y,
                    new Vector3(0, 0, 0), m_UICanvas.rect.width , m_UICanvas.rect.height))
                { 
                    result = true;
                }
                break;
            default:
                result = false;
                break;

        }
        
        return result;
    }

    ///
    /// <summary>   2次元（xy）における矩形同士の衝突判定.   </summary>
    ///
    /// <remarks>   Hondy, 2016/10/15.  </remarks>
    ///
    /// <param name="centerA">  The center a.   </param>
    /// <param name="widthA">   The width a.    </param>
    /// <param name="heightA">  The height a.   </param>
    /// <param name="centerB">  The center b.   </param>
    /// <param name="widthB">   The width b.    </param>
    /// <param name="heightB">  The height b.   </param>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    ///

    bool CheckCollision2DOfRectangles(Vector3 centerA,float widthA, float heightA, Vector3 centerB, float widthB, float heightB)
    {
        // 点と矩形の衝突と線分同士の衝突の複合で判定. 

        // 頂点4つずつと線分4つずつ算出. 

        Vector3[] pointsA = new Vector3[4];
        Vector3[] pointsB = new Vector3[4];
        // pointsA
        pointsA[0].x = centerA.x - widthA * 0.5f;
        pointsA[0].y = centerA.y - heightA * 0.5f;

        pointsA[1].x = centerA.x + widthA * 0.5f;
        pointsA[1].y = centerA.y - heightA * 0.5f;

        pointsA[2].x = centerA.x + widthA * 0.5f;
        pointsA[2].y = centerA.y + heightA * 0.5f;

        pointsA[3].x = centerA.x - widthA * 0.5f;
        pointsA[3].y = centerA.y + heightA * 0.5f;
        
        // pointsB
        pointsB[0].x = centerB.x - widthB * 0.5f;
        pointsB[0].y = centerB.y - heightB * 0.5f;

        pointsB[1].x = centerB.x + widthB * 0.5f;
        pointsB[1].y = centerB.y - heightB * 0.5f;

        pointsB[2].x = centerB.x + widthB * 0.5f;
        pointsB[2].y = centerB.y + heightB * 0.5f;

        pointsB[3].x = centerB.x - widthB * 0.5f;
        pointsB[3].y = centerB.y + heightB * 0.5f;
        

        for (int i = 0; i < 4; i++)
        {
            if (CheckCollision2DOfPointAndRenctangle(pointsA[i], pointsB))
            {
                return true;
            }

            if (CheckCollision2DOfPointAndRenctangle(pointsB[i], pointsA))
            {
                return true;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 _as, _ae, _bs, _be;
                if (i < 3)
                {
                    _as = pointsA[i];
                    _ae = pointsA[i+1];

                    _bs = pointsB[i];
                    _be = pointsB[i + 1];
                }

                // 最後の点のときは0番目の点を終点として使う
                else
                {
                    _as = pointsA[i];
                    _ae = pointsA[0];

                    _bs = pointsB[i];
                    _be = pointsB[0];
                }
                if (CheckCollision2DOfLines(_as, _ae, _bs, _be))
                {
                    return true;
                }
            }
        }
        return false;
    }

    ///

    ///
    /// <summary>   2次元（xy）における線分同士の衝突判定.   </summary>
    ///
    /// <remarks>   Hondy, 2016/10/15.  </remarks>
    ///
    /// <param name="lineAStart">   The line a. </param>
    /// <param name="lineAEnd">     The line b. </param>
    /// <param name="lineBStart">   The line b start.   </param>
    /// <param name="lineBEnd">     The line b end. </param>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    ///

    bool CheckCollision2DOfLines(Vector3 _lineAStart, Vector3 _lineAEnd, Vector3 _lineBStart,Vector3 _lineBEnd)
    {

        /** @brief	seg1のベクトル量を求める. */
        Vector3 _vectorA;
        _vectorA = _lineAStart - _lineAEnd;

        // = seg1End - seg1Start;
        /** @brief	seg2のベクトル量を求める. */
        Vector3 _vectorB;
        _vectorB = _lineBStart - _lineBEnd;

        //= seg2End - seg2Start;

        /** @brief	始点同士を結ぶベクトル. */
        Vector3 _vectorAStartToBStart;
        _vectorAStartToBStart = _lineBStart - _lineAStart;
        Vector3 vecCross;
        vecCross =  Vector3.Cross( _vectorA, _vectorB);
        if (vecCross.z == 0.0f)
        {
            // 平行状態
            return false;
        }

        Vector3 _crossA;
        _crossA = Vector3.Cross(_vectorAStartToBStart, _vectorA);
        Vector3 _crossB;
        _crossB = Vector3.Cross(_vectorAStartToBStart, _vectorB);

        float t1 = _crossB.z / vecCross.z;
        float t2 = _crossA.z / vecCross.z;
        

        const float _eps = 0.00001f;
        if (t1 + _eps < 0 || t1 - _eps > 1 || t2 + _eps < 0 || t2 - _eps > 1)
        {
            // 交差していない
            return false;
        }

        return true;
    }

    ///
    /// <summary>   2次元（xy）における点と矩形の衝突判定.   </summary>
    ///
    /// <remarks>   Hondy, 2016/10/15.  </remarks>
    ///
    /// <param name="point">            The point.  </param>
    /// <param name="rectangleVector">  The rectangle.  </param>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    ///

    bool CheckCollision2DOfPointAndRenctangle(Vector3 _point, Vector3[] _rectanglePoints)
    {
        // 三角以上であること
        if (3 <= _rectanglePoints.Length)
        {
            Vector3 _startPoint, _endPoint;
            for (int i = 0; i < _rectanglePoints.Length; i++)
            {
                if (i < _rectanglePoints.Length - 1)
                {
                    _startPoint = _rectanglePoints[i];
                    _endPoint = _rectanglePoints[i+1];
                }

                // 最後の点のときは0番目の点を終点として使う
                else
                {

                    _startPoint = _rectanglePoints[i];
                    _endPoint = _rectanglePoints[0];
                }
                if (false == CheckIfExistPointToRightOfVector(_point, _startPoint, _endPoint))
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    ///
    /// <summary>   Vector 2 cross. </summary>
    ///
    /// <remarks>   Hondy, 2016/10/15.  </remarks>
    ///
    /// <param name="lhs">  The left hand side. </param>
    /// <param name="rhs">  The right hand side.    </param>
    ///
    /// <returns>   A float.    </returns>
    ///

    static float Vector2Cross(Vector2 _vecA, Vector2 _vecB)
    {
        return _vecA.x * _vecB.y - _vecB.x * _vecA.y;
    }

    ///
    /// <summary>   Check exist right to point of vector.   </summary>
    ///
    /// <remarks>   Hondy, 2016/10/15.  </remarks>
    ///
    /// <param name="_point">               The point.  </param>
    /// <param name="_rectangleStartPoint"> The rectangle start point.  </param>
    /// <param name="_rectangleEndPoint">   The rectangle end point.    </param>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    ///

    bool CheckIfExistPointToRightOfVector(
        Vector3 _point,
        Vector3 _rectangleStartPoint,
        Vector3 _rectangleEndPoint
        )
    {
        Vector2 _p;
        _p.x = _point.x;
        _p.y = _point.y;
        Vector2 _s;
        _s.x = _rectangleStartPoint.x;
        _s.y = _rectangleStartPoint.y;
        Vector2 _e;
        _e.x = _rectangleEndPoint.x;
        _e.y = _rectangleEndPoint.y;
        Vector2 _se,_sp;
        _se = _e - _s;
        _sp = _p - _s;
        // 0未満なら右側
        float _result = Vector2Cross(_se, _sp);
        float n = _p.x * (_s.y - _e.y) + _s.x * (_e.y - _p.y) + _e.x * (_p.y - _s.y);
        if (_result < 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void Rotation()
    {

        if (m_isRotation)
        {
            m_controlUIImage.rectTransform.eulerAngles += new Vector3(0, 0, m_rotationSpeed * XTime.deltaTime);
        }
    }

    void Scaling()
    {
        if (m_isScaling)
        {
            m_controlUIImage.rectTransform.localScale += m_scalingSpeed * XTime.deltaTime;
        }
    }

    void Transmission()
    {
        if (m_isTransmission)
        {
            m_controlUIImage.color += new Color(m_controlUIImage.color.r, m_controlUIImage.color.g, m_controlUIImage.color.b, m_transmisssionSpeedPerSecond * XTime.deltaTime);
        }
    }
    void Coloring()
    {
        if (m_isColoring)
        {

            m_controlUIImage.color += (m_changeColoringSpeedPerSecond * XTime.deltaTime);
        }
    }
}
