using UnityEngine;
using System.Collections;
using UnityEditor;


// エディタに独自のウィンドウを作成する
public class EditorExWindow : EditorWindow
{

    int radio = 0;

    bool node = false;

    bool titleLeft = false;
    bool titleMid = false;
    bool titleRight = false;

    // メニューのWindowにEditorExという項目を追加。
    [MenuItem("Window/HondyEditor/EditorEx")]
    static void Open()
    {
        // メニューのWindow/EditorExを選択するとOpen()が呼ばれる。
        // 表示させたいウィンドウは基本的にGetWindow()で表示＆取得する。
        EditorWindow.GetWindow<EditorExWindow>("HondyEditor/EditorEx"); // タイトル名を"EditorEx"に指定（後からでも変えられるけど）
    }

    // Windowのクライアント領域のGUI処理を記述
    void OnGUI()
    {
        EditorGUILayout.LabelField("ようこそ！　Unityエディタ拡張の沼へ！", GUI.skin.box); // いつまでも居座り続けるぜ！

        // ボタンなのにラベルの見た目。
        if (GUILayout.Button("Buttonだよ！", GUI.skin.label))
        {
            Debug.Log("Buttonだよ！");
        }

        EditorGUILayout.BeginHorizontal();
        {
            // 小さいボタンで、さらに左右がぴったりつながる。
            if (GUILayout.Button("Left", EditorStyles.miniButtonLeft))
            {
                Debug.Log("Left");
            }
            if (GUILayout.Button("Mid", EditorStyles.miniButtonMid))
            {
                Debug.Log("Mid");
            }
            if (GUILayout.Button("Right", EditorStyles.miniButtonRight))
            {
                Debug.Log("Right");
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            // ラジオボタンとか
            EditorGUILayout.PrefixLabel("Radio");

            for (int i = 0; i < 3; i++)
            {
                if (EditorGUILayout.Toggle(i == radio, EditorStyles.radioButton))
                {
                    radio = i;
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        // SelectionGridでも使える。
        radio = GUILayout.SelectionGrid(radio, new string[] { "Radio1", "Radio2", "Radio3" }, 1, EditorStyles.radioButton);

        EditorGUILayout.BeginHorizontal();
        {
            // Show Built In Resourcesから適当に使いたいの選ぶ。
            // できれば、GUIStyleはstaticとかで事前にもっといたほうがいいかも
            titleLeft = GUILayout.Toggle(titleLeft, "Left", (GUIStyle)"OL Titleleft");
            titleMid = GUILayout.Toggle(titleMid, "Mid", (GUIStyle)"OL Titlemid");
            titleRight = GUILayout.Toggle(titleRight, "Right", (GUIStyle)"OL Titleright");
        }
        EditorGUILayout.EndHorizontal();

        // 自分で切り替える必要あったりもする。
        GUIStyle nodeStyle = node ? (GUIStyle)"flow node hex 1 on" : (GUIStyle)"flow node hex 1";
        if (GUILayout.Button("node", nodeStyle))
        {
            node = !node;
        }
    }
}