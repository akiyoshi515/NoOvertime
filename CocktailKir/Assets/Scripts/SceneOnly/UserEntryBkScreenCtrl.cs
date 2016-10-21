using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasRenderer))]
public class UserEntryBkScreenCtrl : MonoBehaviour
{
    private enum State
    {
        None,
        Opening,
    }

    [SerializeField]
    private float m_openTime = 1.0f;

    private CanvasRenderer m_renderer = null;
    private float m_time = 0.0f;
    private State m_sts = State.None;

    // Use this for initialization
    void Start()
    {
        m_renderer = this.GetComponent<CanvasRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (m_sts)
        {
            case State.None:
                break;
            case State.Opening:
                m_time -= Time.deltaTime;
                if (m_time <= 0.0f)
                {
                    m_sts = State.None;
                    m_time = 0.0f;
                }
                m_renderer.SetAlpha(m_time * (1.0f / m_openTime));
                break;
        }
    }

    public void Open()
    {
        m_sts = State.Opening;
        m_time = m_openTime;
    }

}
