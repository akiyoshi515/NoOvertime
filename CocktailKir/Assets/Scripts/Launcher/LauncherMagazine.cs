using UnityEngine;
using System.Collections;

using AkiVACO;
using UnityEngine.Events;

public class LauncherMagazine : MonoBehaviour
{
    /// <summary>
    /// リロードにかかる時間
    /// </summary>
    [SerializeField]
    private float m_reloadTime = 1.0f;

    /// <summary>
    /// 残弾数
    /// </summary>
    [SerializeField]
    private int m_bulletNum = 0;
    public int bulletNum
    {
        get { return m_bulletNum; }
        protected set { m_bulletNum = value; }
    }

    /// <summary>
    /// 最大弾数
    /// </summary>
    [SerializeField]
    private int m_capacity = 0;
    public int capacity
    {
        get { return m_capacity; }
        protected set { m_capacity = value; }
    }

    /// <summary>
    /// リロード中か？
    /// </summary>
    public bool isReloading
    {
        get;
        protected set;
    }

    /// <summary>
    /// 残弾数が最大か？
    /// </summary>
    public bool isMax
    {
        get { return (bulletNum == capacity); }
    }

    /// <summary>
    /// 射撃可能になってるか？
    /// </summary>
    public bool isEnabledShot
    {
        get { return ((bulletNum > 0) && (!isReloading)) || (isUnlimitedBullet); }
    }

    /// <summary>
    /// 弾数無制限か？
    /// </summary>
    public bool isUnlimitedBullet
    {
        get;
        protected set;
    }

    private XUnityEvent m_callbackReloaded = new XUnityEvent();
    private float m_reloadRemainTime = 0.0f;
    private float m_unlimitedBulletRemainTime = 0.0f;
    private float m_unlimitedBulletStartTime = 1.0f;

    /// <summary>
    /// リロードの残り時間
    /// </summary>
    public float reloadRemainTime
    {
        get { return m_reloadRemainTime; }
    }

    /// <summary>
    /// リロードの進捗率（0.0 -> 1.0）
    /// </summary>
    public float reloadTimeRate
    {
        get { return (m_reloadTime - m_reloadRemainTime) * (1.0f / m_reloadTime); }
    }

    /// <summary>
    /// 弾数無制限の残り時間
    /// </summary>
    public float unlimitedBulletRemainTime
    {
        get { return m_unlimitedBulletRemainTime; }
    }

    /// <summary>
    /// 弾数無制限の残り時間（割合）（1.0 -> 0.0）
    /// </summary>
    public float unlimitedBulletTimeRate
    {
        get { return m_unlimitedBulletRemainTime * (1.0f / m_unlimitedBulletStartTime); }
    }

    private int m_bonus3WayBullet = 0;

    /// <summary>
    /// 3Wayの残り弾数
    /// </summary>
    public int bonus3WayBullet
    {
        get { return m_bonus3WayBullet; }
    }

    private int m_bonusCharmBullet = 0;

    /// <summary>
    /// チャームアップの残り弾数
    /// </summary>
    public int bonusCharmBullet
    {
        get { return m_bonusCharmBullet; }
    }

    void Start()
    {
        m_bulletNum = m_capacity;
        m_reloadRemainTime = 0.0f;
        m_unlimitedBulletRemainTime = 0.0f;
    }

    void Update()
    {
        UpdateReload();
        UpdateUnlimitedBullet();
    }

    /// <summary>
    /// リロード（更新関数）
    /// </summary>
    private void UpdateReload()
    {
        if (!isReloading)
        {
            return;
        }

        m_reloadRemainTime -= Time.deltaTime;
        if (m_reloadRemainTime <= 0.0f)
        {
            XLogger.Log("Magazine Finish Reload");
            m_reloadRemainTime = 0.0f;
            m_bulletNum = m_capacity;
            isReloading = false;
            m_callbackReloaded.Invoke();
        }
    }

    /// <summary>
    /// 弾数無制限（更新関数）
    /// </summary>
    private void UpdateUnlimitedBullet()
    {
        if (!isUnlimitedBullet)
        {
            return;
        }

        m_unlimitedBulletRemainTime -= Time.deltaTime;
        if (m_unlimitedBulletRemainTime <= 0.0f)
        {
            XLogger.Log("Magazine Finish UnlimitedBullet");
            m_unlimitedBulletRemainTime = 0.0f;
            isUnlimitedBullet = false;
        }
    }

    /// <summary>
    /// 残弾数の追加
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void AddBullet(int val)
    {
        XLogger.LogValidObject(val <= 0, "Invalid Argument MagazineUnit AddBullet: " + val.ToString());
        bulletNum += val;
        if (bulletNum >= capacity)
        {
            bulletNum = capacity;
        }
    }

    /// <summary>
    /// 残弾数の減算
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void SubBullet(int val)
    {
        XLogger.LogValidObject(val <= 0, "Invalid Argument MagazineUnit SubBullet: " + val.ToString());
        if (!isUnlimitedBullet)
        {
            bulletNum -= val;
        }

        if (bulletNum < 0)
        {
            bulletNum = 0;
            XLogger.LogError("Bullet Overflow");
        }
        XLogger.Log("Magazine SubBullet: " + val.ToString() + "  " + bulletNum.ToString() + "/" + capacity.ToString());
    }

    /// <summary>
    /// 最大値の追加
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void AddCapacity(int val)
    {
        XLogger.LogValidObject(val <= 0, "Invalid Argument MagazineUnit AddCapacity: " + val.ToString());
        capacity += val;
        XLogger.Log("Magazine AddCapacity: " + val.ToString() + "  " + bulletNum.ToString() + "/" + capacity.ToString());
    }

    /// <summary>
    /// チャーム増加弾の使用
    /// </summary>
    /// <returns>チャーム増加値</returns>
    public int UseBonusCharmBullet()
    {
        if (m_bonusCharmBullet > 0)
        {
            --m_bonusCharmBullet;
            if (m_bonusCharmBullet <= 0)
            {
                XLogger.Log("Magazine CharmBullet empty");
            }
            else
            {
                XLogger.Log("Magazine CharmBullet: " + m_bonusCharmBullet.ToString() + "/6");
            }
            return 1;   // TODO
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// チャーム増加弾のリロード
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void ReloadBonusCharmBullet(int val)
    {
        m_bonusCharmBullet = val;
        XLogger.Log("Magazine Reload CharmBullet: " + val.ToString());
    }

    /// <summary>
    /// 3Way弾の使用
    /// </summary>
    /// <returns>使用したか？</returns>
    public bool UseBonus3WayBullet()
    {
        if (m_bonus3WayBullet > 0)
        {
            --m_bonus3WayBullet;
            if (m_bonus3WayBullet <= 0)
            {
                XLogger.Log("Magazine 3WayBullet empty");
            }
            else
            {
                XLogger.Log("Magazine 3WayBullet: " + m_bonus3WayBullet.ToString() + "/3");
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 3WAY弾のリロード
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void ReloadBonus3WayBullet(int val)
    {
        m_bonus3WayBullet = val;
        XLogger.Log("Magazine Reload 3WayBullet: " + val.ToString());
    }

    public void StartReload(UnityAction callback)
    {
        m_callbackReloaded.RemoveAllListeners();
        isReloading = true;
        m_reloadRemainTime = m_reloadTime;
        m_callbackReloaded.AddListener(callback);
        XLogger.Log("Magazine Reload");
    }

    public void ForceStopReload()
    {
        isReloading = false;
        m_reloadRemainTime = 0.0f;
        m_callbackReloaded.Invoke();
        XLogger.Log("Magazine ForceStop Reload");
    }

    public void StartUnlimitedBullet(float time)
    {
        m_unlimitedBulletRemainTime = time;
        m_unlimitedBulletStartTime = time;
        isUnlimitedBullet = true;
        XLogger.Log("Magazine Start UnlimitedBullet");
    }

    public void StopUnlimitedBullet()
    {
        m_unlimitedBulletRemainTime = 0.0f;
        isUnlimitedBullet = false;
        XLogger.Log("Magazine Stop UnlimitedBullet");
    }

}
