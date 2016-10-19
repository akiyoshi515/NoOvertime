using UnityEngine;
using System.Collections;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(GuestCharmViewer))]
public class EditGuestCharmViewer : Editor
{
    public override void OnInspectorGUI()
    {
        GuestCharmViewer viewer = target as GuestCharmViewer;
        GuestScoreUnit unit = viewer.GetComponent<GuestCtrl>().unit;

        string msg = "魅了ユーザー：";
        if (unit.topUserId == -1)
        {
            msg += "なし";
        }
        else
        {
            msg += ((UserID)(unit.topUserId)).ToString();
        }
        EditorGUILayout.LabelField(msg);

        EditorGUILayout.LabelField("魅了パラメータ");
        EditorGUILayout.IntField("User1", unit.charmTable[0]);
        EditorGUILayout.IntField("User2", unit.charmTable[1]);
        EditorGUILayout.IntField("User3", unit.charmTable[2]);
        EditorGUILayout.IntField("User4", unit.charmTable[3]);

    }
}

#endif

#endif
