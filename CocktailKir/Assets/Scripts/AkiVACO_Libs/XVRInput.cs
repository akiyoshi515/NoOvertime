
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

#if UNITY_ANDROID
    public enum XVRKeyCode
    {
        // Number 
        Num1 = UnityEngine.KeyCode.Alpha1,
        Num2 = UnityEngine.KeyCode.Alpha2,
        Num3 = UnityEngine.KeyCode.Alpha3,
        Num4 = UnityEngine.KeyCode.Alpha4,
        Num5 = UnityEngine.KeyCode.Alpha5,
        Num6 = UnityEngine.KeyCode.Alpha6,
        Num7 = UnityEngine.KeyCode.Alpha7,
        Num8 = UnityEngine.KeyCode.Alpha8,
        Num9 = UnityEngine.KeyCode.Alpha9,
        Num10 = UnityEngine.KeyCode.Alpha0,
        Num11 = UnityEngine.KeyCode.LeftShift,
        Num12 = UnityEngine.KeyCode.Slash,

        // Arrow
        Up          = UnityEngine.KeyCode.UpArrow,
        Down        = UnityEngine.KeyCode.DownArrow,
        Left        = UnityEngine.KeyCode.LeftArrow,
        Right       = UnityEngine.KeyCode.RightArrow,
        LeftUp      = UnityEngine.KeyCode.Keypad7,
        LeftDown    = UnityEngine.KeyCode.Keypad1,
        RightUp     = UnityEngine.KeyCode.Keypad9,
        RightDown   = UnityEngine.KeyCode.Keypad3,

        // etc.
        Select = UnityEngine.KeyCode.JoystickButton0,
        Back = UnityEngine.KeyCode.Escape,

    };
#else
    public enum XVRKeyCode
    {
        // Number 
        Num1 = UnityEngine.KeyCode.Alpha1,
        Num2 = UnityEngine.KeyCode.Alpha2,
        Num3 = UnityEngine.KeyCode.Alpha3,
        Num4 = UnityEngine.KeyCode.Alpha4,
        Num5 = UnityEngine.KeyCode.Alpha5,
        Num6 = UnityEngine.KeyCode.Alpha6,
        Num7 = UnityEngine.KeyCode.Alpha7,
        Num8 = UnityEngine.KeyCode.Alpha8,
        Num9 = UnityEngine.KeyCode.Alpha9,
        Num10 = UnityEngine.KeyCode.Alpha0,
        Num11 = UnityEngine.KeyCode.Minus,
        Num12 = UnityEngine.KeyCode.LeftShift,
        
        // Arrow
        Up          = UnityEngine.KeyCode.UpArrow,
        Down        = UnityEngine.KeyCode.DownArrow,
        Left        = UnityEngine.KeyCode.LeftArrow,
        Right       = UnityEngine.KeyCode.RightArrow,
        LeftUp      = UnityEngine.KeyCode.Keypad7,
        LeftDown    = UnityEngine.KeyCode.Keypad1,
        RightUp     = UnityEngine.KeyCode.Keypad9,
        RightDown   = UnityEngine.KeyCode.Keypad3,

        // etc
        Select = UnityEngine.KeyCode.Return,
        Back = UnityEngine.KeyCode.Escape,

    };
#endif

#if DEBUG

    public class XVRInput
    {
        private static bool m_dbgenable = false;

        public static bool enabledDebug
        {
            get { return m_dbgenable; }
            private set { m_dbgenable = value; }
        }

        public static bool IsHold(XVRKeyCode key)
        {
            return Input.GetKey((UnityEngine.KeyCode)key) && (!m_dbgenable);
        }

        public static bool IsTrig(XVRKeyCode key)
        {
            return Input.GetKeyDown((UnityEngine.KeyCode)key) && (!m_dbgenable);
        }

        public static bool IsRelease(XVRKeyCode key)
        {
            return Input.GetKeyUp((UnityEngine.KeyCode)key) && (!m_dbgenable);
        }

        public static bool IsHoldDbg(XVRKeyCode key)
        {
            return Input.GetKey((UnityEngine.KeyCode)key) && (m_dbgenable);
        }

        public static bool IsTrigDbg(XVRKeyCode key)
        {
            return Input.GetKeyDown((UnityEngine.KeyCode)key) && (m_dbgenable);
        }

        public static bool IsReleaseDbg(XVRKeyCode key)
        {
            return Input.GetKeyUp((UnityEngine.KeyCode)key) && (m_dbgenable);
        }

        public static bool IsHoldForced(XVRKeyCode key)
        {
            return Input.GetKey((UnityEngine.KeyCode)key);
        }

        public static bool IsTrigForced(XVRKeyCode key)
        {
            return Input.GetKeyDown((UnityEngine.KeyCode)key);
        }

        public static bool IsReleaseForced(XVRKeyCode key)
        {
            return Input.GetKeyUp((UnityEngine.KeyCode)key);
        }
    
        public static void EnableDebug(bool enabled)
        {
            enabledDebug = enabled;
        }


    }

#else

    public class XVRInput
    {

        public static bool enabledDebug
        {
            get { return false; }
        }

        public static bool IsHold(XVRKeyCode key)
        {
            return Input.GetKey((UnityEngine.KeyCode)key);
        }

        public static bool IsTrig(XVRKeyCode key)
        {
            return Input.GetKeyDown((UnityEngine.KeyCode)key);
        }

        public static bool IsRelease(XVRKeyCode key)
        {
            return Input.GetKeyUp((UnityEngine.KeyCode)key);
        }
    
        public static bool IsHoldDbg(XVRKeyCode key)
        {
            return false;
        }

        public static bool IsTrigDbg(XVRKeyCode key)
        {
            return false;
        }

        public static bool IsReleaseDbg(XVRKeyCode key)
        {
            return false;
        }

        public static void Enable(bool enabled)
        {
            // Empty
        }

        public static bool IsHoldForced(XVRKeyCode key)
        {
            return Input.GetKey((UnityEngine.KeyCode)key);
        }

        public static bool IsTrigForced(XVRKeyCode key)
        {
            return Input.GetKeyDown((UnityEngine.KeyCode)key);
        }

        public static bool IsReleaseForced(XVRKeyCode key)
        {
            return Input.GetKeyUp((UnityEngine.KeyCode)key);
        }

        public static void EnableDebug(bool enabled)
        {
        }
    }

#endif

}   // End of namespace AkiVACO
