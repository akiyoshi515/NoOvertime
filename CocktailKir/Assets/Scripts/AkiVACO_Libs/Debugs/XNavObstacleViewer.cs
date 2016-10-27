
// Author     : Atuki Yoshinaga

#define XCLVIEWER_ENABLE

#if DEBUG
#else
#undef XCLVIEWER_ENABLE
#endif

namespace AkiVACO
{

using UnityEngine;
using System;
using System.Collections;
using XNavObstacleViewInternal;

#if XCLVIEWER_ENABLE

public class XNavObstacleViewer : MonoBehaviour
{
    public GameObject m_viewUnitBox = null;
    public GameObject m_viewUnitCapsule = null;

    public float m_meshAlpha = 0.50f;

    public KeyCode m_keyCode = KeyCode.N;

    private bool m_enableView = false;
    public bool enableView
    {
        get { return m_enableView; }
        set { m_enableView = value; }
    }

    public int m_sizelabelInfo = 0;

    public string[] m_tags;
    public Color[] m_meshColor;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_keyCode))
        {
            XLogger.Log("DbgSys:NavObstacleViewer:" + m_enableView.ToString());
            m_enableView = !m_enableView;
        }

        FindTouchView();
    }

    private void FindTouchView()
    {
        for (int i = 0; i < m_tags.Length; ++i)
        {
            Color clr = m_meshColor[i];
            clr.a = m_meshAlpha;
            GUI.backgroundColor = clr;

            GameObject[] table = GameObject.FindGameObjectsWithTag(m_tags[i]);
            foreach (GameObject obj in table)
            {
                if (obj.GetComponent<XNavObstacleViewFlagment>() == null)
                {
                    TouchView<NavMeshObstacle>(obj,
                        (sc) =>
                        {
                            switch(sc.shape)
                            {
                                case NavMeshObstacleShape.Box:
                                    GameObject unitBox = XFunctions.InstanceChild(m_viewUnitBox, Vector3.zero, Quaternion.identity, obj);
                                    unitBox.GetComponent<XNavObstacleViewUnitBox>().Initialize(this, m_meshColor[i]);
                                    break;
                                case NavMeshObstacleShape.Capsule:
                                    GameObject unitCapsule = XFunctions.InstanceChild(m_viewUnitCapsule, Vector3.zero, Quaternion.identity, obj);
                                    unitCapsule.GetComponent<XNavObstacleViewUnitCapsule>().Initialize(this, m_meshColor[i]);
                                    break;
                            }
                        });

                    obj.AddComponent<XNavObstacleViewFlagment>();
                }
            }
        }

    }
    
    private void TouchView<T>(GameObject obj, Action<T> func)
    {
        T[] table = obj.GetComponents<T>();
        if (table.Length != 0)
        {
            foreach (T col in table)
            {
                func(col);
            }
        }
    }

}

#else

public class XNavObstacleViewer : MonoBehaviour
{
    
    public GameObject m_viewUnitSphere = null;
    public GameObject m_viewUnitBox = null;

    public float m_meshAlpha = 0.50f;

    public KeyCode m_keyCode = KeyCode.U;

    private bool m_enableView = false;
    public bool enableView
    {
        get { return m_enableView; }
        set { m_enableView = value; }
    }

    public int m_sizelabelInfo = 0;

    public string[] m_tags;
    public Color[] m_meshColor;

}

#endif

}
