using UnityEngine;
using System.Collections;

namespace XVInputInternal
{
    public class XVInputNone : IXVInput
    {
        public Vector2 Move()
        {
            return Vector2.zero;
        }

        public float RotateCameraH()
        {
            return 0.0f;
        }

        public Vector2 RotateLauncher()
        {
            return Vector2.zero;
        }

        public bool IsJump()
        {
            return false;
        }

        public bool IsShot()
        {
            return false;
        }

        public bool IsLauncherStance()
        {
            return false;
        }

        public bool IsReload()
        {
            return false;
        }

        public bool Dbg_IsUnlimitedBullet()
        {
            return false;
        }

        public bool Dbg_IsShot3Way()
        {
            return false;
        }

        public bool Dbg_IsReloadBonusCharm()
        {
            return false;
        }

    }

}   // End of namespace XVInputInternal