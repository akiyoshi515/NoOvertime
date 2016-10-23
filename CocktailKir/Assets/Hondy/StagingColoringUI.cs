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

    ///
    /// <summary>   カラー変更速度(秒速). </summary>
    ///
    [SerializeField]
    Color m_changeColoringSpeedPerSecond;

    ///
    /// <summary>   初期カラー.    </summary>
    ///

    [SerializeField]
    float m_startColor;

    ///
    /// <summary>   最終カラー.  </summary>
    ///

    float m_endColor;

 
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    void Coloring()
    {
        if (m_isColoring)
        {

            m_controlUIImage.color += (m_changeColoringSpeedPerSecond * AkiVACO.XTime.deltaTime);
        }
    }
}
