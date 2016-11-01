using UnityEngine;
using System.Collections;

using AkiVACO;

[RequireComponent(typeof(CharNaviCtrl))]
public class GuestCtrl : MonoBehaviour
{
    protected virtual void OnUpdate() { }
    protected virtual void OnNearTarget() { }
    protected virtual void OnHitBallet() { }
    protected virtual void OnDestroyedAttractField() { }

    protected const float NearOffsetDistance = 1.0f;
    protected const float StopedDistance = 0.250f;
    protected const float IllegalStopedTime = 5.0f;

    protected GuestScoreUnit m_unit = new GuestScoreUnit();
    public GuestScoreUnit unit
    {
        get { return m_unit; }
    }

    private CharNaviCtrl m_naviCtrl = null;

    protected Transform m_attractTarget = null;
    public bool isAttractTarget
    {
        get { return (m_attractTarget != null); }
    }

    protected Vector3 m_destination = Vector3.zero;
    protected Vector3 m_goOutDestination = Vector3.zero;

    private Vector3 m_prevPosition = Vector3.zero;
    private float m_stopedTime = 0.0f;

    // Use this for initialization
    protected virtual void Start()
    {
        m_unit.Initialize();

        m_naviCtrl = this.GetComponent<CharNaviCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();

        CheckDistanceToTargetPoint();
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
        m_stopedTime = 0.0f;
        obj.GetComponent<AttractFieldCtrl>().RegistObject(this);
    }

    public void SendDestroyedAttractField()
    {
        m_attractTarget = null;
        m_naviCtrl.SetNavTarget(m_destination);
        m_stopedTime = 0.0f;
        OnDestroyedAttractField();
    }

    public void SetNavTarget(Vector3 target)
    {
        m_naviCtrl.SetNavTarget(target);
        m_stopedTime = 0.0f;
    }

    private void CheckDistanceToTargetPoint()
    {
        if (!isAttractTarget)
        {

            if (Vector3.Distance(m_naviCtrl.targetPosition, this.transform.position) <= NearOffsetDistance)
            {
                OnNearTarget();
            }
            else if (Vector3.Distance(m_prevPosition, this.transform.position) <= StopedDistance)
            {
                m_stopedTime += Time.deltaTime;
                if (m_stopedTime >= IllegalStopedTime)
                {
                    m_stopedTime = 0.0f;
                    if (m_naviCtrl.targetPosition != this.m_goOutDestination)
                    {
                        OnNearTarget();
                    }
                }
            }
            else
            {
                m_stopedTime = 0.0f;
            }
        }

        m_prevPosition = this.transform.position;
    }
}
