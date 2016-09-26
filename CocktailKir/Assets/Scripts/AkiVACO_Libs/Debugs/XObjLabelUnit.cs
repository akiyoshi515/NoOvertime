
// Author     : Atuki Yoshinaga

#define XLABEL_ENABLE

#if DEBUG
#else
#undef XLABEL_ENABLE
#endif

namespace AkiVACO
{
    
using UnityEngine;
using System.Collections;

#if XLABEL_ENABLE

public class XObjLabelUnit : MonoBehaviour
{
    public float m_backScreenAlpha = 0.50f;

    public KeyCode m_keyCode = KeyCode.O;

    public bool m_enableView = false;

    public int m_sizelabelInfo = 0;

    public string[] m_tags;
    public Color[] m_backScreenColor;
    public Vector2[] m_labelSize;

    private GUIStyleState m_styleState = null;
    private GUIStyle m_style = null;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        m_styleState = new GUIStyleState();
        m_style = new GUIStyle();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(m_keyCode))
        {
            m_enableView = !m_enableView;
        }


    }

    void OnGUI()
    {
        if (!m_enableView)
        {
            return;
        }

        Color prevColor = GUI.backgroundColor;

        m_styleState.background = Texture2D.whiteTexture;
        m_style.normal = m_styleState;

        for (int i = 0; i < m_tags.Length; ++i)
        {
            Color clr = m_backScreenColor[i];
            clr.a = m_backScreenAlpha;
            GUI.backgroundColor = clr;

            GameObject[] table = GameObject.FindGameObjectsWithTag(m_tags[i]);
            foreach (GameObject obj in table)
            {
                RenderLabel(obj, m_labelSize[i]);
            }
        }

        GUI.backgroundColor = prevColor;
    }

    void RenderLabel(GameObject obj, Vector2 size)
    {
        try
        {
            IXObjLabelEx str = obj.GetComponent(typeof(IXObjLabelEx)) as IXObjLabelEx;
            if (str != null)
            {
                string msg = obj.name;
                Vector3 vec = Camera.main.WorldToScreenPoint(obj.transform.position);
                GUI.Label(new Rect(vec.x, Screen.height - vec.y - size.y, size.x, 12.0f), msg, m_style);
                GUI.Label(new Rect(vec.x, Screen.height - vec.y - size.y + 12.0f, size.x, size.y - 12.0f), str.GetLabelString(), m_style);
            }
            else
            {
                string msg = obj.name;
                Vector3 vec = Camera.main.WorldToScreenPoint(obj.transform.position);
                GUI.Label(new Rect(vec.x, Screen.height - vec.y - size.y, size.x, size.y), msg, m_style);
            }
        }
        catch
        {
            // Empty
        }
    }

}

#else
    
public class XObjLabelUnit : MonoBehaviour {
    
    public float m_backScreenAlpha = 0.50f;

    public KeyCode m_keyCode = KeyCode.O;

    public bool m_enableView = false;

    public int m_sizelabelInfo = 0;

    public string[] m_tags;
    public Color[] m_backScreenColor;
    public Vector2[] m_labelSize;

}

#endif

}
