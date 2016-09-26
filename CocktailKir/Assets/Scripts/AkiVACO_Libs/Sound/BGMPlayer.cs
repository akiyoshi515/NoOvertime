
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

    public class BGMPlayer : MonoBehaviour
    {
        private AudioSource m_audioSource = null;
        private float m_fadeSpeed = 0.0f;
        private int m_fadeInOutNextClip = -1;

        [SerializeField]
        private float m_maxVolume = 1.0f;

        [SerializeField]
        private float m_fadeInTime = 1.0f;
        [SerializeField]
        private float m_fadeOutTime = 1.0f;

        public float maxValume
        {
            get { return m_maxVolume; }
            private set { m_maxVolume = value; }
        }

        public float fadeInTime
        {
            get { return m_fadeInTime; }
            private set { m_fadeInTime = value; }
        }

        public float fadeOutTime
        {
            get { return m_fadeOutTime; }
            private set { m_fadeOutTime = value; }
        }

        public AudioClip[] m_audioClipTable = null;

        // Awake is called when the script instance is being loaded.
        void Awake()
        {
            for (int i = 0; i < m_audioClipTable.Length; ++i)
            {
                AudioClip clip = m_audioClipTable[i];
                if (clip == null)
                {
                    XLogger.LogWarning("AudioClipTable[" + i.ToString() + "]: Exception null reference", gameObject);
                }
            }

            if ((m_maxVolume < 0.0f) || (m_maxVolume > 1.0f))
            {
                XLogger.LogWarning("MaxVolume: Exception out of range: " + m_maxVolume.ToString(), gameObject);
            }

            m_audioSource = this.GetComponent<AudioSource>();
            XLogger.LogValidObject(m_audioSource, LibConstants.ErrorMsg.GetMsgNotBoundComponent("AudioSource"), gameObject);

            if (m_audioClipTable.Length > 0)
            {
                m_audioSource.clip = m_audioClipTable[0];
                if (m_audioSource.volume > m_maxVolume)
                {
                    m_audioSource.volume = m_maxVolume;
                }
                if (m_audioSource.playOnAwake)
                {
                    this.Play();
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            m_audioSource.loop = true;
            m_fadeSpeed = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_fadeSpeed != 0.0f)
            {
                float vlm = m_audioSource.volume;
                vlm += m_fadeSpeed * Time.unscaledDeltaTime;
                if (vlm >= m_maxVolume)
                {
                    vlm = m_maxVolume;
                    m_fadeSpeed = 0.0f;
                    m_fadeInOutNextClip = -1;
                }
                else if (vlm <= 0.0f)
                {
                    vlm = 0.0f;
                    if (m_fadeInOutNextClip >= 0)
                    {
                        FadeIn();
                        SetClip((uint)m_fadeInOutNextClip);
                        Play();
                    }
                    else
                    {
                        m_fadeSpeed = 0.0f;
                    }
                }
                m_audioSource.volume = vlm;
            }
        }

        void OnDestroy()
        {
            this.Stop();
        }

        // Unique functions
        public void Play()
        {
            m_audioSource.Play();
        }

        public void PlayDelayed(float delay)
        {
            m_audioSource.PlayDelayed(delay);
        }

        public void PlayScheduled(float time)
        {
            m_audioSource.PlayScheduled(time);
        }

        public void PlayFadeInOut(uint nextClip)
        {
            m_fadeInOutNextClip = (int)nextClip;

            FadeOut();
        }

        public void Stop()
        {
            m_audioSource.Stop();
        }

        public void Pause()
        {
            m_audioSource.Pause();
        }

        public void Resume()
        {
            m_audioSource.UnPause();
        }

        public void ResetParam()
        {
            m_audioSource.volume = m_maxVolume;
            this.SetPitch(1.0f);
            m_fadeSpeed = 0.0f;
        }

        public void FadeIn()
        {
            m_fadeSpeed = m_maxVolume / m_fadeInTime;
        }

        public void FadeOut()
        {
            m_fadeSpeed = -(m_maxVolume / m_fadeOutTime);
        }

        public void SetPitch(float pitch)
        {
            m_audioSource.pitch = pitch;
        }

        public bool IsPlaying()
        {
            return m_audioSource.isPlaying;
        }

        public float GetVolume()
        {
            return m_audioSource.volume;
        }

        public float GetPitch()
        {
            return m_audioSource.pitch;
        }

        public void SetMaxVolume(float volume)
        {
            if (volume < 0.0f)
            {
                volume = 0.0f;
            }

            m_maxVolume = volume;
        }

        public void SetClip(uint index)
        {
            Stop();

            if (index < m_audioClipTable.Length)
            {
                m_audioSource.clip = m_audioClipTable[index];
            }
            else
            {
                XLogger.LogError("Index: out of range", gameObject);
            }

        }

    }

}