using UnityEngine;
using System.Collections;

public interface IXVInput
{
    /// <summary>
    /// 移動
    /// </summary>
    Vector2 Move();

    /// <summary>
    /// カメラ回転
    /// </summary>
    float RotateCameraH();

    /// <summary>
    /// ランチャー回転
    /// </summary>
    Vector2 RotateLauncher();

    /// <summary>
    /// ジャンプキーを押しているか？
    /// </summary>
    bool IsJump();

    /// <summary>
    /// 射撃キーを押しているか？
    /// </summary>
    bool IsShot();

    /// <summary>
    /// 歩きキーを押しているか？
    /// </summary>
    bool IsWalk();

    /// <summary>
    /// 構えキーを押しているか？
    /// </summary>
    bool IsLauncherStance();

    /// <summary>
    /// リロードキーを押しているか？
    /// </summary>
    bool IsReload();

    /// <summary>
    /// Debug 3Way射撃
    /// </summary>
    bool Dbg_IsShot3Way();

}
