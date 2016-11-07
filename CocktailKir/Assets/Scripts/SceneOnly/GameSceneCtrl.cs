using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneCtrl : MonoBehaviour
{
    [SerializeField]
    private int m_firstHitCharmBonus = 3;

    public Text m_objtime = null;
    private float m_time = 0.0f;

    void Awake()
    {
        GuestScores.Reset(m_firstHitCharmBonus);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_objtime.text = ((int)m_time).ToString();
        AkiVACO.XEventTimer obj = this.transform.GetChild(0).GetComponent<AkiVACO.XEventTimer>();
        if (obj)
        {
            m_time = obj.nowTime;
        }
    }

    public void EventTimeUp()
    {
        m_objtime.text = "TimeUp";
        AkiVACO.XEventTimer obj;
        AkiVACO.XEventTimer.AttachTimer(out obj, this.gameObject, 3.0f,
            () => 
            {
                GoToNextScene();
            });
        obj.StartTimer();
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene("GameResult");
    }
}
