
// Author     : Atuki Yoshinaga

#define XNOVIEWER_ENABLE

#if DEBUG
#else
#undef XNOVIEWER_ENABLE
#endif

namespace AkiVACO
{

namespace XNavObstacleViewInternal
{

using UnityEngine;
using System.Collections;

#if XNOVIEWER_ENABLE

public class XNavObstacleViewUnitCapsule : XNavObstacleViewUnitBase
{
    private NavMeshObstacle m_obstacle = null;

    protected override void CoordinateCollider(Transform transParent)
    {
        m_obstacle = transParent.GetComponent<NavMeshObstacle>();
        XLogger.LogValidObject(m_obstacle, "DbgSys:GetComponent<NavMeshObstacle:Capsule>", gameObject);
        if (m_obstacle.shape == NavMeshObstacleShape.Capsule)
        {
            this.transform.localPosition = m_obstacle.center;
            float size = m_obstacle.radius * 2.0f;
            this.transform.localScale = new Vector3(size, m_obstacle.height * 0.50f, size);
            this.transform.localRotation = transParent.localRotation;
        }
        else
        {
            XLogger.LogError("Invalid NavMeshObstacle: type=Box");
        }
    }

}

#else

public class XNavObstacleViewUnitCapsule : XNavObstacleViewUnitBase 
{
    protected override void CoordinateCollider(Transform transParent)
    {
    }
}

#endif

}

}