using UnityEngine;
using System.Collections;

using AkiVACO.XValue;

public class GuestPopDestinationCtrl : MonoBehaviour
{
    [System.Serializable]
    public class GuestParam
    {
        public int m_capacity = 0;
        public int m_priority = 0;
        public int m_num = 0;

        public bool IsCapacity()
        {
            return (m_num > 0);
        }
    }

    [System.Serializable]
    public class StrategySlot
    {
        public IGuestPopStrategy m_strategy = null;
        public GuestPopStrategy.PopStrategyType m_strategyType = GuestPopStrategy.PopStrategyType.Wait;
        public float m_time = 1.0f;
        public int[] m_values = null;
        public float[] m_fvalues = null;

        public void Initialize()
        {
            m_strategy = GuestPopStrategy.CreatePopStrategy(m_strategyType);
        }
    }

    [SerializeField]
    private GameObject m_normalGuest = null;

    [SerializeField]
    private GameObject m_gentleGuest = null;

    [SerializeField]
    private GameObject m_impatientGuest = null;

    [SerializeField]
    private GameObject m_stayBehindGuest = null;

    public GameObject m_popPointer = null;

    public float m_radius = 10.0f;

    public GuestParam[] m_param = null;

    public StrategySlot[] m_slotStrategy = null;

    public bool m_isLoop = false;

    private IGuestTypeStrategy m_typeStrategy = null;
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

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        m_slotIndex = 0;
        pointTable = this.GetComponentsInChildren<GuestPopPointerCtrl>();
        baseSumCost = 0;
        foreach (GuestPopPointerCtrl unit in pointTable)
        {
            baseSumCost += unit.m_priority;
        }

        foreach (GuestParam unit in m_param)
        {
            unit.m_num = unit.m_capacity;
        }

        foreach (StrategySlot slot in m_slotStrategy)
        {
            slot.Initialize();
        }

        m_typeStrategy = new GuestPopStrategyInternal.GuestTypeStrategy_Random();
    }

    // Update is called once per frame
    void Update()
    {
        StrategySlot slot = m_slotStrategy[m_slotIndex];
        slot.m_strategy.UpdatePopStrategy(this, m_typeStrategy, slot.m_values, slot.m_fvalues);

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
        return this.transform.position + offset * (m_radius * Random.Range(0.0f, 1.0f) * 0.50f);
    }

    public void SendPopGuest(GuestType type, GuestPopPointerCtrl popPoint, Vector3 destination, GuestPopPointerCtrl goOutDestination)
    {
        if (ValidPopGuest(type))
        {
            switch (type)
            {
                case GuestType.Standard:
                    popPoint.SendPopGuest(m_normalGuest, destination, goOutDestination.transform.position);
                    break;
                case GuestType.Gentle:
                    popPoint.SendPopGuest(m_gentleGuest, destination, goOutDestination.transform.position);
                    break;
                case GuestType.Impatient:
                    popPoint.SendPopGuest(m_impatientGuest, destination, goOutDestination.transform.position);
                    break;
                case GuestType.StayBehind:
                    popPoint.SendPopGuest(m_stayBehindGuest, destination, goOutDestination.transform.position);
                    break;
            } 
        }
    }

    public bool ValidPopGuest(GuestType type)
    {
        if (type == GuestType.InvalidType)
        {
            return false;
        }

        int idx = (int)type;
        if (!m_param[idx].IsCapacity())
        {
            return false;
        }
        --(m_param[idx].m_num);

        return true;
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
