﻿using UnityEngine;
using System.Collections;

public class UIBillboard : MonoBehaviour
{

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

    ///
    /// <summary>  真なら表示. </summary>
    ///
    [SerializeField]
    bool m_isDisplay;
    
    ///
    /// <summary>   UIのメッシュ.  </summary>
    ///
    [SerializeField]
    MeshRenderer m_meshRenderer;
    // Use this for initialization
    void Start()
    {
        if (m_meshRenderer == null)
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Billlboard
        transform.LookAt(transform.position - m_targetCamera.transform.rotation * Vector3.back, m_targetCamera.transform.rotation * Vector3.up);

    }

    ///
    /// <summary>   表示切り替え.   </summary>
    ///
    /// <remarks>   Hondy, 2016/11/02.  </remarks>
    ///
    /// <param name="isEnable"> true if this object is enable.  </param>
    ///

    public void EnableView(bool isEnable)
    {
        m_isDisplay = isEnable;
        if (m_meshRenderer != null)
        {
            m_meshRenderer.enabled = m_isDisplay;
        } 
    }
}