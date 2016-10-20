using UnityEngine;
using System.Collections;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(GuestScoreViewer))]
public class EditGuestScoreViewer : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.IntField("最大人数", GuestScores.maxScore);

        EditorGUILayout.LabelField("魅了パラメータ");
        EditorGUILayout.IntField("User1", GuestScores.scoreTable[0]);
        EditorGUILayout.IntField("User2", GuestScores.scoreTable[1]);
        EditorGUILayout.IntField("User3", GuestScores.scoreTable[2]);
        EditorGUILayout.IntField("User4", GuestScores.scoreTable[3]);

    }
}

#endif

#endif
