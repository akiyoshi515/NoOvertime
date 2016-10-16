using UnityEngine;
using System.Collections;

public class TestEfGenocide : MonoBehaviour
{
    private float m_lifeTime = 10.0f;
    private ParticleSystem m_particle = null;

    // Use this for initialization
    void Start()
    {
        m_particle = this.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        m_lifeTime -= Time.deltaTime;
        if (m_lifeTime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
