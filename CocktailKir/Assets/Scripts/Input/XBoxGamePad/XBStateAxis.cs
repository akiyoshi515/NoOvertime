using UnityEngine;

namespace XBoxInput
{
    /// <summary>
    /// 入力状態の一斉取得用構造体（Axis）
    /// </summary>
    public class XBStateAxis
    {        
        // Stick pushed
        public bool stickPressed = false;

        public Vector2 axis = Vector2.zero;
    }

}   // End of namespace XBoxInput
