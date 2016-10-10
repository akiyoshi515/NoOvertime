using UnityEngine;
using System.Collections;

namespace XVInputInternal
{
    public class XVInputKeyboard : IXVInput
    {
        private float InputToFloat(bool b)
        {
            return (b ? 1.0f : 0.0f);
        }

        public Vector2 Move()
        {
            return new Vector2(
                (InputToFloat(Input.GetKey(KeyCode.D)) - InputToFloat(Input.GetKey(KeyCode.A))),
                (InputToFloat(Input.GetKey(KeyCode.W)) - InputToFloat(Input.GetKey(KeyCode.S))));
        }

        public float RotateCameraH()
        {
            return (InputToFloat(Input.GetKey(KeyCode.E)) - InputToFloat(Input.GetKey(KeyCode.Q)));
        }

        public Vector2 RotateLauncher()
        {
            return new Vector2(
                (InputToFloat(Input.GetKey(KeyCode.RightArrow)) - InputToFloat(Input.GetKey(KeyCode.LeftArrow))),
                (InputToFloat(Input.GetKey(KeyCode.UpArrow)) - InputToFloat(Input.GetKey(KeyCode.DownArrow))));
        }

        public bool IsJump()
        {
            return Input.GetKey(KeyCode.C);
        }

        public bool IsShot()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool IsWalk()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
    }

}   // End of namespace XVInputInternal