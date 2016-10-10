using UnityEngine;
using System.Collections;

using XVInputInternal;

namespace XBoxInput
{
    public class XBGamePad
    {
        /// <summary>
        /// [Button]を押しているか？
        /// </summary>
        public static bool IsPressed(XBKeyCode.Button button, XBKeyCode.UserCode id)
        {
            return Input.GetKey(XBKeyCode.GetKeycode(button, id));
        }

        /// <summary>
        /// [Button]を今押したか？
        /// </summary>
        public static bool IsTriggered(XBKeyCode.Button button, XBKeyCode.UserCode id)
        {
            return Input.GetKeyDown(XBKeyCode.GetKeycode(button, id));
        }

        /// <summary>
        /// [Button]を今放したか？
        /// </summary>
        public static bool IsReleased(XBKeyCode.Button button, XBKeyCode.UserCode id)
        {
            return Input.GetKeyUp(XBKeyCode.GetKeycode(button, id));
        }

        /// <summary>
        /// [Axis]の取得
        /// </summary>
        public static Vector2 GetAxis(XBKeyCode.Axis axis, XBKeyCode.UserCode id)
        {
            string val = ((int)id).ToString();
            switch (axis)
            {
                case XBKeyCode.Axis.LeftStick:
                    return GetAxisInternal(
                        XVInputConstants.LAxisX + val,
                        XVInputConstants.LAxisY + val);

                case XBKeyCode.Axis.RightStick:
                    return GetAxisInternal(
                        XVInputConstants.RAxisX + val,
                        XVInputConstants.RAxisY + val);

                case XBKeyCode.Axis.Dpad:
                    return GetAxisInternal(
                        XVInputConstants.DPadAxisX + val,
                        XVInputConstants.DPadAxisY + val);
            }

            return Vector2.zero;
        }

        /// <summary>
        /// [Axis]の取得（未加工の値）
        /// </summary>
        public static Vector2 GetAxisRaw(XBKeyCode.Axis axis, XBKeyCode.UserCode id)
        {
            string val = ((int)id).ToString();
            switch (axis)
            {
                case XBKeyCode.Axis.LeftStick:
                    return GetAxisRawInternal(
                        XVInputConstants.LAxisX + val,
                        XVInputConstants.LAxisY + val);

                case XBKeyCode.Axis.RightStick:
                    return GetAxisRawInternal(
                        XVInputConstants.RAxisX + val,
                        XVInputConstants.RAxisY + val);

                case XBKeyCode.Axis.Dpad:
                    return GetAxisRawInternal(
                        XVInputConstants.DPadAxisX + val,
                        XVInputConstants.DPadAxisY + val);
            }

            return Vector2.zero;
        }

        /// <summary>
        /// [Trigger]の取得
        /// </summary>
        public static float GetTrigger(XBKeyCode.Trigger axis, XBKeyCode.UserCode id)
        {
            string val = ((int)id).ToString();
            switch (axis)
            {
                case XBKeyCode.Trigger.LeftTrigger:
                    return GetTriggerInternal(
                        XVInputConstants.LTrigger + val);

                case XBKeyCode.Trigger.RightTrigger:
                    return GetTriggerInternal(
                        XVInputConstants.RTrigger + val);
            }

            return 0.0f;
        }

        /// <summary>
        /// [Trigger]の取得（未加工の値）
        /// </summary>
        public static float GetTriggerRaw(XBKeyCode.Trigger axis, XBKeyCode.UserCode id)
        {
            string val = ((int)id).ToString();
            switch (axis)
            {
                case XBKeyCode.Trigger.LeftTrigger:
                    return GetTriggerRawInternal(
                        XVInputConstants.LTrigger + val);

                case XBKeyCode.Trigger.RightTrigger:
                    return GetTriggerRawInternal(
                        XVInputConstants.RTrigger + val);
            }

            return 0.0f;
        }

        /// <summary>
        /// [ボタン]の情報を一括取得
        /// </summary>
        public static XBStateButton GetStateButton(XBKeyCode.UserCode id)
        {
            XBStateButton sts = new XBStateButton();
            sts.A = XBGamePad.IsPressed(XBKeyCode.Button.A, id);
            sts.B = XBGamePad.IsPressed(XBKeyCode.Button.B, id);
            sts.X = XBGamePad.IsPressed(XBKeyCode.Button.X, id);
            sts.Y = XBGamePad.IsPressed(XBKeyCode.Button.Y, id);
            sts.start = XBGamePad.IsPressed(XBKeyCode.Button.Start, id);
            sts.back = XBGamePad.IsPressed(XBKeyCode.Button.Back, id);
            return sts;
        }

        /// <summary>
        /// [Shoulder & Trigger]の情報を一括取得
        /// </summary>
        /// <param name="raw">未加工の値にするか？</param>
        public static XBStateShoulder GetStateShoulder(XBKeyCode.UserCode id, bool raw = false)
        {
            XBStateShoulder sts = new XBStateShoulder();

            sts.shoulderL = XBGamePad.IsPressed(XBKeyCode.Button.LeftShoulder, id);
            sts.shoulderR = XBGamePad.IsPressed(XBKeyCode.Button.RightShoulder, id);

            if (raw)
            {
                sts.triggerL = XBGamePad.GetTriggerRaw(XBKeyCode.Trigger.LeftTrigger, id);
                sts.triggerR = XBGamePad.GetTriggerRaw(XBKeyCode.Trigger.RightTrigger, id);
            }
            else
            {
                sts.triggerL = XBGamePad.GetTrigger(XBKeyCode.Trigger.LeftTrigger, id);
                sts.triggerR = XBGamePad.GetTrigger(XBKeyCode.Trigger.RightTrigger, id);
            }

            return sts;
        }

        /// <summary>
        /// [左スティック]の情報を一括取得
        /// </summary>
        /// <param name="raw">未加工の値にするか？</param>
        public static XBStateAxis GetStateAxisL(XBKeyCode.UserCode id, bool raw = false)
        {
            XBStateAxis sts = new XBStateAxis();

            sts.stickPressed = XBGamePad.IsPressed(XBKeyCode.Button.LeftStick, id);

            if (raw)
            {
                sts.axis = XBGamePad.GetAxisRaw(XBKeyCode.Axis.LeftStick, id);
            }
            else
            {
                sts.axis = XBGamePad.GetAxis(XBKeyCode.Axis.LeftStick, id);
            }

            return sts;
        }

        /// <summary>
        /// [右スティック]の情報を一括取得
        /// </summary>
        /// <param name="raw">未加工の値にするか？</param>
        public static XBStateAxis GetStateAxisR(XBKeyCode.UserCode id, bool raw = false)
        {
            XBStateAxis sts = new XBStateAxis();

            sts.stickPressed = XBGamePad.IsPressed(XBKeyCode.Button.RightStick, id);
            if (raw)
            {
                sts.axis = XBGamePad.GetAxisRaw(XBKeyCode.Axis.RightStick, id);
            }
            else
            {
                sts.axis = XBGamePad.GetAxis(XBKeyCode.Axis.RightStick, id);
            }

            return sts;
        }

        /// <summary>
        /// [十字キー]の情報を一括取得
        /// </summary>
        /// <param name="raw">未加工の値にするか？</param>
        public static XBStateDPad GetStateDPad(XBKeyCode.UserCode id, bool raw = false)
        {
            XBStateDPad sts = new XBStateDPad();

            if (raw)
            {
                sts.axis = XBGamePad.GetAxisRaw(XBKeyCode.Axis.Dpad, id);
            }
            else
            {
                sts.axis = XBGamePad.GetAxis(XBKeyCode.Axis.Dpad, id);
            }

            sts.left = (sts.axis.x < 0);
            sts.right = (sts.axis.x > 0);
            sts.up = (sts.axis.y > 0);
            sts.down = (sts.axis.y < 0);

            return sts;
        }

        /// <summary>
        /// すべての入力情報を一括取得
        /// </summary>
        /// <param name="raw">未加工の値にするか？</param>
        public static XBState GetState(XBKeyCode.UserCode id, bool raw = false)
        {
            XBState sts = new XBState();

            sts.button = XBGamePad.GetStateButton(id);
            sts.shoulders = XBGamePad.GetStateShoulder(id, raw);
            sts.axisL = XBGamePad.GetStateAxisL(id, raw);
            sts.axisR = XBGamePad.GetStateAxisR(id, raw);
            sts.dpad = XBGamePad.GetStateDPad(id, raw);

            return sts;
        }

        /// <summary>
        /// [Internal] GetAxis
        /// </summary>
        protected static Vector2 GetAxisInternal(string strX, string strY)
        {
            Vector2 vec = Vector2.zero;

            try
            {
                vec.x = Input.GetAxis(strX);
                vec.y = -Input.GetAxis(strY);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            return vec;
        }

        /// <summary>
        /// [Internal] GetAxisRaw
        /// </summary>
        protected static Vector2 GetAxisRawInternal(string strX, string strY)
        {
            Vector2 vec = Vector2.zero;

            try
            {
                vec.x = Input.GetAxisRaw(strX);
                vec.y = -Input.GetAxisRaw(strY);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            return vec;
        }

        /// <summary>
        /// [Internal] GetTrigger
        /// </summary>
        protected static float GetTriggerInternal(string str)
        {
            float val = 0.0f;

            try
            {
                val = Input.GetAxis(str);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            return val;
        }

        /// <summary>
        /// [Internal] GetTriggerRaw
        /// </summary>
        protected static float GetTriggerRawInternal(string str)
        {
            float val = 0.0f;

            try
            {
                val = Input.GetAxisRaw(str);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            return val;
        }

    }

}   // End of namespace XBoxInput