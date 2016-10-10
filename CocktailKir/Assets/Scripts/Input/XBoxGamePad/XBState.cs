using UnityEngine;

namespace XBoxInput
{
    /// <summary>
    /// 入力状態の一斉取得用構造体（負荷高）
    /// </summary>
    public class XBState
    {
        public XBStateButton button = new XBStateButton();
        public XBStateShoulder shoulders = new XBStateShoulder();
        public XBStateAxis axisL = new XBStateAxis();
        public XBStateAxis axisR = new XBStateAxis();
        public XBStateDPad dpad = new XBStateDPad();
    }

}   // End of namespace XBoxInput
