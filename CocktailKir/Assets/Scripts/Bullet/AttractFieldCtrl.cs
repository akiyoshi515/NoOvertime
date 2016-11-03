using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using RegistedList = System.Collections.Generic.Queue<GuestCtrl>;

[RequireComponent(typeof(SphereCollider))]
public class AttractFieldCtrl : MonoBehaviour
{
    [SerializeField]
    private float m_radius = 1.0f;

    [SerializeField]
    private float m_time = 0.0f;
    public float time 
    {
        get { return m_time; }
        set { m_time = value; }
    }

    private RegistedList m_registedList = new RegistedList();

    // Use this for initialization
    void Start()
    {
        this.GetComponent<SphereCollider>().radius = m_radius;
    }

    // Update is called once per frame
    void Update()
    {
        m_time -= Time.deltaTime;
        if (m_time <= 0.0f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        foreach (GuestCtrl unit in m_registedList)
        {
            unit.SendDestroyedAttractField();
        }
        m_registedList.Clear();
    }

    public void RegistObject(GuestCtrl obj)
    {
        m_registedList.Enqueue(obj);
    }


}

