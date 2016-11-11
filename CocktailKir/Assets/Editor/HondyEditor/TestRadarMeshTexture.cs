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
    Texture2D[] m_tex2d;
    RenderTexture[] _rt;
    Vector3 m_mapCenter = new Vector3(0,0,0);

    Camera[] m_camera;
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

        m_mapCenter = EditorGUILayout.Vector3Field("マップの中心", m_mapCenter);
     
        if (GUILayout.Button("Set Render Texture", GUI.skin.button))
        {
            CreateRenderTexture();
            SetCamera();
            Debug.Log("Set Render Texture complete");

        }
        if (GUILayout.Button("Generate Map Texture", GUI.skin.button))
        {
            GenerateRenderRadarMeshTexture();
            AssetDatabase.Refresh(ImportAssetOptions.Default);
            Debug.Log("GenerateRenderRadarMeshTexture Complete");

        }

//         if (0 < _rt.Length)
//         {
//             if (_rt[0])
//             {
//                 m_tex2d = new Texture2D[9];
//                 byte[] readBinary = File.ReadAllBytes("Assets/Hondy/" + _rt[0].name + ".png");
//                 m_tex2d[0].LoadImage(readBinary); 
//                 EditorGUI.DrawPreviewTexture(new Rect(0, 140, 128, 128), m_tex2d[0]);
//             }
//         }

    }
    void SetCamera()
    {

        GameObject _parent = new GameObject();
        GameObject _origin = new GameObject();

        Camera _camera = _origin.AddComponent<Camera>();
        _camera.orthographic = true;
        _camera.orthographicSize = 15;
        _camera.transform.Rotate(  (new Vector3( 90, 0, 0)));
        GameObject[] objectArray = new GameObject[9];
        for (int j = 0;j < 9;j++)
        {
            objectArray[j] = Instantiate(_origin) as GameObject;
        }
        m_camera = new Camera[9];
        for (int i = 0; i < 9; i++)
        {
            m_camera[i] = objectArray[i].GetComponent<Camera>();
            objectArray[i].transform.parent = _parent.transform;
            Vector3 pos;
            pos = m_mapCenter + new Vector3(i / 3 * 30, 0, i % 3 * 30);
            objectArray[i].transform.position = pos;
            RenderTexture rt = AssetDatabase.LoadAssetAtPath("Assets/Hondy/" + _rt[i].name + ".asset", typeof(Object)) as RenderTexture;
            m_camera[i].targetTexture = rt;
        }
        GameObject.DestroyImmediate(_origin);
    }

    void CreateRenderTexture()
    {

        _rt = new RenderTexture[9];
        for (int i = 0; i < 9;i++)
        {
            _rt[i] = new RenderTexture(512, 512,1000, RenderTextureFormat.Default);
            _rt[i].name = "mapTexture" + i.ToString();
            AssetDatabase.CreateAsset(_rt[i], "Assets/Hondy/" + _rt[i].name + ".asset");
        }
    }

    void GenerateRenderRadarMeshTexture()
    {
        for (int i = 0; i < 9;i++)
        {

            Texture2D tex = new Texture2D(_rt[i].width, _rt[i].height, TextureFormat.RGB24, false);
            RenderTexture.active = _rt[i];
            tex.ReadPixels(new Rect(0, 0, _rt[i].width, _rt[i].height), 0, 0);
            tex.Apply();

            // Encode texture into PNG
            byte[] bytes = tex.EncodeToPNG();
            Object.DestroyImmediate(tex);
            //Write to a file in the project folder
            File.WriteAllBytes("Assets/Hondy/" + _rt[i].name + ".png", bytes);
        //    AssetDatabase.DeleteAsset("Assets/Hondy/" + _rt[i].name + ".asset");
        }

    }

    void GenerateMapMesh()
    {

    }
}

