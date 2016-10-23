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


public class IStagingUI : MonoBehaviour
{
    
    
    ///
    /// <summary>   UI画像(nullの場合start時にコンポーネントを探索).  </summary>
    ///

    [SerializeField]
    public Image m_controlUIImage;

    RectTransform m_UICanvas;

    ///
    /// <summary>   反復して演出するか.  </summary>
    ///

    bool m_isRepeat;

    ///
    /// <summary>   反復するときに移動方向などを反復させるか. </summary>
    ///

    bool m_isReverseWhenRepeat;

    ///
    /// <summary>   反復するときに位置などをリセットするか.   </summary>
    ///

    bool m_isResetWhenRepeat;

    ///
    /// <summary>   初期化.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/11.  </remarks>
    ///

    void Start ()
    {
        if (m_controlUIImage == null)
        {
            m_controlUIImage = this.gameObject.GetComponent<Image>();   
        }
    }

    ///
    /// <summary>   フレームごとの更新処理.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/11.  </remarks>
    ///

    void Update ()
    {
    }


    public virtual void Reset()
    {

    }
    

}
