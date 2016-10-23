using UnityEngine;
using System.Collections;

using AkiVACO;

public class GuestCtrl : MonoBehaviour
{
    private GuestScoreUnit m_unit = new GuestScoreUnit();
    public GuestScoreUnit unit
    {
        get { return m_unit; }
    }

    // Use this for initialization
    void Start()
    {
        m_unit.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ballet")
        {
            TestBalletCtrl ctrl = col.gameObject.GetComponent<TestBalletCtrl>();
            ctrl.SendHit();
        }
        else if (col.tag == "BalletTrigger")
        {
            AkiVACO.XLogger.Log("Hit!");
            BalletTrigger tr = col.GetComponent<BalletTrigger>();
            if (m_unit.AddCharm(tr.userID, tr.charm))
            {
                this.transform.GetChild(0).GetComponent<MeshMaterialCtrl>().SetMaterial(m_unit.topUserId + 1);  // -1 -> 0
            }
        }
    }
}
