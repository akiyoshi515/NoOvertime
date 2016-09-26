
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

    public class TagEditorOnlyCleaner : MonoBehaviour
    {
        private const string TargetTag = "EditorOnly";

        [SerializeField]
        private bool m_isEnable = true;

        void Awake()
        {
#if DEBUG
            if (!m_isEnable)
            {
                this.gameObject.SetActive(false);
            }
#endif
        }

        // Update is called once per frame
        void Update()
        {
            GameObject[] table = GameObject.FindGameObjectsWithTag(TargetTag);
            foreach (GameObject obj in table)
            {
                XLogger.Log("Destroy object Tag:EditorOnly: " + obj.name);
                Destroy(obj.gameObject);
            }
        }

    }

}