
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AkiVACO;
using AkiVACO.XValue;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(UserCameraPivotSetupper))]
public class EditUserCameraPivotSetupper : Editor
{
    
    public override void OnInspectorGUI()
    {
        UserCameraPivotSetupper gen = target as UserCameraPivotSetupper;

        EditorGUILayout.LabelField("初期カメラ");
        gen.m_stdPivot.position = EditorGUILayout.Vector3Field("座標", gen.m_stdPivot.position);
        gen.m_stdPivot.rotation = ToQuaternion(EditorGUILayout.Vector4Field("回転", ToVector4(gen.m_stdPivot.rotation)));
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("構えカメラ");
        gen.m_shotPivot.position = EditorGUILayout.Vector3Field("座標", gen.m_shotPivot.position);
        gen.m_shotPivot.rotation = ToQuaternion(EditorGUILayout.Vector4Field("回転", ToVector4(gen.m_shotPivot.rotation)));
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("現在のカメラ位置に設定します", MessageType.Info);
        if (GUILayout.Button("Set 初期カメラ位置"))
        {
            Transform camera = gen.transform.GetChild(0);
            camera.localPosition = gen.m_stdPivot.position;
            camera.localRotation = gen.m_stdPivot.rotation;
        }

        if (GUILayout.Button("Set 構えカメラ位置"))
        {
            Transform camera = gen.transform.GetChild(0);
            camera.localPosition = gen.m_shotPivot.position;
            camera.localRotation = gen.m_shotPivot.rotation;
        }

        EditorGUILayout.HelpBox("現在のカメラ位置を保存します", MessageType.Info);
        EditorGUILayout.Space();
        if (GUILayout.Button("Setup 初期カメラ位置"))
        {
            Transform camera = gen.transform.GetChild(0);
            gen.m_stdPivot.position = camera.localPosition;
            gen.m_stdPivot.rotation = camera.localRotation;
        }

        if (GUILayout.Button("Setup 構えカメラ位置"))
        {
            Transform camera = gen.transform.GetChild(0);
            gen.m_shotPivot.position = camera.localPosition;
            gen.m_shotPivot.rotation = camera.localRotation;
        }
    }

    public Quaternion ToQuaternion(Vector4 v)
    {
        return new Quaternion(v.x, v.y, v.z, v.w);
    }

    public Vector4 ToVector4(Quaternion v)
    {
        return new Vector4(v.x, v.y, v.z, v.w);
    }
}

#endif

#endif