
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

using AkiVACO;

public class SceneSwitchManager : MonoBehaviour
{
    private static SceneSwitchManager m_instance = null;
    public static SceneSwitchManager Instance
    {
        get { return m_instance; }
    }

	private static float m_progress = 0.0f;
    public static float progress
    {
        get { return m_progress; }
        private set { m_progress = value; }
    }

    private static bool m_isWaitForSignal = false;
    private static bool m_isWaitForStartSignal = false;

    private string m_nextSceneName = "";

    public string nextSceneName
    {
        get { return m_nextSceneName; }
        set { m_nextSceneName = value; }
    }

    private bool m_enableLoadScene = true;

    //
    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
        {
            XLogger.LogWarning("This object is there are already.");
            Destroy(this.gameObject);
        }
    }

    public static void LoadScene(string name, bool enableLoadScene = true)
    {
        if (m_instance != null)
        {
            SceneSwitchManager sm = m_instance.GetComponent<SceneSwitchManager>();
            sm.nextSceneName = name;
            sm.LoadNextScene(enableLoadScene);
            return;
        }

        XLogger.LogWarning(LibConstants.ErrorMsg.GetMsgNotFoundObject("SceneSwitchManager"));
        XLogger.LogWarning("Force call LoadLevel() : " + name);
        SceneManager.LoadScene(name);
    }

    public void LoadNextScene(bool enableLoadScene)
    {
        m_enableLoadScene = enableLoadScene;

        StartCoroutine("LoadNextSceneAsync");
    }

    IEnumerator LoadNextSceneAsync()
    {
        m_isWaitForStartSignal = false;
        m_isWaitForSignal = false;

        do
        {
            yield return new WaitForEndOfFrame();
        } while (!m_isWaitForStartSignal);

        // Loading LoadScene 
        if (m_enableLoadScene)
        {
            SceneManager.LoadScene("NowLoading");
            yield return new WaitForEndOfFrame();
        }

        XLogger.Log("LoadScene :" + nextSceneName);
        AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        m_progress = async.progress;

        while (async.progress < 0.90f)
        {
			m_progress = async.progress;
            yield return new WaitForEndOfFrame();
        }

        m_progress = 1.0f;

        do
        {
            yield return new WaitForEndOfFrame();
        } while (!m_isWaitForSignal);

        async.allowSceneActivation = true;
        yield break;
    }

    public static void StartLoad()
    {
        XLogger.Log("Start Load");
        m_isWaitForStartSignal = true;
    }

    public static void GoToNextScene()
    {
        XLogger.Log("GoToNextScene");
        m_isWaitForSignal = true;
    }

    public static int GetNowSceneID()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public static string GetNowSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    
}
