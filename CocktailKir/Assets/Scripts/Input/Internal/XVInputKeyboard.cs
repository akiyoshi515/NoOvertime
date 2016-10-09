﻿using UnityEngine;
using System.Collections;

namespace XVInputInternal
{
    public class XVInputKeyboard : IXVInput
    {
        private float InputToFloat(bool b)
        {
            return (b ? 1.0f : 0.0f);
        }

        public float MoveH()
        {
            return (InputToFloat(Input.GetKey(KeyCode.D)) - InputToFloat(Input.GetKey(KeyCode.A)));
        }

        public float MoveV()
        {
            return (InputToFloat(Input.GetKey(KeyCode.W)) - InputToFloat(Input.GetKey(KeyCode.S)));
        }

        public float RotateCameraH()
        {
            return (InputToFloat(Input.GetKey(KeyCode.E)) - InputToFloat(Input.GetKey(KeyCode.Q)));
        }

        public float RotateLauncherV()
        {
            return (InputToFloat(Input.GetKey(KeyCode.DownArrow)) - InputToFloat(Input.GetKey(KeyCode.UpArrow)));
        }

        public float RotateLauncherH()
        {
            return (InputToFloat(Input.GetKey(KeyCode.RightArrow)) - InputToFloat(Input.GetKey(KeyCode.LeftArrow)));
        }

        public bool IsJump()
        {
            return Input.GetKey(KeyCode.C);
        }

        public bool IsShot()
        {
            return Input.GetKey(KeyCode.Space);
        }
    }

}   // End of namespace XVInputInternal