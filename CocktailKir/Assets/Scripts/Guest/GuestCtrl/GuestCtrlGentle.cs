using UnityEngine;
using System.Collections;

public class GuestCtrlGentle : GuestCtrl
{
    protected interface ICtrlState
    {
        void OnAwake(GuestCtrlGentle ctrl);
        void OnUpdate(GuestCtrlGentle ctrl);
        void OnNearTarget(GuestCtrlGentle ctrl);
        void OnHitBullet(GuestCtrlGentle ctrl);
        void OnDestroyedAttractField(GuestCtrlGentle ctrl);
    }

    private ICtrlState m_stateCtrl;
    private void SetState<T>() where T : ICtrlState, new()
    {
        m_stateCtrl = new T();
        m_stateCtrl.OnAwake(this);
    }

    [SerializeField]
    private float m_waitTime = 15.0f;

    [SerializeField]
    private float m_stayTime = 15.0f;

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

    protected override void OnNearTarget()
    {
        m_stateCtrl.OnNearTarget(this);
    }

    protected override void OnHitBullet()
    {
        m_stateCtrl.OnHitBullet(this);
    }

    protected override void OnDestroyedAttractField()
    {
        m_stateCtrl.OnDestroyedAttractField(this);
    }

    protected class CtrlStateEntry : ICtrlState
    {
        public void OnAwake(GuestCtrlGentle ctrl)
        {
        }

        public void OnUpdate(GuestCtrlGentle ctrl)
        {
        }

        public void OnNearTarget(GuestCtrlGentle ctrl)
        {
            ctrl.SetState<CtrlStateWait>();
        }

        public void OnHitBullet(GuestCtrlGentle ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlGentle ctrl)
        {
        }
    }

    protected class CtrlStateWait : ICtrlState
    {
        public void OnAwake(GuestCtrlGentle ctrl)
        {
            ctrl.m_time = ctrl.m_waitTime;
        }

        public void OnUpdate(GuestCtrlGentle ctrl)
        {
            ctrl.m_time -= Time.deltaTime;
            if (ctrl.m_time <= 0.0f)
            {
                ctrl.SetState<CtrlStateExit>();
            }
        }

        public void OnNearTarget(GuestCtrlGentle ctrl)
        {

        }

        public void OnHitBullet(GuestCtrlGentle ctrl)
        {
            ctrl.SetState<CtrlStateStay>();
        }

        public void OnDestroyedAttractField(GuestCtrlGentle ctrl)
        {
        }

    }

    protected class CtrlStateStay : ICtrlState
    {
        public void OnAwake(GuestCtrlGentle ctrl)
        {
            ctrl.m_time = ctrl.m_stayTime;
        }

        public void OnUpdate(GuestCtrlGentle ctrl)
        {
            ctrl.m_time -= Time.deltaTime;
            if (ctrl.m_time <= 0.0f)
            {
                ctrl.SetState<CtrlStateExit>();
            }
        }

        public void OnNearTarget(GuestCtrlGentle ctrl)
        {

        }

        public void OnHitBullet(GuestCtrlGentle ctrl)
        {
            ctrl.m_time = ctrl.m_stayTime;
        }

        public void OnDestroyedAttractField(GuestCtrlGentle ctrl)
        {
        }

    }

    protected class CtrlStateExit : ICtrlState
    {
        public void OnAwake(GuestCtrlGentle ctrl)
        {
            ctrl.SetNavTarget(ctrl.m_goOutDestination);
        }

        public void OnUpdate(GuestCtrlGentle ctrl)
        {
        }

        public void OnNearTarget(GuestCtrlGentle ctrl)
        {
            GameObject.Destroy(ctrl.gameObject);
        }

        public void OnHitBullet(GuestCtrlGentle ctrl)
        {
        }

        public void OnDestroyedAttractField(GuestCtrlGentle ctrl)
        {
            ctrl.SetNavTarget(ctrl.m_goOutDestination);
        }

    }

}
