
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

using AkiVACO;
using AkiVACO.XValue;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(AkiVACO.XLoggerInternal.XLoggerGUIUnit))]
public class EditLoggerUnit : Editor
{
    public override void OnInspectorGUI()
    {
        AkiVACO.XLoggerInternal.XLoggerGUIUnit gen = target as AkiVACO.XLoggerInternal.XLoggerGUIUnit;

        Vector2 pos = EditorGUILayout.Vector2Field("初期座標", new Vector2(gen.m_positionX, gen.m_positionY));
        gen.m_positionX = pos.x;
        gen.m_positionY = pos.y;

        Vector2 move = EditorGUILayout.Vector2Field("移動速度", new Vector2(gen.m_moveSpeedX, gen.m_moveSpeedY));
        gen.m_moveSpeedX = move.x;
        gen.m_moveSpeedY = move.y;

        gen.m_weight = EditorGUILayout.FloatField("ウィンドウ幅", gen.m_weight);

        gen.isEnableView = EditorGUILayout.Toggle("表示/非表示", gen.isEnableView);

        gen.m_enableKey = (KeyCode)(EditorGUILayout.EnumPopup("表示/非表示キー", gen.m_enableKey));
        gen.m_dumpLogMenuKey = (KeyCode)(EditorGUILayout.EnumPopup("ログダンプキー", gen.m_dumpLogMenuKey));

        gen.m_backgroundColor = EditorGUILayout.ColorField("背景色", gen.m_backgroundColor);

    }

}

#endif

#endif