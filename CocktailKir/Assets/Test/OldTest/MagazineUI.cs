using UnityEngine;
using System.Collections;

using AkiVACO;

public class MagazineUI : MonoBehaviour
{
    /// <summary>
    /// リロード時間
    /// </summary>
    [SerializeField]
    private float m_reloadTime = 1.0f;

    /// <summary>
    /// 弾数
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
    [SerializeField]
    private bool m_isReloading = false;
    public bool isReloading
    {
        get { return m_isReloading; }
        protected set { m_isReloading = value; }
    }

    ///
    /// <summary>   リロード時間の進捗率.   </summary>
    ///

    float m_reloadTimeRate;

    
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
        get { return (bulletNum > 0) && (!isReloading); }
    }

    private float m_time = 0.0f;

    void Awake()
    {
        XLogger.LogError("Illegal Awake! TestCS", gameObject);
    }

    void Start()
    {
        m_bulletNum = m_capacity;
        m_time = 0.0f;
    }

    /// <summary>
    /// リロード（更新関数）
    /// </summary>
    /// <returns>最大まで回復したか？</returns>
    void Update()
    {
        if (!isReloading)
        {
            return;
        }

        if (!isMax)
        {
            m_time += Time.deltaTime;
            if (m_time >= m_reloadTime)
            {
                m_time -= m_reloadTime;
                this.AddBullet(1);
            }
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
        XLogger.Log("Magazine AddBullet: " + val.ToString() + "  " + bulletNum.ToString() + "/" + capacity.ToString());
    }

    /// <summary>
    /// 残弾数の減算
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void SubBullet(int val)
    {
        XLogger.LogValidObject(val <= 0, "Invalid Argument MagazineUnit SubBullet: " + val.ToString());
        bulletNum -= val;
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

    public void StartReload()
    {
        isReloading = true;
    }

    public void StopReload()
    {
        isReloading = false;
        m_time = 0.0f;
    }

}
