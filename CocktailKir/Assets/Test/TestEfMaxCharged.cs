using UnityEngine;
using System.Collections;

public class TestEfMaxCharged : MonoBehaviour, IEffect
{
    [SerializeField]
    private ParticleSystem m_particleCharge = null;

    [SerializeField]
    private ParticleSystem m_particleBarn = null;

    // Use this for initialization
    void Awake()
    {
    }

    public void WakeupEffect()
    {
        m_particleCharge.Play();
    }

    public void SleepEffect()
    {
        if (m_particleCharge.isPlaying)
        {
            m_particleCharge.Stop();
            m_particleBarn.Play();
        }
    }

}
