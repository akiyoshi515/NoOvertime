
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

public class XCollisionViewUnitSphere : XCollisionViewUnitBase
{
    private SphereCollider m_collider = null;

    protected override void CoordinateCollider(Transform transParent)
    {
        m_collider = transParent.GetComponent<SphereCollider>();
        XLogger.LogValidObject(m_collider, "DbgSys:GetComponent<SphereCollider>", gameObject);

        this.transform.localPosition = m_collider.center;
        float size = m_collider.radius * 2.0f;
        this.transform.localScale = new Vector3(size, size, size);
        this.transform.localRotation = transParent.localRotation;
    }

}

#else
    
public class XCollisionViewUnitSphere : XCollisionViewUnitBase 
{
    protected override void CoordinateCollider(Transform transParent)
    {
    }
}

#endif

}

}