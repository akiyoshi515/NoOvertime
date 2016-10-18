using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteEventObject : MonoBehaviour {

    ///
    /// <summary>   イベントのキー.   </summary>
    ///
    /// <remarks>   Hondy, 2016/10/18.  </remarks>
    ///
    /// <seealso cref="T:UnityEngine.MonoBehaviour"/>
    ///

    public struct EventKey
    {
        ///
        /// <summary>   イベントの発生タイム. </summary>
        ///

        float time;
        public float Time
        {
            get { return time; }
            set { time = value; }
        }
        ///
        /// <summary>   イベント.   </summary>
        ///

        IEventAction eventAction;
        public IEventAction EventAction
        {
            get { return eventAction; }
            set { eventAction = value; }
        }
    }

    float m_deltaTime;

    List<EventKey> eventKeyArray;

    List<EventKey> m_remainingEventKeyList;

    Vector3 m_prePosition;
    Vector3 m_nextPosition;

    Vector3 m_preRotation;
    Vector3 m_nextRotation;


    ///
    /// <summary>   Use this for initialization.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/18.  </remarks>
    ///

    void Start()
    {

    }



    ///
    /// <summary>   Update is called once per frame.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/18.  </remarks>
    ///

    void Update()
    {
        m_deltaTime += AkiVACO.XTime.deltaTime;
        int i = 0;
        while (m_remainingEventKeyList[i].Time < m_deltaTime)
        {
            m_remainingEventKeyList[i].EventAction.Action();
        }
    }
}
