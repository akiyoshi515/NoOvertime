using UnityEngine;
using System.Collections;

public class MeshMaterialCtrl : MonoBehaviour
{
    [SerializeField]
    protected Renderer m_target = null;

    [SerializeField]
    protected Material[] m_materials = new Material[1];

    public void SetMaterial(int idx)
    {
        m_target.material = m_materials[idx];
    }
}
