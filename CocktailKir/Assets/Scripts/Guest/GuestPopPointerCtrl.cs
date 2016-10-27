using UnityEngine;
using System.Collections;

using AkiVACO;

public class GuestPopPointerCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject m_standardGuest = null;

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
    /// GuestのPop処理
    /// </summary>
    /// <param name="baseObject">GuestのBaseObject</param>
    /// <param name="destination">目的地</param>
    /// <param name="goOutDestination">退避時の目的地</param>
    private void PopGuest(GameObject baseObject, Transform destination, Transform goOutDestination)
    {
        GameObject obj = XFunctions.Instance(baseObject, this.transform.position, this.transform.rotation);

        // TODO
    }

}
