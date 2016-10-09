using UnityEngine;
using System.Collections;

namespace XVInputInternal
{
    public class XVInputNone : IXVInput
    {

        public float MoveH()
        {
            return 0.0f;
        }

        public float MoveV()
        {
            return 0.0f;
        }

        public float RotateCameraH()
        {
            return 0.0f;
        }

        public float RotateLauncherV()
        {
            return 0.0f;
        }

        public float RotateLauncherH()
        {
            return 0.0f;
        }

        public bool IsJump()
        {
            return false;
        }

        public bool IsShot()
        {
            return false;
        }

        public bool IsWalk()
        {
            return false;
        }
    }

}   // End of namespace XVInputInternal