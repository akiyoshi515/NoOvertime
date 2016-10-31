using UnityEngine;
using System.Collections;

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



}

