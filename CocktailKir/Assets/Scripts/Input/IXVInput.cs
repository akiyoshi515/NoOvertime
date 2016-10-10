using UnityEngine;
using System.Collections;

public interface IXVInput
{
    Vector2 Move();
    float RotateCameraH();

    Vector2 RotateLauncher();

    bool IsJump();
    bool IsShot();

    bool IsWalk();

}
