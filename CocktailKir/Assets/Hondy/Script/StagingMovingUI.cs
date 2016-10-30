/// 
// file:	Assets\Hondy\StagingMovingUI.cs
//
// summary:	Implements the staging moving user interface class
/// 

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///
/// <summary>   A staging moving user interface.    </summary>
///
/// <remarks>   Hondy, 2016/10/19.  </remarks>
///
/// <seealso cref="T:IStagingUI"/>
///

public class StagingMovingUI : IStagingUI {

    ///
    /// <summary>   スクロールするかのフラグ.   </summary>
    ///

    [SerializeField, Header("スクロール,移動するかのフラグ")]
    bool m_isMove;

    ///
    /// <summary>   スクロール速度(秒速).    </summary>
    ///

    [SerializeField, Header("スクロール,移動速度(秒速)")]
    float m_scrollSpeed;

    ///
    /// <summary>   スクロールに開始位置を使用するかのフラグ.   </summary>
    ///

    [SerializeField, Header("スクロール,移動開始位置を使用するかのフラグ(falseの場合インスペクタで設定した位置からスクロール)")]
    bool m_isUseStartPosition;

    ///
    /// <summary>   スクロール開始位置.  </summary>
    ///

    [SerializeField, Header("移動開始位置")]
    Vector3 m_scrollStartPosition;

    ///
    /// <summary>   スクロール開始位置.  </summary>
    ///

    [SerializeField, Header("移動停止位置")]
    Vector3 m_scrollStopPosition;

    ///
    /// <summary>   スクロール方向.    </summary>
    ///

    [SerializeField, Header("移動方向")]
    Vector3 m_scrollDirection;

    ///
    /// <summary>   移動停止のフラグ設定.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/12.  </remarks>
    ///

    public enum MOVE_STOP_SETTING_ENUM
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
    MOVE_STOP_SETTING_ENUM m_scrollStopSetting;

    ///
    /// <summary>   The scroll stop distance.   </summary>
    ///

    float m_scrollStopDistance;

    ///
    /// <summary>   The scroll delta position.  </summary>
    ///

    Vector3 m_deltaPosition;

    ///
    /// <summary>   The scroll stop time.   </summary>
    ///

    float m_MovingStopTime;

    ///
    /// <summary>   The scroll delta time.  </summary>
    ///

    float m_deltaTime;

    ///
    /// <summary>   UI画像.   </summary>
    ///

    [SerializeField]
    Image m_controlUIImage;

    ///
    /// <summary>   The canvas. </summary>
    ///

    RectTransform m_UICanvas;

    ///
    /// <summary>   Use this for initialization.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/19.  </remarks>
    ///

    void Start ()
    {
        if (m_UICanvas == null)
        {
            m_UICanvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        }

        if (m_controlUIImage == null)
        {
            m_controlUIImage = this.gameObject.GetComponent<Image>();
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
    /// <summary>   Update is called once per frame.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/19.  </remarks>
    ///

    void Update()
    {

        Scroll();
    }

    ///
    /// <summary>   Scrolls this object.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/19.  </remarks>
    ///

    void Scroll()
    {
        /// フラグが立ってたらスクロール処理
        if (m_isMove)
        {
            if (CheckDoScrooll())
            {
                m_deltaTime += AkiVACO.XTime.deltaTime;

                m_controlUIImage.rectTransform.position = m_scrollStartPosition + m_scrollDirection * m_scrollSpeed * m_deltaTime;

            }
        }
    }

    ///
    /// <summary>   移動するフラグが立っているかチェック. </summary>
    ///
    /// <remarks>   Hondy, 2016/10/19.  </remarks>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    ///

    bool CheckDoScrooll()
    {
        bool result = false;
        switch (m_scrollStopSetting)
        {
            case MOVE_STOP_SETTING_ENUM.NOT_STOP:
                result = true;
                break;
            case MOVE_STOP_SETTING_ENUM.DISTANCE_STOP:
                if (Vector3.Distance(m_deltaPosition, m_scrollStartPosition) < m_scrollStopDistance)
                {
                    result = true;
                }
                break;
            case MOVE_STOP_SETTING_ENUM.TIME_STOP:
                if (m_deltaTime < m_MovingStopTime)
                {
                    result = true;
                }
                break;
            case MOVE_STOP_SETTING_ENUM.HEIGHT_CENTER_STOP:
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
            case MOVE_STOP_SETTING_ENUM.WIDTH_CENTER_STOP:
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
            case MOVE_STOP_SETTING_ENUM.SCREEEN_OUT_STOP:
                // 回転した矩形同士で衝突判定
                if (CheckCollision2DOfRectangles(m_controlUIImage.rectTransform.anchoredPosition, m_controlUIImage.rectTransform.sizeDelta.x, m_controlUIImage.rectTransform.sizeDelta.y,
                    new Vector3(0, 0, 0), m_UICanvas.rect.width, m_UICanvas.rect.height))
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

    bool CheckCollision2DOfRectangles(Vector3 centerA, float widthA, float heightA, Vector3 centerB, float widthB, float heightB)
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
                    _ae = pointsA[i + 1];

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
    /// <param name="_lineAStart">  The line a. </param>
    /// <param name="_lineAEnd">    The line b. </param>
    /// <param name="_lineBStart">  The line b start.   </param>
    /// <param name="_lineBEnd">    The line b end. </param>
    ///
    /// <returns>   true if it succeeds, false if it fails. </returns>
    ///

    bool CheckCollision2DOfLines(Vector3 _lineAStart, Vector3 _lineAEnd, Vector3 _lineBStart, Vector3 _lineBEnd)
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
        vecCross = Vector3.Cross(_vectorA, _vectorB);
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
    /// <param name="_point">           The point.  </param>
    /// <param name="_rectanglePoints"> The rectangle.  </param>
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
                    _endPoint = _rectanglePoints[i + 1];
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
    /// <param name="_vecA">    The left hand side. </param>
    /// <param name="_vecB">    The right hand side.    </param>
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
        Vector2 _se, _sp;
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


}
