using UnityEngine;
using System.Collections;

using AkiVACO;

[RequireComponent(typeof(CharNaviCtrl))]
public class GuestCtrl : MonoBehaviour
{
    private GuestScoreUnit m_unit = new GuestScoreUnit();
    public GuestScoreUnit unit
    {
        get { return m_unit; }
    }

    private CharNaviCtrl m_naviCtrl = null;

    // Use this for initialization
    void Start()
    {
        m_unit.Initialize();

        m_naviCtrl = this.GetComponent<CharNaviCtrl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ballet")
        {
            BalletCtrl ctrl = col.gameObject.GetComponent<BalletCtrl>();
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
        
        if (col.tag == "AttractField")
        {
            m_naviCtrl.SetNavTarget(col.gameObject.transform);
            col.gameObject.GetComponent<AttractFieldCtrl>().RegistObject(this);
            // TODO
        }
    }

    public void SendDestroyedAttractField()
    {
        m_naviCtrl.SetNavTarget(null);  // TODO
    }


}
