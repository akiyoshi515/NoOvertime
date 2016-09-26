
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AkiVACO
{

    public class SEPlayerUnit : MonoBehaviour
    {

        private Transform m_pool = null;

        public GameObject m_unit = null;
        public int m_startInstance = 10;

        // Awake is called when the script instance is being loaded.
        void Awake()
        {
            m_pool = this.transform.FindChild("SEPool");
            XLogger.LogValidObject(m_pool, LibConstants.ErrorMsg.GetMsgNotFoundObject("SEPool"), gameObject);

            // Instance Pool
            for (int i = 0; i < m_startInstance; ++i)
            {
                XFunctions.InstanceChild(m_unit, Vector3.zero, Quaternion.identity, m_pool.gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public SEUnit AllocPlayer(AudioClip clip, Vector3 position, Quaternion rotate)
        {
            GameObject obj = AllocPlayerUnit(position, rotate);
            SEUnit unit = obj.GetComponent<SEUnit>();
            unit.PlayOneShot(clip, m_pool);
            return unit;
        }

        public SEUnit AllocPlayer(AudioClip clip, Vector3 position, Quaternion rotate, float volumeScale)
        {
            GameObject obj = AllocPlayerUnit(position, rotate);
            SEUnit unit = obj.GetComponent<SEUnit>();
            unit.PlayOneShot(clip, m_pool, volumeScale);
            return unit;
        }

        public SEUnit AllocLoopPlayer(AudioClip clip, Vector3 position, Quaternion rotate)
        {
            GameObject obj = AllocPlayerUnit(position, rotate);
            SEUnit unit = obj.GetComponent<SEUnit>();
            unit.PlayLoop(clip, m_pool);
            return unit;
        }

        public SEUnit AllocLoopPlayer(AudioClip clip, Vector3 position, Quaternion rotate, float volumeScale)
        {
            GameObject obj = AllocPlayerUnit(position, rotate);
            SEUnit unit = obj.GetComponent<SEUnit>();
            unit.PlayLoop(clip, m_pool, volumeScale);
            return unit;
        }

        private GameObject AllocPlayerUnit(Vector3 position, Quaternion rotate)
        {
            if (m_pool.childCount > 0)
            {
                GameObject obj = m_pool.GetChild(0).gameObject;
                obj.transform.parent = this.transform;
                obj.transform.position = position;
                obj.transform.rotation = rotate;
                return obj;
            }
            else
            {
                return XFunctions.InstanceChild(m_unit, position, rotate, gameObject);
            }
        }

    }

}