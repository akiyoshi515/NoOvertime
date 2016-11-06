using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace AkiVACO
{

    public class XEventTimer : MonoBehaviour
    {
        [SerializeField]
        protected float m_time = 0.0f;
        public float time
        {
            get { return m_time; }
            set { m_time = value; }
        }

        [SerializeField]
        protected bool m_isLoop = false;
        public bool isLoop
        {
            get { return m_isLoop; }
            set { m_isLoop = value; }
        }

        [SerializeField]
        protected bool m_playOnAwake = false;
        public bool playOnAwake
        {
            get { return m_playOnAwake; }
            set { m_playOnAwake = value; }
        }

        [SerializeField]
        protected bool m_isOnShot = false;
        public bool isOnShot
        {
            get { return m_isOnShot; }
            protected set { m_isOnShot = value; }
        }

        [SerializeField]
        protected AkiVACO.XUnityEvent m_events = new AkiVACO.XUnityEvent();
        public AkiVACO.XUnityEvent events
        {
            get { return m_events; }
            protected set { m_events = value; }
        }

        protected float m_nowTime = 0.0f;

        void Awake()
        {
            this.enabled = m_playOnAwake;
            if (m_playOnAwake)
            {
                m_nowTime = m_time;
            }
        }

        // Update is called once per frame
        void Update()
        {
            m_nowTime -= Time.deltaTime;
            if (m_nowTime <= 0.0f)
            {
                m_events.Invoke();
                if (m_isLoop)
                {
                    m_nowTime = m_time - m_nowTime;
                }
                else
                {
                    m_nowTime = 0.0f;
                    if (isOnShot)
                    {
                        Destroy(this);
                    }
                    else
                    {
                        this.enabled = false;
                    }
                }
            }
        }

        public void DetachTimer()
        {
            Destroy(this);
        }

        public void StartTimer()
        {
            m_nowTime = m_time;
            this.enabled = true;
        }

        public void StopTimer()
        {
            this.enabled = false;
        }

        public void PauseTimer()
        {
            this.enabled = false;
        }

        public void ResumeTimer()
        {
            this.enabled = true;
        }

        public void AddListener(UnityAction call)
        {
            m_events.AddListener(call);
        }

        public void RemoveListener(UnityAction call)
        {
            m_events.RemoveListener(call);
        }

        public void RemoveAllListeners()
        {
            m_events.RemoveAllListeners();
        }

        public static void AttachTimer(out XEventTimer outTimer, GameObject obj, float time, UnityAction call = null)
        {
            XEventTimer ev = obj.AddComponent<XEventTimer>();
            ev.isLoop = false;
            ev.isOnShot = false;
            ev.time = time;

            if (call != null)
            {
                ev.AddListener(call);
            }

            outTimer = ev;
        }

        public static void AttachLoopTimer(out XEventTimer outTimer, GameObject obj, float time, UnityAction call = null)
        {
            XEventTimer ev = obj.AddComponent<XEventTimer>();
            ev.isLoop = true;
            ev.isOnShot = false;
            ev.time = time;

            if (call != null)
            {
                ev.AddListener(call);
            }

            outTimer = ev;
        }

        public static void StartOneShot(GameObject obj, float time, UnityAction call)
        {
            XEventTimer ev = obj.AddComponent<XEventTimer>();
            ev.isLoop = false;
            ev.isOnShot = true;
            ev.time = time;
            ev.AddListener(call);

            ev.StartTimer();
        }

    }

}