using UnityEngine;
using System.Collections;

public class StagingColoringUI : IStagingUI {
    
    ///
    /// <summary>   スクロールのフラグ管理.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/12.  </remarks>
    ///

    public enum COLORING_STOP_FLAG_ENUM
    {
        /// <summary>   ストップしない.  </summary>
        NOT_STOP,
        /// <summary>   距離計算で停止. </summary>
        COLOR_STOP,
        /// <summary>   時間経過で停止. </summary>
        TIME_STOP,
    }

    

    [SerializeField]
    bool m_isColoring;

    [SerializeField]
    bool m_isRepeat;


    [SerializeField]
    bool m_isRepeatReverse;
    

    ///
    /// <summary>   初期カラー.    </summary>
    ///

    [SerializeField]
    Color m_startColor;

    ///
    /// <summary>   最終カラー.  </summary>
    ///

    [SerializeField]
    Color m_endColor;

    [SerializeField]
    Color m_CurrentColor;



    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_stopTime < m_deltaTime)
        {
            if (m_isRepeat)
            {
                m_deltaTime = 0;

                if (m_isRepeatReverse)
                {
                    Color temp;
                    temp = m_endColor;
                    m_endColor = m_startColor;
                    m_startColor = temp;
                }
            }
        }
        else
        {
            m_deltaTime += AkiVACO.XTime.deltaTime;
            Trans();
        }
        m_controlUIImage.color = m_CurrentColor;

    }

    void Trans()
    {
        m_CurrentColor = Color.Lerp(m_startColor,m_endColor,m_deltaTime/m_stopTime);
    }
}
