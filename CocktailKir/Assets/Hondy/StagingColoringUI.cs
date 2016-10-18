using UnityEngine;
using System.Collections;

public class StagingColoringUI : IStagingUI {

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
