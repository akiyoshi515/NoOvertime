using UnityEngine;
using System.Collections;

using AkiVACO;

[RequireComponent(typeof(CharNaviCtrl))]
public class GuestCtrl : MonoBehaviour
{
    protected virtual void OnUpdate() { }
    protected virtual void OnHitBallet() { }
    protected virtual void OnDestroyedAttractField() { }

    protected GuestScoreUnit m_unit = new GuestScoreUnit();
    public GuestScoreUnit unit
    {
        get { return m_unit; }
    }

    protected CharNaviCtrl m_naviCtrl = null;

    protected Transform m_attractTarget = null;
    public bool isAttractTarget
    {
        get { return (m_attractTarget != null); }
    }

    protected Vector3 m_destination = Vector3.zero;
    protected Vector3 m_goOutDestination = Vector3.zero;

    // Use this for initialization
    protected virtual void Start()
    {
        m_unit.Initialize();

        m_naviCtrl = this.GetComponent<CharNaviCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate(); // TODO

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
            OnHitBallet();
        }
        
        if (col.tag == "AttractField")
        {
            SetAttractField(col.gameObject);
            // TODO
        }
    }

    public void SetDestination(Vector3 destination, Vector3 goOutDestination)
    {
        m_destination = destination;
        m_goOutDestination = goOutDestination;

        CharNaviCtrl ctrl = this.GetComponent<CharNaviCtrl>();
        ctrl.SetNavTarget(destination);
    }

    private void SetAttractField(GameObject obj)
    {
        m_attractTarget = obj.transform;
        m_naviCtrl.SetNavTarget(obj.transform);
        obj.GetComponent<AttractFieldCtrl>().RegistObject(this);
    }

    public void SendDestroyedAttractField()
    {
        m_attractTarget = null;
        m_naviCtrl.SetNavTarget(m_destination); // TODO
        OnDestroyedAttractField();
    }

}
