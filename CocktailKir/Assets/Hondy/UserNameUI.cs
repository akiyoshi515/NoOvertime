using UnityEngine;
using System.Collections;

public class UserNameUI : MonoBehaviour {

    ///
    /// <summary>   Userの名前.   </summary>
    ///

    [SerializeField]
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
    [SerializeField]
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

    [SerializeField]
    Camera targetCamera;
    public UnityEngine.Camera TargetCamera
    {
        get
        {
            return targetCamera;
        }
        set
        {
            targetCamera = value;
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
        transform.LookAt(transform.position - targetCamera.transform.rotation * Vector3.back, targetCamera.transform.rotation * Vector3.up);

    }

}
