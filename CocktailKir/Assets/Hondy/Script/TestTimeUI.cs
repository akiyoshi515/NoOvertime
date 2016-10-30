using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AkiVACO;

///
/// <summary>   試験用制限時間UIスクリプト. </summary>
///
/// <remarks>   Hondy, 2016/10/11.  </remarks>
///
/// <seealso cref="T:UnityEngine.MonoBehaviour"/>
///

public class TestTimeUI : MonoBehaviour {

    ///
    /// <summary>   制限時間.   </summary>
    ///

    [SerializeField,Header("制限時間")]
    float m_rimitMAXTime = 15;

    ///
    /// <summary>   経過制限時間.   </summary>
    ///

    float m_rimitDeltaTime;

    ///
    /// <summary>   秒針スプライトイメージ.    </summary>
    ///

    [SerializeField]
    Image m_secondHandImage;



    ///
    /// <summary>   初期化.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/11.  </remarks>
    ///

	void Start ()
    {
        // 経過時間０
        m_rimitDeltaTime = 0;
        // 回転軸変更
        m_secondHandImage.rectTransform.pivot = new UnityEngine.Vector2(0.5f, 0.0f);
	}

    ///
    /// <summary>   フレームごとの更新.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/11.  </remarks>
    ///

	void Update ()
    {
        // 時間経過
        m_rimitDeltaTime += AkiVACO.XTime.deltaTime;
        // 角度設定
        float _imageAngle = -360*(m_rimitDeltaTime/m_rimitMAXTime);
        m_secondHandImage.rectTransform.eulerAngles = new UnityEngine.Vector3(0,0, _imageAngle);
	}
}
