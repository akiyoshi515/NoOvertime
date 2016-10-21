
// 引用元：http://notargs.com/blog/blog/2015/01/23/92/

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;

using XVInputInternal;

namespace NotargsInputManagerUtil
{
    /// <summary>
    /// InputManagerを自動的に設定してくれるクラス
    /// </summary>
    public class InputManagerSetter
    {
        /// <summary>
        /// インプットマネージャーを再設定します。
        /// </summary>
        [MenuItem("Utility/Reset InputManager for XBox")]
        public static void ResetInputManager()
        {
            Debug.Log("インプットマネージャーの設定を開始します。");
            InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

            Debug.Log("設定を全てクリアします。");
            inputManagerGenerator.Clear();

            Debug.Log("プレイヤーごとの設定を追加します。");
            for (int i = 0; i < 4; i++)
            {
                AddPlayerInputSettingsForXBox(inputManagerGenerator, i);
            }

            Debug.Log("グローバル設定を追加します。");
            AddGlobalInputSettings(inputManagerGenerator);

            Debug.Log("スタンダードな設定を追加します。");
            AddStdInputSettings(inputManagerGenerator);

            Debug.Log("インプットマネージャーの設定が完了しました。");
        }

        /// <summary>
        /// グローバルな入力設定を追加する（OK、キャンセルなど）
        /// </summary>
        /// <param name="inputManagerGenerator">Input manager generator.</param>
        private static void AddGlobalInputSettings(InputManagerGenerator inputManagerGenerator)
        {
            /*
            // 横方向
            {
                var name = "Horizontal";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 1));
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "a", "d", "left", "right"));
            }
            */
        }

        /// <summary>
        /// プレイヤーごとの入力設定を追加する
        /// </summary>
        /// <param name="inputManagerGenerator">Input manager generator.</param>
        /// <param name="playerIndex">Player index.</param>
        private static void AddPlayerInputSettingsForXBox(InputManagerGenerator inputManagerGenerator, int playerIndex)
        {
            if (playerIndex < 0 || playerIndex > 3)
            {
                Debug.LogError("プレイヤーインデックスの値が不正です。");
            }

            int joystickNum = playerIndex + 1;

            string strIndex = joystickNum.ToString();

            // 左スティック
            {
                inputManagerGenerator.AddAxis(
                    InputAxis.CreatePadAxis(XVInputConstants.LAxisX + strIndex, joystickNum, 1));
                inputManagerGenerator.AddAxis(
                    InputAxis.CreatePadAxis(XVInputConstants.LAxisY + strIndex, joystickNum, 2));
            }

            // 右スティック
            {
                inputManagerGenerator.AddAxis(
                    InputAxis.CreatePadAxis(XVInputConstants.RAxisX + strIndex, joystickNum, 4));
                inputManagerGenerator.AddAxis(
                    InputAxis.CreatePadAxis(XVInputConstants.RAxisY + strIndex, joystickNum, 5));
            }

            // 十字キー
            {
                inputManagerGenerator.AddAxis(
                    InputAxis.CreatePadAxis(XVInputConstants.DPadAxisX + strIndex, joystickNum, 6));
                inputManagerGenerator.AddAxis(
                    InputAxis.CreatePadAxis(XVInputConstants.DPadAxisY + strIndex, joystickNum, 7));
            }

            // トリガー
            {
                inputManagerGenerator.AddAxis(
                    InputAxis.CreatePadAxis(XVInputConstants.LTrigger + strIndex, joystickNum, 3));
                inputManagerGenerator.AddAxis(
                    InputAxis.CreatePadAxis(XVInputConstants.RTrigger + strIndex, joystickNum, 3));
            }

        }

        /// <summary>
        /// デフォルトのEventSystemで扱う入力設定を追加する
        /// </summary>
        /// <param name="inputManagerGenerator">Input manager generator.</param>
        private static void AddStdInputSettings(InputManagerGenerator inputManagerGenerator)
        {
            // 横方向
            {
                var name = "Horizontal";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "a", "d", "left", "right"));
            }

            // 縦方向
            {
                var name = "Vertical";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "s", "w", "down", "up"));
            }

            // 決定
            {
                var name = "Submit";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "", "enter", "", "space"));
            }

            // キャンセル
            {
                var name = "Cancel";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "", "escape", "", "joystick button 1"));
            }

        }

        /// <summary>
        /// キーボードでプレイした場合、割り当たっているキーを取得する
        /// </summary>
        /// <param name="upKey">Up key.</param>
        /// <param name="downKey">Down key.</param>
        /// <param name="leftKey">Left key.</param>
        /// <param name="rightKey">Right key.</param>
        /// <param name="attackKey">Attack key.</param>
        /// <param name="playerIndex">Player index.</param>
        private static void GetAxisKey(out string upKey, out string downKey, out string leftKey, out string rightKey, out string attackKey, int playerIndex)
        {
            upKey = "";
            downKey = "";
            leftKey = "";
            rightKey = "";
            attackKey = "";

            switch (playerIndex)
            {
                case 0:
                    upKey = "w";
                    downKey = "s";
                    leftKey = "a";
                    rightKey = "d";
                    attackKey = "e";
                    break;
                case 1:
                    upKey = "i";
                    downKey = "k";
                    leftKey = "j";
                    rightKey = "l";
                    attackKey = "o";
                    break;
                case 2:
                    upKey = "up";
                    downKey = "down";
                    leftKey = "left";
                    rightKey = "right";
                    attackKey = "[0]";
                    break;
                case 3:
                    upKey = "[8]";
                    downKey = "[5]";
                    leftKey = "[4]";
                    rightKey = "[6]";
                    attackKey = "[9]";
                    break;
                default:
                    Debug.LogError("プレイヤーインデックスの値が不正です。");
                    upKey = "";
                    downKey = "";
                    leftKey = "";
                    rightKey = "";
                    attackKey = "";
                    break;
            }
        }
    }

}

#endif
