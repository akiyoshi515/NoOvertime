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
    /// 弾数
    /// </summary>
    [SerializeField]
    private int m_balletNum = 0;
    public int balletNum
    {
        get { return m_balletNum; }
        protected set { m_balletNum = value; }
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
        get { return (balletNum == capacity); }
    }

    /// <summary>
    /// 射撃可能になってるか？
    /// </summary>
    public bool isEnabledShot
    {
        get { return ((balletNum > 0) && (!isReloading)) || (isUnlimitedBallet); }
    }

    /// <summary>
    /// 弾数無制限か？
    /// </summary>
    public bool isUnlimitedBallet
    {
        get;
        protected set;
    }

    private XUnityEvent m_callbackReloaded = new XUnityEvent();
    private float m_reloadRemainTime = 0.0f;
    private float m_unlimitedBalletRemainTime = 0.0f;

    void Start()
    {
        m_balletNum = m_capacity;
        m_reloadRemainTime = 0.0f;
        m_unlimitedBalletRemainTime = 0.0f;
    }

    void Update()
    {
        UpdateReload();
        UpdateUnlimitedBallet();
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
            m_balletNum = m_capacity;
            isReloading = false;
            m_callbackReloaded.Invoke();
        }
    }

    /// <summary>
    /// 弾数無制限（更新関数）
    /// </summary>
    private void UpdateUnlimitedBallet()
    {
        if (!isUnlimitedBallet)
        {
            return;
        }

        m_unlimitedBalletRemainTime -= Time.deltaTime;
        if (m_unlimitedBalletRemainTime <= 0.0f)
        {
            XLogger.Log("Magazine Finish UnlimitedBallet");
            m_unlimitedBalletRemainTime = 0.0f;
            isUnlimitedBallet = false;
        }
    }

    /// <summary>
    /// 残弾数の追加
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void AddBallet(int val)
    {
        XLogger.LogValidObject(val <= 0, "Invalid Argument MagazineUnit AddBallet: " + val.ToString());
        balletNum += val;
        if (balletNum >= capacity)
        {
            balletNum = capacity;
        }
    }

    /// <summary>
    /// 残弾数の減算
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void SubBallet(int val)
    {
        XLogger.LogValidObject(val <= 0, "Invalid Argument MagazineUnit SubBallet: " + val.ToString());
        if (!isUnlimitedBallet)
        {
            balletNum -= val;
        }

        if (balletNum < 0)
        {
            balletNum = 0;
            XLogger.LogError("Ballet Overflow");
        }
        XLogger.Log("Magazine SubBallet: " + val.ToString() + "  " + balletNum.ToString() + "/" + capacity.ToString());
    }

    /// <summary>
    /// 最大値の追加
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void AddCapacity(int val)
    {
        XLogger.LogValidObject(val <= 0, "Invalid Argument MagazineUnit AddCapacity: " + val.ToString());
        capacity += val;
        XLogger.Log("Magazine AddCapacity: " + val.ToString() + "  " + balletNum.ToString() + "/" + capacity.ToString());
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

    public void StartUnlimitedBallet(float time)
    {
        m_unlimitedBalletRemainTime = time;
        isUnlimitedBallet = true;
        XLogger.Log("Magazine Start UnlimitedBallet");
    }

    public void StopUnlimitedBallet()
    {
        m_unlimitedBalletRemainTime = 0.0f;
        isUnlimitedBallet = false;
        XLogger.Log("Magazine Stop UnlimitedBallet");
    }

}
