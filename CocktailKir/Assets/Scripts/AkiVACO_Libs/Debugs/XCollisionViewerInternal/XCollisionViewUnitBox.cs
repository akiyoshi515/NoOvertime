
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

public class XCollisionViewUnitBox : XCollisionViewUnitBase
{
    private BoxCollider m_collider = null;

    protected override void CoordinateCollider(Transform transParent)
    {
        m_collider = transParent.GetComponent<BoxCollider>();
        XLogger.LogValidObject(m_collider, "DbgSys:GetComponent<BoxCollider>", gameObject);

        this.transform.localPosition = m_collider.center;
        this.transform.localScale = m_collider.size;
        this.transform.rotation = transParent.rotation;
    }

}

#else
    
public class XCollisionViewUnitBox : XCollisionViewUnitBase 
{
    protected override void CoordinateCollider(Transform transParent)
    {
    }
}

#endif

}

}