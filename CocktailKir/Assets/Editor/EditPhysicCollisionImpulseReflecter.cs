using UnityEngine;
using System.Collections;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(PhysicCollisionImpulseReflecter))]
public class EditPhysicCollisionImpulseReflecter : Editor
{
    public override void OnInspectorGUI()
    {
        PhysicCollisionImpulseReflecter gen = target as PhysicCollisionImpulseReflecter;

        if (gen.m_targetTag == null)
        {
            int size = EditorGUILayout.IntField("ターゲット数", 0);
            gen.m_targetTag = new string[size];
            for (int i = 0; i < size; ++i)
            {
                gen.m_targetTag[i] = "";
            }
        }
        else
        {
            int size = EditorGUILayout.IntField("ターゲット数", gen.m_targetTag.Length);

            if (gen.m_targetTag.Length != size)
            {
                string[] tags = new string[size];
                Color[] meshColor = new Color[size];

                int index = Mathf.Min(gen.m_targetTag.Length, size);
                int idx;
                for (idx = 0; idx < index; ++idx)
                {
                    tags[idx] = gen.m_targetTag[idx];
                }
                for (; idx < size; ++idx)
                {
                    tags[idx] = "";
                }

                gen.m_targetTag = tags;
            }

            for (int i = 0; i < size; ++i)
            {
                gen.m_targetTag[i] = EditorGUILayout.TagField("リフレクトターゲット" + i.ToString(), gen.m_targetTag[i]);
            }
            EditorGUILayout.Space();
        }
    }
}

#endif

#endif
