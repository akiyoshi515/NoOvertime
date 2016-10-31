using UnityEngine;
using System.Collections;

public class GuestCtrlImpatient : GuestCtrl
{
    protected interface ICtrlState
    {
        void OnAwake(GuestCtrlImpatient ctrl);
        void OnUpdate(GuestCtrlImpatient ctrl);
        void OnHitBallet(GuestCtrlImpatient ctrl);
        void OnDestroyedAttractField(GuestCtrlImpatient ctrl);
    }

    private ICtrlState m_stateCtrl;
    private void SetState<T>() where T : ICtrlState, new()
    {
        m_stateCtrl = new T();
        m_stateCtrl.OnAwake(this);
    }

    [SerializeField]
    private float m_waitTime = 5.0f;

    private float m_time = 0.0f;

    protected override void Start()
    {
        base.Start();
        m_stateCtrl = new CtrlStateEntry();
    }

    protected override void OnUpdate()
    {
        m_stateCtrl.OnUpdate(this);
    }

    protected override void OnHitBallet()
    {
        m_stateCtrl.OnHitBallet(this);
    }

    protected override void OnDestroyedAttractField()
    {
        m_stateCtrl.OnDestroyedAttractField(this);
    }

    protected class CtrlStateEntry : ICtrlState
    {
        public void OnAwake(GuestCtrlImpatient ctrl)
        {
        }

        public void OnUpdate(GuestCtrlImpatient ctrl)
        {
        }

        public void OnHitBallet(GuestCtrlImpatient ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlImpatient ctrl)
        {
        }

    }

    protected class CtrlStateWait : ICtrlState
    {
        public void OnAwake(GuestCtrlImpatient ctrl)
        {
            if (ctrl.m_unit.topUserId == -1)
            {
                ctrl.m_time = ctrl.m_waitTime;
            }
            else
            {
                ctrl.SetState<CtrlStateExit>();
            }
        }

        public void OnUpdate(GuestCtrlImpatient ctrl)
        {
            ctrl.m_time -= Time.deltaTime;
            if (ctrl.m_time <= 0.0f)
            {
                ctrl.SetState<CtrlStateExit>();
            }
        }

        public void OnHitBallet(GuestCtrlImpatient ctrl)
        {
            ctrl.SetState<CtrlStateStay>();
        }

        public void OnDestroyedAttractField(GuestCtrlImpatient ctrl)
        {
        }

    }

    protected class CtrlStateStay : ICtrlState
    {
        public void OnAwake(GuestCtrlImpatient ctrl)
        {
            // TODO
            ctrl.SetState<CtrlStateExit>();
        }

        public void OnUpdate(GuestCtrlImpatient ctrl)
        {
        }

        public void OnHitBallet(GuestCtrlImpatient ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlImpatient ctrl)
        {
        }
    }

    protected class CtrlStateExit : ICtrlState
    {
        public void OnAwake(GuestCtrlImpatient ctrl)
        {
        }

        public void OnUpdate(GuestCtrlImpatient ctrl)
        {
        }

        public void OnHitBallet(GuestCtrlImpatient ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlImpatient ctrl)
        {
            ctrl.m_naviCtrl.SetNavTarget(ctrl.m_goOutDestination);
        }

    }

}
