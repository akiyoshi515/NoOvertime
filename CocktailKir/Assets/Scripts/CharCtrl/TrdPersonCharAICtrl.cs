using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TrdPersonCharCtrl))]
public class TrdPersonCharAICtrl : MonoBehaviour
{
    public NavMeshAgent naviAgent
    {
        get;
        private set;
    }

    public TrdPersonCharCtrl charCtrl
    {
        get;
        private set; 
    }

    [SerializeField]
    private Transform m_target = null;
    public Transform target
    {
        get { return m_target; }
        set { m_target = value; }
    }

    void Start()
    {
        naviAgent = this.GetComponentInChildren<NavMeshAgent>();
        charCtrl = this.GetComponent<TrdPersonCharCtrl>();

        naviAgent.updateRotation = false;
        naviAgent.updatePosition = true;
    }

    void Update()
    {
        if (target != null)
        {
            naviAgent.SetDestination(target.position);
        }

        if (naviAgent.remainingDistance > naviAgent.stoppingDistance)
        {
            charCtrl.Move(naviAgent.desiredVelocity, false);
        }
        else
        {
            charCtrl.Move(Vector3.zero, false);
        }
    }

}