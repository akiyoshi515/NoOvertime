
using UnityEngine;
using System.Collections;

namespace AkiVACO
{

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class CombineMeshes : MonoBehaviour
    {
        public Material m_targetMaterial;

        void Start()
        {
            Component[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; ++i)
            {
                combine[i].mesh = ((MeshFilter)meshFilters[i]).sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }

            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            transform.gameObject.SetActive(true);

            transform.gameObject.GetComponent<Renderer>().material = m_targetMaterial;
        }
    }

}