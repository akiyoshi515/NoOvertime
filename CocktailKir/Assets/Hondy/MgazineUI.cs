using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MgazineUI : MonoBehaviour {

    ///
    /// <summary>   残り弾数.   </summary>
    ///
    [SerializeField]
    uint m_numberOfBullet;

    ///
    /// <summary>   残り弾数のゲッターとセッター. </summary>
    ///
    /// <value> マガジンの残り弾数. </value>
    ///

    public uint NumberOfBullet
    {
        get
        {
            return m_numberOfBullet;
        }

        set
        {
            m_numberOfBullet = value;
        }
    }

    ///
    /// <summary>   最大弾数.   </summary>
    ///

    [SerializeField]
    uint m_maximumNumberOfBullet = 6;

    ///
    /// <summary>   マガジンの最大弾数のゲッターとセッター.  </summary>
    ///
    /// <value> マガジンの最大弾数.   </value>
    ///

    public uint MaximumNumberOfBullet
    {
        get
        {
            return m_maximumNumberOfBullet;
        }

        set
        {
            m_maximumNumberOfBullet = value;
        }
    }

    ///
    /// <summary>   ゲージとして使う画像.  </summary>
    ///

    [SerializeField]
    Image m_uiImage;

    ///
    /// <summary>   シェーダー入ってるマテリアル. </summary>
    ///

    Material m_imageMaterial;
    // Use this for initialization
    void Start ()
    {
        if (m_uiImage == null)
        {
            m_uiImage = gameObject.GetComponent<Image>();
        }
        m_imageMaterial = m_uiImage.material;
        float maskvaule = (float)((float)NumberOfBullet / (float)m_maximumNumberOfBullet);
        // maskのテクスチャミスってると若干残る場合があるからマイナスにする
        if (maskvaule <= 0)
        {
            maskvaule = -0.01f;
        }
#if DEBUG
        if (m_imageMaterial.shader.name == "Custom/GaugeSpriteShader")
#endif
        {
            AkiVACO.XLogger.LogWarning("shader間違ってるよ");
            m_imageMaterial.SetFloat("_Mask", maskvaule);
        }


    }
	
	// Update is called once per frame
	void Update ()
    {
        float maskvaule = (float)((float)NumberOfBullet / (float)m_maximumNumberOfBullet);
        // maskのテクスチャミスってると若干残る場合があるからマイナスにする
        if (maskvaule <= 0)
        {
            maskvaule = -0.01f;
        }
        m_imageMaterial.SetFloat("_Mask", maskvaule);
    }


}
