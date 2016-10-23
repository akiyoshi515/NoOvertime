using UnityEngine;
using System.Collections;

using AkiVACO;

public class TestMagazine : MonoBehaviour
{
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
    [SerializeField]
    private bool m_isReloading = false;
    public bool isReloading
    {
        get { return m_isReloading; }
        protected set { m_isReloading = value; }
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
        get { return (balletNum > 0) && (!isReloading); }
    }

    private float m_time = 0.0f;

    void Start()
    {
        m_balletNum = m_capacity;
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
                this.AddBallet(1);
            }
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
        XLogger.Log("Magazine AddBallet: " + val.ToString() + "  " + balletNum.ToString() + "/" + capacity.ToString());
    }

    /// <summary>
    /// 残弾数の減算
    /// </summary>
    /// <param name="val">追加量：(value > 0)</param>
    public void SubBallet(int val)
    {
        XLogger.LogValidObject(val <= 0, "Invalid Argument MagazineUnit SubBallet: " + val.ToString());
        balletNum -= val;
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
