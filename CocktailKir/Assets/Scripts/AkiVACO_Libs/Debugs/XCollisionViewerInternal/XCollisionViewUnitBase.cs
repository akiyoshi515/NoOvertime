
// Author     : Atuki Yoshinaga

#define XCLVIEWER_ENABLE

#if DEBUG
#else
#undef XCLVIEWER_ENABLE
#endif

namespace AkiVACO
{
    
namespace XCollisionViewInternal
{

using UnityEngine;
using System.Collections;

#if XCLVIEWER_ENABLE

public abstract class XCollisionViewUnitBase : MonoBehaviour
{
    private bool m_prevEnableView = false;

    private XCollisionViewer m_viewer = null;
    private MeshRenderer m_renderer = null;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        m_renderer = this.GetComponent<MeshRenderer>();
        XLogger.LogValidObject(m_renderer, "DbgSys:GetComponent<MeshRenderer>", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_viewer == null)
        {
            XLogger.LogError("DbgSys:No Initialized", gameObject);
            return;
        }
        if (m_prevEnableView != m_viewer.enableView)
        {
            EnableView(m_viewer.enableView);
            m_prevEnableView = m_viewer.enableView;
        }
    }

    private void EnableView(bool enableView)
    {
        m_renderer.enabled = enableView;
    }

    public void Initialize(XCollisionViewer viewer, Color color)
    {
        m_viewer = viewer;
        CoordinateCollider(this.transform.parent);

        Color clr = color;
        clr.a = viewer.m_meshAlpha;
        m_renderer.material.color = clr;
        m_renderer.material.mainTexture = Texture2D.whiteTexture;

        m_prevEnableView = m_viewer.enableView;
        EnableView(m_viewer.enableView);
    }

    protected virtual void CoordinateCollider(Transform transParent)
    {

    }

}

#else
    
public abstract class XCollisionViewUnitBase : MonoBehaviour {
	
    protected virtual void CoordinateCollider(Transform transParent)
    {

    }

}

#endif

}

}