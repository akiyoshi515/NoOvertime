
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AkiVACO
{

    public class SEUnit : MonoBehaviour, IXObjLabelEx
    {

        private AudioSource m_audioSource = null;
        private Transform m_pool = null;

        public float volume
        {
            get { return m_audioSource.volume; }
            set { m_audioSource.volume = value; }
        }

        // Awake is called when the script instance is being loaded.
        void Awake()
        {
            m_audioSource = this.GetComponent<AudioSource>();
            XLogger.LogValidObject(m_audioSource, LibConstants.ErrorMsg.GetMsgNotBoundComponent("AudioSource"), gameObject);
            this.gameObject.SetActive(false);
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_audioSource.isPlaying)
            {
                // Post SEPool
                this.transform.parent = m_pool;
                m_audioSource.clip = null;
                this.gameObject.SetActive(false);
            }
        }

        // Unique functions
        public void PlayOneShot(AudioClip clip, Transform pool)
        {
            this.gameObject.SetActive(true);
            m_pool = pool;
            m_audioSource.clip = clip;
            m_audioSource.loop = false;
            m_audioSource.PlayOneShot(clip);
        }

        public void PlayOneShot(AudioClip clip, Transform pool, float volumeScale)
        {
            this.gameObject.SetActive(true);
            m_pool = pool;
            m_audioSource.clip = clip;
            m_audioSource.loop = false;
            m_audioSource.PlayOneShot(clip, volumeScale);
        }

        public void PlayLoop(AudioClip clip, Transform pool)
        {
            this.gameObject.SetActive(true);
            m_pool = pool;
            m_audioSource.clip = clip;
            m_audioSource.loop = true;
            m_audioSource.PlayOneShot(clip);
        }

        public void PlayLoop(AudioClip clip, Transform pool, float volumeScale)
        {
            this.gameObject.SetActive(true);
            m_pool = pool;
            m_audioSource.clip = clip;
            m_audioSource.loop = true;
            m_audioSource.PlayOneShot(clip, volumeScale);
        }

        public void Stop()
        {
            m_audioSource.Stop();
        }

        public string GetLabelString()
        {
            return "N:" + m_audioSource.clip.name;
        }

    }

}