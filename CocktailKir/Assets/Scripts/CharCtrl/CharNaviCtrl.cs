using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharBaseCtrl))]
public class CharNaviCtrl : MonoBehaviour
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

    [SerializeField]
    private Transform m_target = null;
    public Transform target
    {
        get { return m_target; }
        private set { m_target = value; }
    }

    public Vector3 staticTarget
    {
        get;
        private set;
    }

    public bool isStaticTarget
    {
        get;
        private set;
    }

    public Vector3 targetPosition
    {
        get 
        {
            if (isStaticTarget)
            {
                return staticTarget;
            }
            else
            {
                if (target != null)
                {
                    return target.position;
                }
                else
                {
                    return Vector3.zero;
                }
            }
        }
    }

    void Awake()
    {
        staticTarget = Vector3.zero;
    }

    void Start()
    {
        naviAgent = this.GetComponentInChildren<NavMeshAgent>();
        charCtrl = this.GetComponent<CharBaseCtrl>();

        naviAgent.updateRotation = false;
        naviAgent.updatePosition = true;
    }

    void Update()
    {
        if (isStaticTarget)
        {
            naviAgent.SetDestination(staticTarget);
        }
        else
        {
            if (target != null)
            {
                naviAgent.SetDestination(target.position);
            }
        }

        if (naviAgent.remainingDistance > naviAgent.stoppingDistance)
        {
            charCtrl.Move(naviAgent.desiredVelocity, false, false);
        }
        else
        {
            charCtrl.Move(Vector3.zero, false, false);
        }
    }

    void OnDestroy()
    {
        if (m_target != null)
        {
            if (m_target.GetComponent<NavPointOneShot>() != null)
            {
                Destroy(m_target.gameObject);
            }
        }
    }

    public void SetNavTarget(Transform target)
    {
        m_target = target;
        isStaticTarget = false;
    }

    public void SetNavTarget(Vector3 target)
    {
        staticTarget = target;
        isStaticTarget = true;
    }
}