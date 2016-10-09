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

        public float MoveCameraH()
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
    }

}   // End of namespace XVInputInternal