using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavLineAgent))]
[RequireComponent(typeof(CharBaseCtrl))]
public class CharNaviLineCtrl : MonoBehaviour
{
    private enum State
    {
        None,
        Waiting,
        Starting,
        Stopping,
    } 

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

    public int m_stopCount = 2;
    public int m_restartCount = 2;
    public float m_stdAcceleration = 1.0f;
    public float m_restartAcceleration = 0.50f;

    private NavLineAgent m_navLineAgent = null;
    private State m_sts = State.None;
    private float m_waitTime = 0.0f;
    public bool IsWaiting
    {
        get { return (m_sts == State.Waiting); }
    }

    public float waitTime
    {
        get { return m_waitTime; }
    }

    private Vector3 m_prevDirection = Vector3.zero;

    void Start()
    {
        naviAgent = this.GetComponentInChildren<NavMeshAgent>();
        charCtrl = this.GetComponent<CharBaseCtrl>();
        m_navLineAgent = this.GetComponent<NavLineAgent>();
        m_navLineAgent.NextIndex();

        naviAgent.updateRotation = false;
        naviAgent.updatePosition = true;

        naviAgent.speed = charCtrl.moveSpeed;
        naviAgent.acceleration = m_stdAcceleration;
    }

    void Update()
    {
        switch(m_sts)
        {
            case State.None:
                OnUpdate(
                    () =>
                    {
                        float pt = m_navLineAgent.GetWaitTime(m_stopCount);
                        if (pt != 0.0f)
                        {
                            m_sts = State.Stopping;
                            naviAgent.autoBraking = true;
                        }
                    });
                break;

            case State.Starting:
                OnUpdate(
                    () =>
                    {
                        float t = m_navLineAgent.GetWaitTime(-m_restartCount);
                        if (t != 0.0f)
                        {
                            m_sts = State.None;
                            naviAgent.acceleration = m_stdAcceleration;
                        }
                    });
                break;

            case State.Stopping:
                OnUpdate(
                    () =>
                    {
                        float t = m_navLineAgent.GetWaitTime(-1);
                        if (t != 0.0f)
                        {
                            m_sts = State.Waiting;
                            m_waitTime = t;
                        }
                    });
                break;

            case State.Waiting:
                m_waitTime -= Time.deltaTime;
                if (m_waitTime <= 0.0f)
                {
                    m_waitTime = 0.0f;
                    m_sts = State.Starting;
                    naviAgent.autoBraking = false;
                    naviAgent.acceleration = m_restartAcceleration;
                }
                break;
        }
    }

    private void OnUpdate(UnityEngine.Events.UnityAction callback)
    {
        naviAgent.SetDestination(m_navLineAgent.m_vecTarget);

        if (naviAgent.remainingDistance > naviAgent.stoppingDistance)
        {
            charCtrl.Move(naviAgent.desiredVelocity, false, false);
        }
        else
        {
            if (m_navLineAgent.NextIndex())
            {
                callback.Invoke();
            }
            else
            {
                StopNavi();
            }
        }
        ValidMovement();
    }

    private void ValidMovement()
    {
        return;
        Vector3 movement = (m_navLineAgent.m_vecTarget - this.transform.position).normalized;
        float dt = Vector3.Dot(m_prevDirection, movement);
        while (dt < 0.0f)
        {
            AkiVACO.XLogger.LogWarning("Invalid Operate Passing target.");
            if (m_navLineAgent.NextIndex())
            {
                movement = (m_navLineAgent.m_vecTarget - this.transform.position).normalized;
                dt = Vector3.Dot(this.transform.forward, movement);
            }
            else
            {
                StopNavi();
            }
        }
        m_prevDirection = movement;
    }

    public void StopNavi()
    {
        naviAgent.enabled = false;
        this.enabled = false;
    }

}