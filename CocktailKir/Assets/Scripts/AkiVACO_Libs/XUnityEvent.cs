using UnityEngine.Events;
using System.Collections;

namespace AkiVACO
{
    [System.Serializable]
    public class XUnityEvent : UnityEvent
    {
    }

    [System.Serializable]
    public class XUnityEvent<T> : UnityEvent<T>
    {
    }

    [System.Serializable]
    public class XUnityEvent<T0, T1> : UnityEvent<T0, T1>
    {
    }

    [System.Serializable]
    public class XUnityEvent<T0, T1, T2> : UnityEvent<T0, T1, T2>
    {
    }

    [System.Serializable]
    public class XUnityEvent<T0, T1, T2, T3> : UnityEvent<T0, T1, T2, T3>
    {
    }

}