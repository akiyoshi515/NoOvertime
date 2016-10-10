using UnityEngine;

namespace XBoxInput
{
    /// <summary>
    /// 入力状態の一斉取得用構造体（DPadAxis）
    /// </summary>
    public class XBStateDPad
    {
        // Dpad pushed
        public bool left = false;
        public bool right = false;
        public bool up = false;
        public bool down = false;
        
        public Vector2 axis = Vector2.zero;
    }

}   // End of namespace XBoxInput
