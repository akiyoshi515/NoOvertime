using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MgazineUI : MonoBehaviour {

    ///
    /// <summary>   残り弾数.   </summary>
    ///
    [SerializeField,Header("残り弾数")]
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

    [SerializeField,Header("最大弾数")]
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
    /// <summary>   ゲージとして使うメッシュレンダラー.  </summary>
    ///
    
    MeshRenderer m_meshRenderer;

    ///
    /// <summary>   シェーダー入ってるマテリアル. </summary>
    ///

    Material m_material;


    ///
    /// <summary>   回転の指標にするカメラ.  </summary>
    ///

    [SerializeField, Header("回転指標にするカメラ")]
    Camera m_targetCamera;
    public UnityEngine.Camera TargetCamera
    {
        get
        {
            return m_targetCamera;
        }
        set
        {
            m_targetCamera = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (m_meshRenderer == null)
        {
            m_meshRenderer = gameObject.GetComponent<MeshRenderer>();
        }
        m_material = m_meshRenderer.material;
        float maskvaule = (float)((float)NumberOfBullet / (float)m_maximumNumberOfBullet);
        // maskのテクスチャミスってると若干残る場合があるからマイナスにする
        if (maskvaule <= 0)
        {
            maskvaule = -0.01f;
        }
#if DEBUG
        if (m_material.shader.name == "Custom/GaugeSpriteShader")
#endif
        {
            AkiVACO.XLogger.LogWarning("shader間違ってるよ");
            m_material.SetFloat("_Mask", maskvaule);
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
        m_material.SetFloat("_Mask", maskvaule);
        transform.LookAt(transform.position - m_targetCamera.transform.rotation * Vector3.back, m_targetCamera.transform.rotation * Vector3.up);
    }


}
