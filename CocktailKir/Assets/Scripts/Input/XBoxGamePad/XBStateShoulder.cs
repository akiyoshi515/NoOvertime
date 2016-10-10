using UnityEngine;

namespace XBoxInput
{
    /// <summary>
    /// 入力状態の一斉取得用構造体（Shoulder）
    /// </summary>
    public class XBStateShoulder
    {
        // Shoulder
        public bool shoulderR = false;
        public bool shoulderL = false;

        // Trigger
        public float triggerL = 0.0f;
        public float triggerR = 0.0f;
    }

}   // End of namespace XBoxInput
