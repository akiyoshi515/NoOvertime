using UnityEngine;
using System.Collections;
using UnityEditor;

// 
// // エディタに独自のウィンドウを作成する
// public class EditorExWindow : EditorWindow
// {
// 
//     int radio = 0;
// 
//     bool node = false;
// 
//     bool titleLeft = false;
//     bool titleMid = false;
//     bool titleRight = false;
// 
//     // メニューのWindowにEditorExという項目を追加。
//     [MenuItem("Window/HondyEditor/EditorEx")]
//     static void Open()
//     {
//         // メニューのWindow/EditorExを選択するとOpen()が呼ばれる。
//         // 表示させたいウィンドウは基本的にGetWindow()で表示＆取得する。
//         EditorWindow.GetWindow<EditorExWindow>("HondyEditor/EditorEx"); // タイトル名を"EditorEx"に指定（後からでも変えられるけど）
//     }
// 
//     [MenuItem("Window/HondyEditor/Create/NewPrefab")]
//     public static void CreatePrefab()
//     {
//         string name = "target";
//         string outputPath = "Assets/Hondy/Prefab.prefab";
// 
//         GameObject gameObject = EditorUtility.CreateGameObjectWithHideFlags(name, HideFlags.HideInHierarchy);
//         
// 
//         PrefabUtility.CreatePrefab(outputPath, gameObject);
// 
//         Editor.DestroyImmediate(gameObject);
//     }

// 
//     // textureを作成
//     [MenuItem("Window/HondyEditor/Create/BakeTexuture")]
//     public static void BakeTexture()
//     {
//         string path  = ("Assets/Hondy/tetTexture.png");
//         
// 
//         Texture2D texture = new Texture2D();
//         // textureが与えられたとして
//         var temp = RenderTexture.GetTemporary(texture.width, texture.height);
//         Graphics.Blit(texture, temp);
//         // ReadPixelsで直前のレンダリング結果を読み込める
//         var copy = new Texture2D(texture.width, texture.height);
//         copy.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
//         RenderTexture.ReleaseTemporary(temp);
// 
// 
//         // PNGとして書き出し
//         System.IO.File.WriteAllBytes(path, texture.EncodeToPNG());
//         Object.DestroyImmediate(texture);    // 破棄を忘れずに
//                                              // assetファイルとして書き出し
//         AssetDatabase.CreateAsset(texture, path);
//     }
// 
// 
//     // Windowのクライアント領域のGUI処理を記述
//     void OnGUI()
//     {
//         EditorGUILayout.LabelField("ようこそ！　Unityエディタ拡張の沼へ！", GUI.skin.box); // いつまでも居座り続けるぜ！
// 
//         // ボタンなのにラベルの見た目。
//         if (GUILayout.Button("Buttonだよ！", GUI.skin.label))
//         {
//             Debug.Log("Buttonだよ！");
//         }
// 
//         EditorGUILayout.BeginHorizontal();
//         {
//             // 小さいボタンで、さらに左右がぴったりつながる。
//             if (GUILayout.Button("Left", EditorStyles.miniButtonLeft))
//             {
//                 Debug.Log("Left");
//             }
//             if (GUILayout.Button("Mid", EditorStyles.miniButtonMid))
//             {
//                 Debug.Log("Mid");
//             }
//             if (GUILayout.Button("Right", EditorStyles.miniButtonRight))
//             {
//                 Debug.Log("Right");
//             }
//         }
//         EditorGUILayout.EndHorizontal();
// 
//         EditorGUILayout.BeginHorizontal();
//         {
//             // ラジオボタンとか
//             EditorGUILayout.PrefixLabel("Radio");
// 
//             for (int i = 0; i < 3; i++)
//             {
//                 if (EditorGUILayout.Toggle(i == radio, EditorStyles.radioButton))
//                 {
//                     radio = i;
//                 }
//             }
//         }
//         EditorGUILayout.EndHorizontal();
// 
//         // SelectionGridでも使える。
//         radio = GUILayout.SelectionGrid(radio, new string[] { "Radio1", "Radio2", "Radio3" }, 1, EditorStyles.radioButton);
// 
//         EditorGUILayout.BeginHorizontal();
//         {
//             // Show Built In Resourcesから適当に使いたいの選ぶ。
//             // できれば、GUIStyleはstaticとかで事前にもっといたほうがいいかも
//             titleLeft = GUILayout.Toggle(titleLeft, "Left", (GUIStyle)"OL Titleleft");
//             titleMid = GUILayout.Toggle(titleMid, "Mid", (GUIStyle)"OL Titlemid");
//             titleRight = GUILayout.Toggle(titleRight, "Right", (GUIStyle)"OL Titleright");
//         }
//         EditorGUILayout.EndHorizontal();
// 
//         // 自分で切り替える必要あったりもする。
//         GUIStyle nodeStyle = node ? (GUIStyle)"flow node hex 1 on" : (GUIStyle)"flow node hex 1";
//         if (GUILayout.Button("node", nodeStyle))
//         {
//             node = !node;
//         }
//     }
// }