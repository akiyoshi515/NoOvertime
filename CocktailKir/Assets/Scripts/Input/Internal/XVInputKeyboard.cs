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
                (InputToFloat(Input.GetKey(KeyCode.RightArrow)) - InputToFloat(Input.GetKey(KeyCode.LeftArrow))),
                (InputToFloat(Input.GetKey(KeyCode.UpArrow)) - InputToFloat(Input.GetKey(KeyCode.DownArrow))));
        }

        public float RotateCameraH()
        {
            return (InputToFloat(Input.GetKey(KeyCode.D)) - InputToFloat(Input.GetKey(KeyCode.A)));
        }

        public Vector2 RotateLauncher()
        {
            return new Vector2(
                (InputToFloat(Input.GetKey(KeyCode.RightArrow)) - InputToFloat(Input.GetKey(KeyCode.LeftArrow))),
                (InputToFloat(Input.GetKey(KeyCode.UpArrow)) - InputToFloat(Input.GetKey(KeyCode.DownArrow))));
        }

        public bool IsJump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool IsShot()
        {
            return Input.GetKeyDown(KeyCode.C);
        }

        public bool IsShotHolded()
        {
            return Input.GetKey(KeyCode.C);
        }

        public bool IsLauncherStance()
        {
            return Input.GetKey(KeyCode.Z);
        }

        public bool IsReload()
        {
            return Input.GetKeyDown(KeyCode.X);
        }

        public bool Dbg_IsUnlimitedBullet()
        {
#if DEBUG
            return Input.GetKeyDown(KeyCode.W);
#else
            return false;
#endif
        }

        public bool Dbg_IsShot3Way()
        {
#if DEBUG
            return Input.GetKeyDown(KeyCode.Q);
#else
            return false;
#endif
        }

        public bool Dbg_IsReloadBonusCharm()
        {
#if DEBUG
            return Input.GetKeyDown(KeyCode.E);
#else
            return false;
#endif
        }

    }

}   // End of namespace XVInputInternal