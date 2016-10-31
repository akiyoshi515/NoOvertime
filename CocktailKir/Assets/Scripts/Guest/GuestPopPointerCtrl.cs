using UnityEngine;
using System.Collections;

using AkiVACO;

public class GuestPopPointerCtrl : MonoBehaviour
{
    [SerializeField]
    public int m_cost = 0;

    [SerializeField]
    private GameObject m_targetPoint = null;

    [SerializeField]
    private GameObject m_standardGuest = null;

    void Awake()
    {
#if DEBUG
        // Empty
#else
        GameObject.Destroy(this.GetComponent<MeshRenderer>());
        GameObject.Destroy(this.GetComponent<MeshFilter>());
#endif
    }

    /// <summary>
    /// GuestのPopを指示する
    /// </summary>
    /// <param name="type">PopするGuestのType</param>
    /// <param name="destination">目的地</param>
    /// <param name="goOutDestination">退避時の目的地</param>
    public void SendPopGuest(GuestType type, Transform destination, Transform goOutDestination)
    {
        switch (type)
        {
            case GuestType.Standard:
                PopGuest(m_standardGuest, destination, goOutDestination);
                break;
                // TODO
        }
    }

    /// <summary>
    /// GuestのPopを指示する
    /// </summary>
    /// <param name="type">PopするGuestのType</param>
    /// <param name="destination">目的地(Vector3)</param>
    /// <param name="goOutDestination">退避時の目的地</param>
    public void SendPopGuest(GuestType type, Vector3 destination, Transform goOutDestination)
    {
        switch (type)
        {
            case GuestType.Standard:
                PopGuest(m_standardGuest, destination, goOutDestination);
                break;
            // TODO
        }
    }

    /// <summary>
    /// GuestのPop処理
    /// </summary>
    /// <param name="baseObject">GuestのBaseObject</param>
    /// <param name="destination">目的地</param>
    /// <param name="goOutDestination">退避時の目的地</param>
    private GameObject PopGuest(GameObject baseObject, Transform destination, Transform goOutDestination)
    {
        GameObject obj = XFunctions.Instance(baseObject, this.transform.position, this.transform.rotation);
        CharNaviCtrl ctrl = obj.GetComponent<CharNaviCtrl>();
        ctrl.target = destination;
        ctrl.isStaticTarget = false;
        // TODO
        return obj;
    }

    /// <summary>
    /// GuestのPop処理
    /// </summary>
    /// <param name="baseObject">GuestのBaseObject</param>
    /// <param name="destination">目的地(固定)</param>
    /// <param name="goOutDestination">退避時の目的地</param>
    private GameObject PopGuest(GameObject baseObject, Vector3 destination, Transform goOutDestination)
    {
        GameObject obj = XFunctions.Instance(baseObject, this.transform.position, this.transform.rotation);
        CharNaviCtrl ctrl = obj.GetComponent<CharNaviCtrl>();
        ctrl.target = null;
        ctrl.staticTarget = destination;
        ctrl.isStaticTarget = true;
        // TODO
        return obj;
    }

}
