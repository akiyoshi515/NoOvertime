using UnityEngine;
using System.Collections;

public class GuestRadarMesh : MonoBehaviour {

    UserID m_id;
    [SerializeField]
    GuestCharmViewer m_charmViwer;
    GuestScoreUnit m_unit;

    [SerializeField]
    MeshRenderer m_mesh;

    Material m_meshMaterial;

    [SerializeField]
    Color[] m_color = new Color[5];
    // Use this for initialization
    void Start () {

        m_unit = m_charmViwer.GetComponent<GuestCtrl>().unit;
        m_meshMaterial = m_mesh.material;
    }
	
	// Update is called once per frame
	void Update () {

        switch (m_unit.topUserId)
        {
            case -1:
                m_meshMaterial.SetColor("_Color",m_color[0]);
                break;
            case 0:
                m_meshMaterial.SetColor("_Color", m_color[1]);
                break;
            case 1:
                m_meshMaterial.SetColor("_Color", m_color[2]);
                break;
            case 2:
                m_meshMaterial.SetColor("_Color", m_color[3]);
                break;
            case 3:
                m_meshMaterial.SetColor("_Color", m_color[4]);
                break;
        }
    }
}
