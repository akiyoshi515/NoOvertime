using UnityEngine;
using System.Collections;

public interface IXVInput
{
    float MoveH();
    float MoveV();
    float RotateCameraH();

    float RotateLauncherV();
    float RotateLauncherH();

    bool IsJump();
    bool IsShot();

    bool IsWalk();
}
