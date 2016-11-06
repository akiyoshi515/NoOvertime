using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeshUIMagazine 
    :
    MonoBehaviour
{
    ///
    /// <summary>   true if this object is reloading.   </summary>
    ///

    [SerializeField]
    bool m_isReloading = false;
    public bool IsReloading
    {
        get { return m_isReloading; }
        set { m_isReloading = value; }
    }

    ///
    /// <summary>   true if this object is petit fever. </summary>
    ///

    [SerializeField]
    bool m_isPetitFever = false;
    public bool IsPetitFever
    {
        get { return m_isPetitFever; }
        set { m_isPetitFever = value; }
    }
    ///
    /// <summary>   残り弾数.   </summary>
    ///
    [SerializeField,Header("残り弾数")]
    uint m_numberOfBullet;
    public uint NumberOfBullet
    {
        get { return m_numberOfBullet; }
        set { m_numberOfBullet = value; }
    }

    ///
    /// <summary>   Number of 3 way bullets.    </summary>
    ///

    [SerializeField, Header("残り弾数(3way)")]
    uint m_numberOf3WayBullet;
    public uint NumberOf3WayBullet
    {
        get { return m_numberOf3WayBullet; }
        set { m_numberOf3WayBullet = value; }
    }

    ///
    /// <summary>   Number of charm bullets.    </summary>
    ///

    [SerializeField, Header("残り弾数(チャームアップ)")]
    uint m_numberOfCharmBullet;
    public uint NumberOfCharmBullet
    {
        get { return m_numberOfCharmBullet; }
        set { m_numberOfCharmBullet = value; }
    }

    ///
    /// <summary>   3wayチャーム弾の残弾   </summary>
    ///

    uint m_numberOf3wayCharmBullet;

    ///
    /// <summary>   3wayチャーム弾のアクセサ.  </summary>
    ///
    /// <value> The total number of 3way charm bullet.  </value>
    ///

    public uint NumberOf3wayCharmBullet
    {
        get { return m_numberOf3wayCharmBullet; }
        set { m_numberOf3wayCharmBullet = value; }
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
    [SerializeField,Header("ゲージのバー")]
    MeshRenderer m_gaugeQuadMeshRenderer;

    ///
    /// <summary>   通常弾を表現しているメッシュレンダラー.    </summary>
    ///

    [SerializeField,Header("通常弾のメッシュたち")]
    MeshRenderer[] m_normalBullletQuadMeshRenderer;
    

    ///
    /// <summary>   3way弾を表現しているメッシュレンダラー.    </summary>
    ///

    [SerializeField, Header("3way弾のメッシュたち")]
    MeshRenderer[] m_3wayBulletQuadMeshRenderer;

    ///
    /// <summary>   チャームアップアイテム取得時に通常弾にひっつくアイコンのメッシュレンダラー.    </summary>
    ///

    [SerializeField, Header("通常弾にひっつくハートのメッシュたち")]
    MeshRenderer[] m_normalBullletHeartIconQuadMeshRenderer;
    
    ///
    /// <summary>   チャームアップアイテム取得時に3way弾にひっつくアイコンのメッシュレンダラー.    </summary>
    ///

    [SerializeField, Header("3way弾にひっつくハートのメッシュたち")]
    MeshRenderer[] m_3wayBulletHeartIconQuadMeshRenderer;



    [SerializeField, Header("Reload Logo")]
    MeshRenderer m_reloadLogoMeshRenderer;


    ///
    /// <summary>   チャームアップアイテムのアイコンのメッシュレンダラー(保険).   </summary>
    ///

    MeshRenderer m_charmUpItemIconMeshRenderer;

    ///
    /// <summary>   3wayアイテムのアイコンのメッシュレンダラー(保険).   </summary>
    ///

    MeshRenderer m_3wayBulletItemIconMeshRenderer;

    ///
    /// <summary>   無限弾アイテムのアイコンのメッシュレンダラー(保険).   </summary>
    ///

    MeshRenderer m_UnlimitedBulletItemIconMeshRenderer;

    ///
    /// <summary>   フィーバー時の後光.  </summary>
    ///
    [SerializeField]
    MeshRenderer m_feverBackLightMesh;

    ///
    /// <summary>   シェーダー入ってるマテリアル. </summary>
    ///

    Material m_reloadGaugeMaterial;

    ///
    /// <summary>   シェーダー入ってるマテリアル. </summary>
    ///

    Material m_feverGaugeMaterial;
    
    ///
    /// <summary>   The reload time rate.   </summary>
    ///
    [SerializeField]
    float m_reloadTimeRate;
    public float ReloadTimeRate
    {
        set { m_reloadTimeRate = value; }
    }

    ///
    /// <summary>   The fever time rate.    </summary>
    ///

    [SerializeField]
    float m_feverTimeRate;
    public float FeverTimeRate
    {
        get { return m_feverTimeRate; }
        set { m_feverTimeRate = value; }
    }

    [SerializeField]
    private LauncherMagazine m_magazine = null;

    // 後で破棄
    enum TestReloadState
    {
        NONE,
        RELOAD
    }

    TestReloadState m_testState;

    // Use this for initialization
    void Start ()
    {
        if (m_gaugeQuadMeshRenderer == null)
        {
            m_gaugeQuadMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        }
        m_reloadGaugeMaterial = m_gaugeQuadMeshRenderer.material;
        float maskvaule = (float)((float)NumberOfBullet / (float)m_maximumNumberOfBullet);
        // maskのテクスチャミスってると若干残る場合があるからマイナスにする
        if (maskvaule <= 0)
        {
            maskvaule = -0.01f;
        }
#if DEBUG
        if (m_reloadGaugeMaterial.shader.name == "Custom/GaugeSpriteShader")
#endif
        {
            AkiVACO.XLogger.LogWarning("shader間違ってるよ");
            m_reloadGaugeMaterial.SetFloat("_Mask", maskvaule);
        }
    }

    // Update is called once per frame
    void Update()
    {

//         // test code
// 
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             {
//                 if (0 < m_numberOf3wayCharmBullet)
//                 {
//                     m_numberOf3wayCharmBullet--;
//                 }
//                 else if (0 < m_numberOf3WayBullet)
//                 {
//                     m_numberOf3WayBullet--;
//                 }
//                 else if (0 < m_numberOfCharmBullet)
//                 {
//                     m_numberOfCharmBullet--;
//                 }
//                 else if (0 < m_numberOfBullet)
//                 {
//                     NumberOfBullet--;
//                 }
//             }
//         }
// 
//         switch (m_testState)
//         {
//             case TestReloadState.NONE:
// 
//                 if (Input.GetKeyDown(KeyCode.R))
//                 {
//                     m_testState = TestReloadState.RELOAD;
//                     m_isReloading = true;
//                 }
//                 break;
//             case TestReloadState.RELOAD:
//                 if (m_reloadTimeRate < 1)
//                 {
//                     m_reloadTimeRate += Time.deltaTime * 0.5f;
// 
//                 }
//                 else 
//                 {
//                     m_reloadTimeRate = 0f;
//                     m_numberOfBullet = 6;
//                     m_testState = TestReloadState.NONE;
//                     m_isReloading = false;
//                 }
//                 break;
//             default:
//                 break;
// 
//         }
// 
//         
//         if (Input.GetKeyDown(KeyCode.I))
//         {
//             m_numberOf3WayBullet = 3;
//         }
//         if (Input.GetKeyDown(KeyCode.O))
//         {
//             m_numberOfCharmBullet = 3;
//         }
// 
//         if (Input.GetKeyDown(KeyCode.U))
//         {
//             m_isPetitFever = !m_isPetitFever;
//         }
        // test code end

        UpdateMagazineState();

        // ゲージ制御
        ControllReloadGauge();

        // 弾アイコン制御
        ControllBulletIcon();
        
        ControlllFeverEffect();
        if (m_isReloading)
        {
            m_reloadLogoMeshRenderer.enabled = true;
        } 
        else
        {
            m_reloadLogoMeshRenderer.enabled = false;
        }

    }

    void ControlllFeverEffect()
    {
        if (m_isPetitFever)
        {
            m_feverBackLightMesh.enabled = true;
        }
        else
        {
            m_feverBackLightMesh.enabled = false;
        }
    }


    void ControllReloadGauge()
    {

        float maskvaule = (float)(m_reloadTimeRate);

        if (m_isReloading)
        {
            if (maskvaule <= 0)
            {
                maskvaule = -0.01f;
            }
            m_reloadGaugeMaterial.SetFloat("_Mask", maskvaule);
        }
        else
        {
            m_reloadGaugeMaterial.SetFloat("_Mask", maskvaule);
        }
    }

    void ControllItemIcon()
    {

        // チャームアップ残弾1以上
        if (0 < m_numberOfCharmBullet)
        {

        }
        
        // 3way残弾1以上
        if (0 < NumberOf3WayBullet)
        {

        }

        // プチフィーバー中
        if (m_isPetitFever)
        {

        }

    }

    void ControllBulletIcon()
    {     // 3wayチャーム弾が放てるか
        // アクセサからの情報によってはもういらんかも
        if (0 < m_numberOfCharmBullet && 0 < m_numberOf3WayBullet)
        {
            uint temp = m_numberOf3WayBullet % m_numberOfCharmBullet;
            if (m_numberOf3WayBullet / m_numberOfCharmBullet < 1)
            {

                m_numberOf3wayCharmBullet = temp;
            }
            else
            {
                m_numberOf3wayCharmBullet = m_numberOfCharmBullet - temp;

            }
            m_numberOfCharmBullet -= m_numberOf3wayCharmBullet;
            m_numberOf3WayBullet -= m_numberOf3wayCharmBullet;
        }


        int i = 0;


        for (; i < 6; i++)
        {
            m_3wayBulletHeartIconQuadMeshRenderer[(int)(i * 0.5)].enabled = false;
            m_3wayBulletQuadMeshRenderer[(int)(i * 0.5)].enabled = false;
            m_normalBullletHeartIconQuadMeshRenderer[i].enabled = false;
            m_normalBullletQuadMeshRenderer[i].enabled = false;
        }

        // 表示可能なアイコンの洗い出し
        uint _3cb = m_numberOf3wayCharmBullet;
        uint _3b = m_numberOf3WayBullet;
        uint _cb = m_numberOfCharmBullet;
        uint _b = m_numberOfBullet;

        // 表示するアイコンを決める
        for (i = 0; i < 6;)
        {
            if (0 < _3cb)
            {
                m_3wayBulletHeartIconQuadMeshRenderer[(int)(i * 0.5)].enabled = true;
                m_3wayBulletQuadMeshRenderer[(int)(i * 0.5)].enabled = true;
                i += 2;
                _3cb--;
            }
            else if (0 < _3b)
            {
                m_3wayBulletQuadMeshRenderer[(int)(i * 0.5)].enabled = true;
                i += 2;
                _3b--;
            }

            else if (0 < _cb)
            {
                m_normalBullletHeartIconQuadMeshRenderer[i].enabled = true;
                m_normalBullletQuadMeshRenderer[i].enabled = true;
                i++;
                _cb--;
            }
            else if (0 < _b)
            {
                m_normalBullletQuadMeshRenderer[i].enabled = true;
                i++;
                _b--;
            }
            else
            {

                i++;
            }
        }
    }

    private void UpdateMagazineState()
    {
        m_numberOfBullet = (uint)m_magazine.bulletNum;
        m_numberOf3wayCharmBullet = (uint)m_magazine.bonus3WayBullet;
        m_numberOfCharmBullet = (uint)m_magazine.bonusCharmBullet;

        m_isPetitFever = m_magazine.isUnlimitedBullet;
        m_isReloading = m_magazine.isReloading;
    }

}
