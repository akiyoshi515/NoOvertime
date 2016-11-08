using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class TestRadarMeshTexture 
    :
    EditorWindow
{

    Object target;
    Object test;
    RenderTexture m_rt;
    Texture m_tex2d;
    // メニューのWindowにEditorExという項目を追加。
    [MenuItem("Window/HondyEditor/test")]
    static void Open()
    {
        // メニューのWindow/EditorExを選択するとOpen()が呼ばれる。
        // 表示させたいウィンドウは基本的にGetWindow()で表示＆取得する。
        EditorWindow.GetWindow<TestRadarMeshTexture>("HondyEditor/test"); // タイトル名を"EditorEx"に指定（後からでも変えられるけど）
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("hoge", GUI.skin.box);
        
        
        m_rt = EditorGUILayout.ObjectField("RenderTexture", m_rt, typeof(RenderTexture), true) as RenderTexture;

        if (GUILayout.Button("hoge！", GUI.skin.button))
        {
            GenerateRenderRadarMeshTexture();
            Debug.Log("GenerateRenderRadarMeshTexture");
        }




    }


    void GenerateRenderRadarMeshTexture()
    {

        Texture2D tex = new Texture2D(m_rt.width, m_rt.height, TextureFormat.RGB24, false);
        RenderTexture.active = m_rt;
        tex.ReadPixels(new Rect(0, 0, m_rt.width, m_rt.height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.DestroyImmediate(tex);
        //Write to a file in the project folder
        File.WriteAllBytes("/Assets/Hondy/" + m_rt.name + ".png", bytes);

    }
}

