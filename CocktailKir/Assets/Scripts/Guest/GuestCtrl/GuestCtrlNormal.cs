using UnityEngine;
using System.Collections;

public class GuestCtrlNormal : GuestCtrl
{
    protected interface ICtrlState
    {
        void OnAwake(GuestCtrlNormal ctrl);
        void OnUpdate(GuestCtrlNormal ctrl);
        void OnHitBallet(GuestCtrlNormal ctrl);
        void OnDestroyedAttractField(GuestCtrlNormal ctrl);
    }

    private ICtrlState m_stateCtrl;
    private void SetState<T>() where T : ICtrlState, new()
    {
        m_stateCtrl = new T();
        m_stateCtrl.OnAwake(this);
    }

    [SerializeField]
    private float m_waitTime = 10.0f;

    [SerializeField]
    private float m_stayTime = 10.0f;

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
        public void OnAwake(GuestCtrlNormal ctrl)
        {
        }

        public void OnUpdate(GuestCtrlNormal ctrl) 
        {

        }

        public void OnHitBallet(GuestCtrlNormal ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlNormal ctrl) 
        {
        }

    }

    protected class CtrlStateWait : ICtrlState
    {
        public void OnAwake(GuestCtrlNormal ctrl)
        {
            ctrl.m_time = ctrl.m_waitTime;
        }

        public void OnUpdate(GuestCtrlNormal ctrl)
        {
            ctrl.m_time -= Time.deltaTime;
            if (ctrl.m_time <= 0.0f)
            {
                ctrl.SetState<CtrlStateExit>();
            }
        }

        public void OnHitBallet(GuestCtrlNormal ctrl)
        {
            ctrl.SetState<CtrlStateStay>();
        }

        public void OnDestroyedAttractField(GuestCtrlNormal ctrl)
        {
        }

    }

    protected class CtrlStateStay : ICtrlState
    {
        public void OnAwake(GuestCtrlNormal ctrl)
        {
            ctrl.m_time = ctrl.m_stayTime;
        }

        public void OnUpdate(GuestCtrlNormal ctrl)
        {
            ctrl.m_time -= Time.deltaTime;
            if (ctrl.m_time <= 0.0f)
            {
                ctrl.SetState<CtrlStateExit>();
            }
        }

        public void OnHitBallet(GuestCtrlNormal ctrl)
        {
            ctrl.m_time = ctrl.m_stayTime;
        }

        public void OnDestroyedAttractField(GuestCtrlNormal ctrl)
        {
        }

    }

    protected class CtrlStateExit : ICtrlState
    {
        public void OnAwake(GuestCtrlNormal ctrl)
        {
        }

        public void OnUpdate(GuestCtrlNormal ctrl)
        {
        }

        public void OnHitBallet(GuestCtrlNormal ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlNormal ctrl)
        {
            ctrl.m_naviCtrl.SetNavTarget(ctrl.m_goOutDestination);
        }

    }

}
