using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeshUIMagazine 
    :
    MonoBehaviour
{
    bool m_isReloading;
    public bool Reloading
    {
        get { return m_isReloading; }
        set { m_isReloading = value; }
    }
    ///
    /// <summary>   残り弾数.   </summary>
    ///
    [SerializeField,Header("残り弾数")]
    uint m_numberOfBullet;


    [SerializeField, Header("残り弾数(3way)")]
    uint m_numberOf3WayBullet;


    [SerializeField, Header("残り弾数(チャームアップ)")]
    uint m_numberOfCharmBullet;

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
            for (int i = 0; i < m_normalBullletQuadMeshRenderer.Length; i++)
            {
                if ( i < m_numberOfBullet )
                {
                    m_normalBullletQuadMeshRenderer[i].enabled = true;
                }
            }
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
    /// <summary>   ゲージ本体として使うメッシュレンダラー.  </summary>
    ///
    [SerializeField]
    MeshRenderer m_gaugeQuadMeshRenderer;

    ///
    /// <summary>   通常弾を表現しているメッシュレンダラー.    </summary>
    ///

    [SerializeField]
    MeshRenderer[] m_normalBullletQuadMeshRenderer;



    ///
    /// <summary>   3way弾を表現しているメッシュレンダラー.    </summary>
    ///

    [SerializeField]
    MeshRenderer[] m_3wayBulletQuadMeshRenderer;


    ///
    /// <summary>   シェーダー入ってるマテリアル. </summary>
    ///

    Material m_material;

    ///
    /// <summary>   The reload time rate.   </summary>
    ///

    float m_reloadTimeRate;
    public float ReloadTimeRate
    {
        set { m_reloadTimeRate = value; }
    }
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
        if (m_gaugeQuadMeshRenderer == null)
        {
            m_gaugeQuadMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        }
        m_material = m_gaugeQuadMeshRenderer.material;
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
    void Update()
    {
        float maskvaule = (float)(m_reloadTimeRate);

        if (m_isReloading)
        {
            if (maskvaule <= 0)
            {
                maskvaule = -0.01f;
            }
            m_material.SetFloat("_Mask", maskvaule);
        }
        else
        {
            m_material.SetFloat("_Mask", maskvaule);
        }
        transform.LookAt(transform.position - m_targetCamera.transform.rotation * Vector3.back, m_targetCamera.transform.rotation * Vector3.up);


        for (int i = 0; i < m_normalBullletQuadMeshRenderer.Length; i++)
        {
            if (i < m_numberOfBullet)
            {
                m_normalBullletQuadMeshRenderer[i].enabled = true;
            }
            else
            {
                m_normalBullletQuadMeshRenderer[i].enabled = false;
            }
        }
    }

}
