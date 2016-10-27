
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

public class XNavObstacleViewUnitBox : XNavObstacleViewUnitBase
{
    private NavMeshObstacle m_obstacle = null;

    protected override void CoordinateCollider(Transform transParent)
    {
        m_obstacle = transParent.GetComponent<NavMeshObstacle>();
        XLogger.LogValidObject(m_obstacle, "DbgSys:GetComponent<NavMeshObstacle:Box>", gameObject);
        if (m_obstacle.shape == NavMeshObstacleShape.Box)
        {
            this.transform.localPosition = m_obstacle.center;
            this.transform.localScale = m_obstacle.size;
            this.transform.rotation = transParent.rotation;
        }
        else
        {
            XLogger.LogError("Invalid NavMeshObstacle: type=Capsule");
        }
    }

}

#else
    
public class XNavObstacleViewUnitBox : XNavObstacleViewUnitBase 
{
    protected override void CoordinateCollider(Transform transParent)
    {
    }
}

#endif

}

}