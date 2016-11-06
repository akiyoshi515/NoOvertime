using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneCtrl : MonoBehaviour
{
    [SerializeField]
    private int m_firstHitCharmBonus = 3;

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

    }

    public void EventTimeUp()
    {
        AkiVACO.XEventTimer obj;
        AkiVACO.XEventTimer.AttachTimer(out obj, this.gameObject, 5.0f,
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
