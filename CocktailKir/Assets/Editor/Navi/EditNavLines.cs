
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

        serializedObject.Update();

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

        SerializedProperty resolution =  serializedObject.FindProperty("m_resolution");
        SerializedProperty lineViewHeight = serializedObject.FindProperty("m_lineViewHeight");

        resolution.intValue = EditorGUILayout.IntSlider("分割数", resolution.intValue, 1, 16);
        lineViewHeight.floatValue = EditorGUILayout.FloatField("線を表示する高さ", lineViewHeight.floatValue);

        m_collapsed = EditorGUILayout.Foldout(m_collapsed, "NavPointer情報");

        if (m_collapsed)
        {
            {
                EditorGUILayout.LabelField("座標情報");
                EditorGUILayout.BeginVertical();
                int idx = 0;
                foreach (Vector3 vec in gen.table)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Vector3Field(idx.ToString(), vec);
                    EditorGUI.indentLevel--;
                    ++idx;
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.Space();
            {
                EditorGUILayout.LabelField("WaitPoint情報");
                EditorGUILayout.BeginVertical();
                int idx = 0;
                for (int i = 0; i < gen.waitPoint.Length; ++i)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.LabelField(idx.ToString());
                    EditorGUILayout.IntField("Index", gen.waitPoint[i]);
                    EditorGUILayout.FloatField("Time", gen.waitTime[i]);
                    EditorGUI.indentLevel--;
                    ++idx;
                }
                EditorGUILayout.EndVertical();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    void CalcNavLines()
    {
        if (!m_exec)
        {
            return;
        }

        NavLines gen = target as NavLines;
        serializedObject.Update();

        int resolution = gen.resolution;
        float resolutionRate = 1.0f / resolution;

        if (gen.Edit_GetPointerCount() >= 4)
        {
            Queue<Vector3> que = new Queue<Vector3>();

            SerializedProperty loopPoint = serializedObject.FindProperty("m_loopPoint");

            int idx = 0;
            loopPoint.intValue = -1;
            System.Func<Vector3> funcNameless = () => 
            {
                Transform transBasis = gen.transform.GetChild(idx);
                NavLinePointer pointer = transBasis.GetComponent<NavLinePointer>();
                if (pointer != null)
                {
                    if (pointer.isLoop)
                    {
                        loopPoint.intValue = idx;
                    }
                }
                ++idx;
                return transBasis.transform.position;
            };

            Vector3 v0 = funcNameless.Invoke();
            Vector3 v1 = funcNameless.Invoke();
            Vector3 v2 = funcNameless.Invoke();
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

                vec.y = gen.lineViewHeight;

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

            SerializedProperty waitPoint = serializedObject.FindProperty("m_tableWaitPoint");
            SerializedProperty waitTime = serializedObject.FindProperty("m_tableWaitTime");
            waitPoint.ClearArray();
            waitTime.ClearArray();
            int idxWaitPoint = 0;
            for (; idx < gen.transform.childCount; )
            {
                Transform trans = gen.transform.GetChild(idx);
                NavLinePointer cs = trans.GetComponent<NavLinePointer>();
                if (cs != null)
                {
                    if (cs.isLoop)
                    {
                        loopPoint.intValue = idx;
                    }
                }
                else
                {
                    NavLineWaitPointer csWait = trans.GetComponent<NavLineWaitPointer>();
                    if (csWait!= null)
                    {
                        waitPoint.InsertArrayElementAtIndex(idxWaitPoint);
                        waitTime.InsertArrayElementAtIndex(idxWaitPoint);

                        waitPoint.GetArrayElementAtIndex(idxWaitPoint).intValue = (idx-1).MinLimitedZero() * resolution;
                        waitTime.GetArrayElementAtIndex(idxWaitPoint).floatValue = csWait.waitTime;

                        ++idxWaitPoint;
                    }
                }
                ++idx;
                v = trans.transform.position;
                namelessCardinal.Invoke();
            }

            serializedObject.ApplyModifiedProperties();

            if (gen.isLoop)
            {
                v = gen.transform.GetChild(gen.loopPoint).transform.position;
                namelessCardinal.Invoke();
            }
            namelessCardinal.Invoke();
            nameless.Invoke(0.0f);

            gen.table = new Vector3[que.Count];
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
                gen.table[n] = commit;
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