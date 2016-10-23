using UnityEngine;
using System.Collections;

using XBoxInput;

namespace XVInputInternal
{
    public class XVInputController : IXVInput
    {
        XBKeyCode.UserCode m_userCode;

        private float InputToFloat(bool b)
        {
            return (b ? 1.0f : 0.0f);
        }

        public XVInputController(UserID id)
        {
            switch (id)
            {
                case UserID.User1:
                    m_userCode = XBKeyCode.UserCode.User1;
                    break;
                case UserID.User2:
                    m_userCode = XBKeyCode.UserCode.User2;
                    break;
                case UserID.User3:
                    m_userCode = XBKeyCode.UserCode.User3;
                    break;
                case UserID.User4:
                    m_userCode = XBKeyCode.UserCode.User4;
                    break;
            }
        }

        public XVInputController(XBKeyCode.UserCode id)
        {
            m_userCode = id;
        }

        public XVInputController()
        {
            AkiVACO.XLogger.LogWarning("Create XBox Controller: UserCode = Any");
            m_userCode = XBKeyCode.UserCode.Any;
        }

        public Vector2 Move()
        {
            if (IsLauncherStance())
            {
                return Vector2.zero;
            }
            else
            {
                return XBGamePad.GetAxis(XBKeyCode.Axis.LeftStick, m_userCode);
            }
        }

        public float RotateCameraH()
        {
            return (
                InputToFloat(XBGamePad.IsPressed(XBKeyCode.Button.RightShoulder, m_userCode))
                ) - (
                InputToFloat(XBGamePad.IsPressed(XBKeyCode.Button.LeftShoulder, m_userCode))
                );
        }

        public Vector2 RotateLauncher()
        {

            if (IsLauncherStance())
            {
                return XBGamePad.GetAxis(XBKeyCode.Axis.LeftStick, m_userCode);
            }
            else
            {
                return Vector2.zero;
            }
        }

        public bool IsJump()
        {
            return XBGamePad.IsTriggered(XBKeyCode.Button.A, m_userCode);
        }

        public bool IsShot()
        {
            if (IsLauncherStance())
            {
                return XBGamePad.IsPressed(XBKeyCode.Button.X, m_userCode);
            }
            return false;
        }

        public bool IsWalk()
        {
            return XBGamePad.IsPressed(XBKeyCode.Button.LeftShoulder, m_userCode);
        }

        public bool IsLauncherStance()
        {
            return (
                (XBGamePad.GetTriggerRaw(XBKeyCode.Trigger.RightTrigger, m_userCode) > 0.0f)
                || (XBGamePad.GetTriggerRaw(XBKeyCode.Trigger.LeftTrigger, m_userCode) > 0.0f));
        }

        public bool IsReload()
        {
            return XBGamePad.IsPressed(XBKeyCode.Button.B, m_userCode);
        }

    }

}   // End of namespace XVInputInternal