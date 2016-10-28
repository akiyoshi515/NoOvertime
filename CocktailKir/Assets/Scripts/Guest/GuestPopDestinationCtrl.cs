using UnityEngine;
using System.Collections;

using AkiVACO.XValue;

public class GuestPopDestinationCtrl : MonoBehaviour
{
    [System.Serializable]
    public class GuestParam
    {
        public int m_capacity = 0;
        public int m_num = 0;

    }

    [System.Serializable]
    public class StrategySlot
    {
        public IGuestPopStrategy m_strategy = null;
        public GuestPopStrategy.StrategyType m_strategyType = GuestPopStrategy.StrategyType.Wait;
        public float m_time = 1.0f;
        public int[] m_values = null;
        public float[] m_fvalues = null;

        public StrategySlot()
        {
            m_strategy = GuestPopStrategy.CreatePopStrategy(m_strategyType);
        }
    }

    public GameObject m_popPointer = null;

    public float m_radius = 10.0f;

    public GuestParam[] m_param = null;

    public StrategySlot[] m_slotStrategy = null;

    public bool m_isLoop = false;

    private int m_slotIndex = 0;
    private float m_slotTime = 0.0f;

    public GuestPopPointerCtrl[] pointTable
    {
        get;
        protected set;
    }

    public int baseSumCost
    {
        get;
        protected set;
    }

    // Use this for initialization
    void Start()
    {
        m_slotIndex = 0;
        pointTable = this.GetComponentsInChildren<GuestPopPointerCtrl>();
        baseSumCost = 0;
        foreach (GuestPopPointerCtrl unit in pointTable)
        {
            baseSumCost += unit.m_cost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StrategySlot slot = m_slotStrategy[m_slotIndex];
        slot.m_strategy.UpdatePopStrategy(this, slot.m_values, slot.m_fvalues);

        m_slotTime -= Time.deltaTime;
        if (m_slotTime <= 0.0f)
        {
            NextSlot();
        }
    }

    public Vector3 GetDestination()
    { 
        // HACK Random
        float r = Random.Range(0.0f, 360.0f);
        Vector3 offset =  Quaternion.AngleAxis(r, this.transform.up) * this.transform.right;
        return this.transform.position + offset;
    }

    public void NextSlot(int jumpValue = 1)
    {
        m_slotIndex += jumpValue;
        if (m_slotIndex >= m_slotStrategy.Length)
        {
            if (m_isLoop)
            {
                while (m_slotIndex >= m_slotStrategy.Length)
                {
                    m_slotIndex -= m_slotStrategy.Length;
                }
            }
            else
            {
                m_slotIndex = m_slotStrategy.Length -1;
            }
        }

        m_slotTime = m_slotStrategy[m_slotIndex].m_time;
    }

}
