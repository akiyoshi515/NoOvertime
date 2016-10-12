using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavLineAgent))]
[RequireComponent(typeof(CharBaseCtrl))]
public class CharNaviLineCtrl : MonoBehaviour
{
    public NavMeshAgent naviAgent
    {
        get;
        private set;
    }

    public CharBaseCtrl charCtrl
    {
        get;
        private set; 
    }

    private NavLineAgent m_navLineAgent = null;

    void Start()
    {
        naviAgent = this.GetComponentInChildren<NavMeshAgent>();
        charCtrl = this.GetComponent<CharBaseCtrl>();
        m_navLineAgent = this.GetComponent<NavLineAgent>();
        m_navLineAgent.NextIndex();

        naviAgent.updateRotation = false;
        naviAgent.updatePosition = true;
    }

    void Update()
    {
        naviAgent.SetDestination(m_navLineAgent.m_vecTarget);

        if (naviAgent.remainingDistance > naviAgent.stoppingDistance)
        {
            charCtrl.Move(naviAgent.desiredVelocity, false);
        }
        else
        {
            if (!(m_navLineAgent.NextIndex()))
            {
                StopNavi();
            }
//            charCtrl.Move(Vector3.zero, false);
        }
    }

    public void StopNavi()
    {
        naviAgent.enabled = false;
        this.enabled = false;
    }
}