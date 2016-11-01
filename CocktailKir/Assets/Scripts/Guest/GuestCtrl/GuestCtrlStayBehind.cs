using UnityEngine;
using System.Collections;

public class GuestCtrlStayBehind : GuestCtrl
{
    protected interface ICtrlState
    {
        void OnAwake(GuestCtrlStayBehind ctrl);
        void OnUpdate(GuestCtrlStayBehind ctrl);
        void OnNearTarget(GuestCtrlStayBehind ctrl);
        void OnHitBallet(GuestCtrlStayBehind ctrl);
        void OnDestroyedAttractField(GuestCtrlStayBehind ctrl);
    }

    private ICtrlState m_stateCtrl;
    private void SetState<T>() where T : ICtrlState, new()
    {
        m_stateCtrl = new T();
        m_stateCtrl.OnAwake(this);
    }

    protected override void Start()
    {
        base.Start();
        m_stateCtrl = new CtrlStateEntry();
    }

    protected override void OnUpdate()
    {
        m_stateCtrl.OnUpdate(this);
    }

    protected override void OnNearTarget()
    {
        m_stateCtrl.OnNearTarget(this);
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
        public void OnAwake(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnUpdate(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnNearTarget(GuestCtrlStayBehind ctrl)
        {
            ctrl.SetState<CtrlStateWait>();
        }

        public void OnHitBallet(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlStayBehind ctrl)
        {
        }
    }

    protected class CtrlStateWait : ICtrlState
    {
        public void OnAwake(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnUpdate(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnNearTarget(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnHitBallet(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlStayBehind ctrl)
        {
        }

    }

    protected class CtrlStateStay : ICtrlState
    {
        public void OnAwake(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnUpdate(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnNearTarget(GuestCtrlStayBehind ctrl)
        {
            GameObject.Destroy(ctrl.gameObject);
        }

        public void OnHitBallet(GuestCtrlStayBehind ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlStayBehind ctrl)
        {
        }

    }

}
