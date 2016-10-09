using UnityEngine;
using System.Collections;

namespace XVInputInternal
{
    public interface IXVInput
    {
        float MoveH();
        float MoveV();
        float MoveCameraH();

        bool IsJump();
        bool IsShot();

    }

}   // End of namespace XVInputInternal