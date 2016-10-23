using UnityEngine;
using System.Collections;

public class UserNameUI : MonoBehaviour {

    ///
    /// <summary>   Userの名前.   </summary>
    ///

    [SerializeField, Header("Userの名前")]
    string m_name;
    public string Name
    {
        get
        {
            return m_name;
        }
        set
        {
            m_name = value;
            m_textMesh.text = m_name;
        }
    }

    ///
    /// <summary>   ユーザーごとの色.   </summary>
    ///
    [SerializeField, Header("表示色")]
    Color m_userColor;
    public UnityEngine.Color UserColor
    {
        get
        {
            return m_userColor;
        }
        set
        {
            m_userColor = value;
            m_textMesh.color = m_userColor;
        }
    }

    ///
    /// <summary>   回転の指標にするカメラ.  </summary>
    ///

    [SerializeField, Header("回転の指標にするカメラ")]
    Camera m_targetCamera;
    public UnityEngine.Camera TargetCamera
    {
        get
        {
            return m_targetCamera;
        }
        set
        {
            m_targetCamera = value;
        }
    }

    ///
    /// <summary>   The text mesh.  </summary>
    ///

    TextMesh m_textMesh;

    void Start()
    {
        m_textMesh = gameObject.GetComponent<TextMesh>();
        m_textMesh.color = m_userColor;
        m_textMesh.text = m_name;
    }



    void Update()
    {
        // カメラに合わせて回転
        transform.LookAt(transform.position - m_targetCamera.transform.rotation * Vector3.back, m_targetCamera.transform.rotation * Vector3.up);

    }

}
