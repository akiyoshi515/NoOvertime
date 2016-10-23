
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

public class XCollisionViewUnitCapsule : XCollisionViewUnitBase
{
    private CapsuleCollider m_collider = null;

    protected override void CoordinateCollider(Transform transParent)
    {
        m_collider = transParent.GetComponent<CapsuleCollider>();
        XLogger.LogValidObject(m_collider, "DbgSys:GetComponent<CapsuleCollider>", gameObject);

        this.transform.localPosition = m_collider.center;
        float size = m_collider.radius * 2.0f;
        this.transform.localScale = new Vector3(size, m_collider.height * 0.50f, size);
        this.transform.localRotation = transParent.localRotation;
    }

}

#else

    public class XCollisionViewUnitCapsule : XCollisionViewUnitBase 
{
    protected override void CoordinateCollider(Transform transParent)
    {
    }
}

#endif

}

}