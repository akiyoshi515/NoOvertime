using UnityEngine;
using System.Collections;

namespace XBoxInput
{
    /// <summary>
    /// 最適化促進の為、あえて取得関数を使用
    /// </summary>
    public class XBKeyCode
    {
        public enum Button
        {
            A,
            B,
            Y,
            X,
            Back, 
            Start,
            LeftShoulder,
            RightShoulder,
            LeftStick,
            RightStick,
        }

        public enum Axis
        {
            LeftStick,
            RightStick,
            Dpad,
        }

        public enum Trigger
        {
            LeftTrigger, 
            RightTrigger,
        }

        public enum UserCode 
        {
            Any = 0, 
            User1 = 1,
            User2 = 2,
            User3 = 3,
            User4 = 4,
        }

        public static KeyCode GetKeycode(Button button, UserCode id)
        {
            switch (id)
            {
                case UserCode.User1:
                    switch (button)
                    {
                        case Button.A: return KeyCode.Joystick1Button0;
                        case Button.B: return KeyCode.Joystick1Button1;
                        case Button.X: return KeyCode.Joystick1Button2;
                        case Button.Y: return KeyCode.Joystick1Button3;
                        case Button.LeftShoulder: return KeyCode.Joystick1Button4;
                        case Button.RightShoulder: return KeyCode.Joystick1Button5;
                        case Button.Back: return KeyCode.Joystick1Button6;
                        case Button.Start: return KeyCode.Joystick1Button7;
                        case Button.LeftStick: return KeyCode.Joystick1Button8;
                        case Button.RightStick: return KeyCode.Joystick1Button9;
                    }
                    break;

                case UserCode.User2:
                    switch (button)
                    {
                        case Button.A: return KeyCode.Joystick2Button0;
                        case Button.B: return KeyCode.Joystick2Button1;
                        case Button.X: return KeyCode.Joystick2Button2;
                        case Button.Y: return KeyCode.Joystick2Button3;
                        case Button.LeftShoulder: return KeyCode.Joystick2Button4;
                        case Button.RightShoulder: return KeyCode.Joystick2Button5;
                        case Button.Back: return KeyCode.Joystick2Button6;
                        case Button.Start: return KeyCode.Joystick2Button7;
                        case Button.LeftStick: return KeyCode.Joystick2Button8;
                        case Button.RightStick: return KeyCode.Joystick2Button9;
                    }
                    break;

                case UserCode.User3:
                    switch (button)
                    {
                        case Button.A: return KeyCode.Joystick3Button0;
                        case Button.B: return KeyCode.Joystick3Button1;
                        case Button.X: return KeyCode.Joystick3Button2;
                        case Button.Y: return KeyCode.Joystick3Button3;
                        case Button.LeftShoulder: return KeyCode.Joystick3Button4;
                        case Button.RightShoulder: return KeyCode.Joystick3Button5;
                        case Button.Back: return KeyCode.Joystick3Button6;
                        case Button.Start: return KeyCode.Joystick3Button7;
                        case Button.LeftStick: return KeyCode.Joystick3Button8;
                        case Button.RightStick: return KeyCode.Joystick3Button9;
                    }
                    break;

                case UserCode.User4:
                    switch (button)
                    {
                        case Button.A: return KeyCode.Joystick4Button0;
                        case Button.B: return KeyCode.Joystick4Button1;
                        case Button.X: return KeyCode.Joystick4Button2;
                        case Button.Y: return KeyCode.Joystick4Button3;
                        case Button.LeftShoulder: return KeyCode.Joystick4Button4;
                        case Button.RightShoulder: return KeyCode.Joystick4Button5;
                        case Button.Back: return KeyCode.Joystick4Button6;
                        case Button.Start: return KeyCode.Joystick4Button7;
                        case Button.LeftStick: return KeyCode.Joystick4Button8;
                        case Button.RightStick: return KeyCode.Joystick4Button9;
                    }
                    break;

                case UserCode.Any:
                    switch (button)
                    {
                        case Button.A: return KeyCode.JoystickButton0;
                        case Button.B: return KeyCode.JoystickButton1;
                        case Button.X: return KeyCode.JoystickButton2;
                        case Button.Y: return KeyCode.JoystickButton3;
                        case Button.LeftShoulder: return KeyCode.JoystickButton4;
                        case Button.RightShoulder: return KeyCode.JoystickButton5;
                        case Button.Back: return KeyCode.JoystickButton6;
                        case Button.Start: return KeyCode.JoystickButton7;
                        case Button.LeftStick: return KeyCode.JoystickButton8;
                        case Button.RightStick: return KeyCode.JoystickButton9;
                    }
                    break;
            }
            return KeyCode.None;
        }

    }   // End of class XBKeyCode

}   // End of namespace XBoxInput