
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

using AkiVACO;
using AkiVACO.XValue;
using AkiVACO.EditorUtil;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(XObjLabelUnit))]
public class EditObjectLabel : Editor
{
    private EditorUtilFoldout m_collapsed = new EditorUtilFoldout(true);

    public override void OnInspectorGUI()
    {
        XObjLabelUnit gen = target as XObjLabelUnit;

        gen.m_keyCode = (KeyCode)(EditorGUILayout.EnumPopup("KeyCode", gen.m_keyCode));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("背景アルファ値");
        gen.m_backScreenAlpha = EditorGUILayout.Slider(gen.m_backScreenAlpha, 0.0f, 1.0f);
        EditorGUILayout.EndHorizontal();

        m_collapsed.Invoke(
            "タグ情報",
            () =>
            {
                int size = EditorGUILayout.IntField("ラベル数", gen.m_sizelabelInfo);
                gen.m_sizelabelInfo = size;

                if (gen.m_tags == null)
                {
                    gen.m_tags = new string[size];
                    gen.m_labelSize = new Vector2[size];
                    gen.m_backScreenColor = new Color[size];
                    for (int i = 0; i < size; ++i)
                    {
                        gen.m_tags[i] = "";
                        gen.m_labelSize[i] = new Vector2(50.0f, 50.0f);
                        gen.m_backScreenColor[i] = Color.grey;
                    }
                }
                else if (gen.m_tags.Length != size)
                {
                    string[] tags = new string[size];
                    Vector2[] labelSize = new Vector2[size];
                    Color[] backScreenColor = new Color[size];

                    int index = Mathf.Min(gen.m_tags.Length, size);
                    int idx;
                    for (idx = 0; idx < index; ++idx)
                    {
                        tags[idx] = gen.m_tags[idx];
                        labelSize[idx] = gen.m_labelSize[idx];
                        backScreenColor[idx] = gen.m_backScreenColor[idx];
                    }
                    for (; idx < size; ++idx)
                    {
                        tags[idx] = "";
                        labelSize[idx] = new Vector2(50.0f, 50.0f);
                        backScreenColor[idx] = Color.grey;
                    }

                    gen.m_tags = tags;
                    gen.m_labelSize = labelSize;
                    gen.m_backScreenColor = backScreenColor;
                }

                for (int i = 0; i < size; ++i)
                {
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.BeginHorizontal();
                    gen.m_tags[i] = EditorGUILayout.TagField("タグ名", gen.m_tags[i]);
                    EditorGUILayout.EndVertical();
                    gen.m_labelSize[i] = EditorGUILayout.Vector2Field("ラベルサイズ", gen.m_labelSize[i]);
                    gen.m_backScreenColor[i] = EditorGUILayout.ColorField("ラベルカラー", gen.m_backScreenColor[i]);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.Space();
                }
            });

    }

}

#endif

#endif