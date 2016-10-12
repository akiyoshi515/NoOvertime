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
            if (IsLauncherStance())
            {
                return Vector2.zero;
            }
            else
            {
                return new Vector2(
                    (InputToFloat(Input.GetKey(KeyCode.RightArrow)) - InputToFloat(Input.GetKey(KeyCode.LeftArrow))),
                    (InputToFloat(Input.GetKey(KeyCode.UpArrow)) - InputToFloat(Input.GetKey(KeyCode.DownArrow))));
            }
        }

        public float RotateCameraH()
        {
            return (InputToFloat(Input.GetKey(KeyCode.D)) - InputToFloat(Input.GetKey(KeyCode.A)));
        }

        public Vector2 RotateLauncher()
        {
            if (IsLauncherStance())
            {
                return new Vector2(
                    (InputToFloat(Input.GetKey(KeyCode.RightArrow)) - InputToFloat(Input.GetKey(KeyCode.LeftArrow))),
                    (InputToFloat(Input.GetKey(KeyCode.UpArrow)) - InputToFloat(Input.GetKey(KeyCode.DownArrow))));
            }
            else
            {
                return Vector2.zero;
            }
        }

        public bool IsJump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool IsShot()
        {
            if (IsLauncherStance())
            {
                return Input.GetKeyDown(KeyCode.C);
            }
            return false;
        }

        public bool IsWalk()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }

        public bool IsLauncherStance()
        {
            return Input.GetKey(KeyCode.Z);
        }

        public bool IsReload()
        {
            return Input.GetKey(KeyCode.X);
        }

    }

}   // End of namespace XVInputInternal