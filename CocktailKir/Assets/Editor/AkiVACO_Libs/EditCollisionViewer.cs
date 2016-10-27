
// Author     : Atuki Yoshinaga

//#define ENABLE_EDIT_COLLISION_MESH

using UnityEngine;
using System.Collections;

using AkiVACO;
using AkiVACO.XValue;
using AkiVACO.EditorUtil;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(XCollisionViewer))]
public class EditCollisionViewer : Editor
{
    private EditorUtilFoldout m_collapsed = new EditorUtilFoldout(true);

    public override void OnInspectorGUI()
    {
        XCollisionViewer gen = target as XCollisionViewer;

#if ENABLE_EDIT_COLLISION_MESH
        gen.m_viewUnitSphere = EditorGUILayout.ObjectField("MeshSphere", gen.m_viewUnitSphere, typeof(GameObject)) as GameObject;
        gen.m_viewUnitBox = EditorGUILayout.ObjectField("MeshBox", gen.m_viewUnitBox, typeof(GameObject)) as GameObject;
        gen.m_viewUnitCapsule = EditorGUILayout.ObjectField("MeshCapsule", gen.m_viewUnitCapsule, typeof(GameObject)) as GameObject;
#endif
        gen.m_keyCode = (KeyCode)(EditorGUILayout.EnumPopup("KeyCode", gen.m_keyCode));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("アルファ値");
        gen.m_meshAlpha = EditorGUILayout.Slider(gen.m_meshAlpha, 0.0f, 1.0f);
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
                    gen.m_meshColor = new Color[size];
                    for (int i = 0; i < size; ++i)
                    {
                        gen.m_tags[i] = "";
                        gen.m_meshColor[i] = Color.red;
                    }
                }
                else if (gen.m_tags.Length != size)
                {
                    string[] tags = new string[size];
                    Color[] meshColor = new Color[size];

                    int index = Mathf.Min(gen.m_tags.Length, size);
                    int idx;
                    for (idx = 0; idx < index; ++idx)
                    {
                        tags[idx] = gen.m_tags[idx];
                        meshColor[idx] = gen.m_meshColor[idx];
                    }
                    for (; idx < size; ++idx)
                    {
                        tags[idx] = "";
                        meshColor[idx] = Color.red;
                    }

                    gen.m_tags = tags;
                    gen.m_meshColor = meshColor;
                }

                for (int i = 0; i < size; ++i)
                {
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.BeginHorizontal();
                    gen.m_tags[i] = EditorGUILayout.TagField("タグ名", gen.m_tags[i]);
                    EditorGUILayout.EndVertical();
                    gen.m_meshColor[i] = EditorGUILayout.ColorField("ラベルカラー", gen.m_meshColor[i]);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.Space();
                }
            });

    }

}

#endif

#endif