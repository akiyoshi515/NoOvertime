using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///
/// <summary>   A bullet icon effect(試用).   </summary>
///
/// <remarks>   Hondy, 2016/10/31.  </remarks>
///
/// <seealso cref="T:UnityEngine.MonoBehaviour"/>
///

public class BulletIconEffect
    :
    MonoBehaviour
{

    enum BulletState
    {
        DISPLAY,
        DISPLAY_COMPLETE,
        HIDE,
        HIDE_COMPLETE
    }

    ///
    /// <summary>   The state.  </summary>
    ///

    BulletState m_state;

    float m_time;

    bool m_isNoneBullet;
    public bool IsNoneBullet
    {
        get { return m_isNoneBullet; }
        set { m_isNoneBullet = value; }
    }


    bool m_isReload;
    public bool IsReload
    {
        get { return m_isReload; }
        set { m_isReload = value; }
    }



    // Use this for initialization
    void Start () {

        m_state = BulletState.HIDE_COMPLETE;
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_isReload = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            m_isNoneBullet = true;
        }
        switch (m_state)
        {
            case BulletState.DISPLAY:
                if (m_time  < 0.2f)
                {
                    m_time += Time.deltaTime;
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) *(m_time / 0.2f);
                    transform.transform.eulerAngles = new Vector3(0, 0, 360*(m_time / 0.2f));
                } 
                else
                {
                    m_state = BulletState.DISPLAY_COMPLETE;
                    m_isNoneBullet = false;
                    m_time  = 0;
                }
                break;
            case BulletState.DISPLAY_COMPLETE:
                if (IsNoneBullet == true)
                {
                    m_state = BulletState.HIDE;
                }
                else
                {
                }
                break;
            case BulletState.HIDE:
                if (m_time < 0.1)
                {
                    m_time += Time.deltaTime;
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) - (new Vector3(1.0f, 1.0f, 1.0f) * (m_time / 0.1f));
                    transform.transform.eulerAngles = new Vector3(0, 0, 360- 360 * (m_time / 0.1f));
                }
                else
                {
                    m_state = BulletState.HIDE_COMPLETE;
                    m_time = 0;
                    transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                    transform.transform.eulerAngles = new Vector3(0, 0, 0);
                    m_isNoneBullet = false;
                    

                }
                break;
            case BulletState.HIDE_COMPLETE:
                if (IsReload)
                {
                    m_state = BulletState.DISPLAY;
                    m_isReload = false;
                }
                
                break;
            default:
                break;

        }
	}
}
