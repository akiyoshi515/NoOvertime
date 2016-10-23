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

    private Camera targetCamera;
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
    }



    void Update()
    {
        // カメラに合わせて回転
        transform.LookAt(transform.position - TargetCamera.transform.rotation * Vector3.back, TargetCamera.transform.rotation * Vector3.up);

    }

}
