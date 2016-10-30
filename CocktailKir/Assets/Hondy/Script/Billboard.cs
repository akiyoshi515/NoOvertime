using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    ///
    /// <summary>   回転の指標にするカメラ.  </summary>
    ///

    [SerializeField, Header("回転指標にするカメラ")]
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
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Billlboard
        transform.LookAt(transform.position - m_targetCamera.transform.rotation * Vector3.back, m_targetCamera.transform.rotation * Vector3.up);

    }
}
