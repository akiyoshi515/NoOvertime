
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AkiVACO;
using AkiVACO.XValue;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(NavLines))]
public class EditNavLines : Editor
{
    private static bool m_collapsed = true;
    private static bool m_exec = false;

    public override void OnInspectorGUI()
    {
        NavLines gen = target as NavLines;

        EditorGUILayout.HelpBox("y = 合計の距離", MessageType.Info);

        bool bn = EditorGUILayout.Toggle("自動更新：" + (m_exec ? "実行中" : "停止中"), m_exec);
        if (m_exec != bn)
        {
            m_exec = bn;
            if (m_exec)
            {
                EditorApplication.update += CalcNavLines;
            }
            else
            {
                EditorApplication.update -= CalcNavLines;
            }
        }

        gen.m_resolution = EditorGUILayout.IntSlider("分割数", gen.m_resolution, 1, 16);
        gen.m_lineViewHeight = EditorGUILayout.FloatField("線を表示する高さ", gen.m_lineViewHeight);

        m_collapsed = EditorGUILayout.Foldout(m_collapsed, "NavPointer情報");

        if (m_collapsed)
        {
            EditorGUILayout.BeginVertical();
            int idx = 0;
            foreach (Vector3 vec in gen.m_table)
            {
                EditorGUILayout.Vector3Field(idx.ToString(), vec);
                ++idx;
            }
            EditorGUILayout.EndVertical();
        }
    }

    void CalcNavLines()
    {
        if (!m_exec)
        {
            return;
        }

        NavLines gen = target as NavLines;

        int resolution = gen.m_resolution;
        float resolutionRate = 1.0f / resolution;

        if (gen.GetPointerCount() >= 4)
        {
            Queue<Vector3> que = new Queue<Vector3>();

            int idx = 0;
            Vector3 v0 = gen.transform.GetChild(idx++).transform.position;
            Vector3 v1 = gen.transform.GetChild(idx++).transform.position;
            Vector3 v2 = gen.transform.GetChild(idx++).transform.position;
            Vector3 v = Vector3.zero;

            System.Action<float> nameless = (s) =>
            {
                float s2 = s * s;
                float s3 = s2 * s;
                float f0 = ((-s3) + (2.0f * s2) - (s));
                float f1 = ((3.0f * s3) - (5.0f * s2) + (2.0f));
                float f2 = ((-3.0f * s3) + (4.0f * s2) + (s));
                float f3 = ((s3) - (s2));

                Vector3 vec = Vector3.zero;
                vec.x = ((f0 * v0.x) + (f1 * v1.x) + (f2 * v2.x) + (f3 * v.x)) * 0.50f;
                vec.z = ((f0 * v0.z) + (f1 * v1.z) + (f2 * v2.z) + (f3 * v.z)) * 0.50f;

                vec.y = gen.m_lineViewHeight;

                que.Enqueue(vec);
            };


            System.Action namelessCardinal = () =>
            {
                for (int k = 0; k < resolution; ++k)
                {
                    nameless.Invoke(resolutionRate * k);
                }

                v0 = v1;
                v1 = v2;
                v2 = v;
            };

            for (; idx < gen.transform.childCount; )
            {
                v = gen.transform.GetChild(idx++).transform.position;
                namelessCardinal.Invoke();
            }

            namelessCardinal.Invoke();
            nameless.Invoke(0.0f);

            gen.m_table = new Vector3[que.Count];
            LineRenderer render = gen.transform.GetChild(0).GetComponent<LineRenderer>();
            render.SetVertexCount(que.Count);

            Vector3 prev = que.Peek();
            float sumDistance = 0.0f;
            int n = 0;
            foreach (Vector3 vec in que)
            {
                Vector3 commit = vec;
                sumDistance += Vector3.Distance(vec, prev);
                commit.y = sumDistance;
                prev = vec;

                render.SetPosition(n, vec);
                gen.m_table[n] = commit;
                ++n;
            }

        }
        else
        {
            Debug.LogError("EditNavLines: NavPointerが４つ以上必要です。");
        }
    }
}

#endif

#endif