using UnityEngine;
using System.Collections;

public class UserNoUI : MonoBehaviour
{
    
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
    /// <summary>   trueだったら画面表示,falseだったら画面非表示. </summary>
    ///
    [SerializeField]
    bool m_isDisplay;
    public bool IsDisplay
    {
        get { return m_isDisplay; }
        set { m_isDisplay = value; }
    }
    ///
    /// <summary>   管理するメッシュレンダラーコンポーネント.  </summary>
    ///

    MeshRenderer m_meshRenderer;

    void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        m_meshRenderer.enabled = m_isDisplay;
        // カメラに合わせて回転
        transform.LookAt(transform.position - m_targetCamera.transform.rotation * Vector3.back, m_targetCamera.transform.rotation * Vector3.up);

    }

}
