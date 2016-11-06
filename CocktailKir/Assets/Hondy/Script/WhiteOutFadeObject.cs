using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WhiteOutFadeObject : TestBaseFadeObject
{

    [SerializeField]
    Color m_fadeInColor;

    [SerializeField]
    Color m_fadeOutColor;

    [SerializeField]
    Image m_image;

    // Use this for initialization
    void Start()
    {

    }

    void FadeIn()
    {
        m_image.color = m_fadeInColor * m_deltaTime / m_fadeTime;
    }

    bool IsFadeIn()
    {
        if (m_fadeTime < m_deltaTime)
        {
            m_deltaTime = 0;
            return true;
        }
        return false;
    }
    
    void FadeOut()
    {
        m_image.color = m_fadeInColor - m_fadeOutColor * m_deltaTime / m_fadeTime;
    }

    public virtual bool IsFadeOut()
    {
        if (m_fadeTime < m_deltaTime)
        {
            m_deltaTime = 0;
            return true;
        }
        return false;
    }
}
