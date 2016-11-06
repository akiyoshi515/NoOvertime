using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
public class TestBaseFadeObject : MonoBehaviour {

    enum FadeState
    {
        WAIT,
        FADE_IN,
        END_FADE_IN,
        FADE_OUT,
        END_FADE_OUT,
        TRANSITION_SCENE
    }

    [SerializeField]
    protected float m_fadeTime;

    protected float m_deltaTime;

    FadeState m_fadeState;
    [SerializeField]
    public string m_transitionSceneName;

    bool m_isStartFadeIn;
    public bool StartFadeIn
    {
        get { return m_isStartFadeIn; }
        set { m_isStartFadeIn = value; }
    }

    

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update ()
    {
        UpdateState();
    }

    virtual public void UpdateState()
    {
        m_deltaTime += Time.deltaTime;
        switch (m_fadeState)
        {
            case FadeState.WAIT:
                if (m_isStartFadeIn)
                {
                    m_fadeState = FadeState.FADE_IN;
                }
                break;
            case FadeState.FADE_IN:
                if (IsFadeIn())
                {
                    m_fadeState = FadeState.END_FADE_IN;
                }
                break;
            case FadeState.END_FADE_IN:
                if (IsFadeIn())
                {
                    m_fadeState = FadeState.END_FADE_IN;
                }
                break;
            case FadeState.FADE_OUT:
                if (IsFadeOut())
                {
                    m_fadeState = FadeState.END_FADE_IN;
                }
                break;
            case FadeState.END_FADE_OUT:
                if (IsFadeIn())
                {
                    m_fadeState = FadeState.END_FADE_IN;
                }
                break;
            case FadeState.TRANSITION_SCENE:
                if (IsFadeOut())
                {
                    m_fadeState = FadeState.END_FADE_IN;
                }
                break;
            default:
                break;
        }
    }


    public virtual void FadeIn() { }
    public virtual bool IsFadeIn() { return false; }
    public virtual void FadeOut() { }
    public virtual bool IsFadeOut() { return false; }

    public virtual void TranstionScene()
    {
        SceneManager.LoadScene(m_transitionSceneName);
    }
}
