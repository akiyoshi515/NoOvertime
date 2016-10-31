
#define EDIT_BASIS_INSPECTOR

using UnityEngine;
using System.Collections;

#if DEBUG

#if UNITY_EDITOR

using UnityEditor;

using AkiVACO.XValue;
using AkiVACO.EditorUtil;

[CustomEditor(typeof(GuestPopDestinationCtrl))]
public class EditGuestPopDestinationCtrl : Editor
{
    private static EDUtilFoldout m_param = new EDUtilFoldout();
    private static EDUtilFoldout m_slotStrategy = new EDUtilFoldout();
    private static EDUtilFoldout m_costPointer = new EDUtilFoldout();

#if EDIT_BASIS_INSPECTOR
    private static EDUtilFoldout m_basis = new EDUtilFoldout();
#endif

    public override void OnInspectorGUI()
    {
        GuestPopDestinationCtrl gen = target as GuestPopDestinationCtrl;
        SerializedObject ser = new SerializedObject(gen);
        ser.Update();

        {
            float radius = EditorGUILayout.FloatField("直径", gen.m_radius);
            Transform transEditMesh = gen.transform.FindChild("EditMesh");
            if (transEditMesh != null)
            {
                transEditMesh.localScale = new Vector3(radius, transEditMesh.localScale.y, radius);
            }
            else
            {
                EditorGUILayout.HelpBox("EditMeshがありません", MessageType.Warning);
            }
            ser.FindProperty("m_radius").floatValue = radius;
        }

        m_param.Invoke(
            "お客さんのパラメータ",
            () =>
            {
                SerializedProperty param = ser.FindProperty("m_param");
                if (param.arraySize != GuestConstParam.SumGuestType)
                {
                    int size = param.arraySize;
                    if (size < GuestConstParam.SumGuestType)
                    {
                        for (int i = size; i < GuestConstParam.SumGuestType; ++i)
                        {
                            param.InsertArrayElementAtIndex(i);
                        }
                    }
                    else
                    {
                        for (int i = size; i >= GuestConstParam.SumGuestType; --i)
                        {
                            param.DeleteArrayElementAtIndex(i);
                        }
                    }
                }
                param.GetArrayElementAtIndex(0).FindPropertyRelative("m_capacity").intValue = EditorGUILayout.IntField("キャパシティ", gen.m_param[0].m_capacity);

                EditorGUILayout.IntField("残人数", gen.m_param[0].m_num);
            });

        m_slotStrategy.Invoke(
            "戦略データ",
            () =>
            {
                SerializedProperty slotStrategy = ser.FindProperty("m_slotStrategy");
                EDUtilFunctions.ResizeArray(ref slotStrategy, "個数");

                if (slotStrategy.arraySize == 0)
                {
                    EditorGUILayout.HelpBox("戦略Slotが設定されていません", MessageType.Error);
                }
                for (int i = 0; i < slotStrategy.arraySize; ++i)
                {
                    EditorGUILayout.LabelField("戦略Slot" + (i + 1).ToString());
                    EditorGUI.indentLevel++;
                    GuestPopStrategy.StrategyType oldSelectedType = GuestPopStrategy.StrategyType.Wait;
                    SerializedProperty strategyType = slotStrategy.GetArrayElementAtIndex(i).FindPropertyRelative("m_strategyType");
                    if (strategyType != null)
                    {
                        oldSelectedType = (GuestPopStrategy.StrategyType)strategyType.enumValueIndex;
                    }
                    GuestPopStrategy.StrategyType selectedType = (GuestPopStrategy.StrategyType)EditorGUILayout.EnumPopup(
                        "戦略タイプ", oldSelectedType);
                    bool isNewcomer = true;
                    if (selectedType == oldSelectedType)
                    {
                        isNewcomer = false;
                    }
                    strategyType.enumValueIndex = (int)selectedType;
                    SerializedProperty time = slotStrategy.GetArrayElementAtIndex(i).FindPropertyRelative("m_time");
                    time.floatValue = EditorGUILayout.FloatField("実行時間", time.floatValue);
//                    gen.m_slotStrategy[i].m_strategy = GuestPopStrategy.CreatePopStrategy(selectedType);
                    SerializedProperty slot = slotStrategy.GetArrayElementAtIndex(i);
                    EditStrategySlotValues(ref slot, isNewcomer);
                    EditorGUI.indentLevel--;
                }

            });

        ser.FindProperty("m_isLoop").boolValue = EditorGUILayout.Toggle("戦略データはループするか？", gen.m_isLoop);

        m_costPointer.Invoke(
            "各PopPointのコスト",
            () =>
            {
                GuestPopPointerCtrl[] table = gen.GetComponentsInChildren<GuestPopPointerCtrl>();
                if (table.Length == 0)
                {
                    EditorGUILayout.HelpBox("PopPointが設定されていません", MessageType.Error);

                }
                if (GUILayout.Button("PopPointを生成"))
                {
                    GameObject obj = GameObject.Instantiate(gen.m_popPointer, gen.transform.position, gen.transform.rotation, gen.transform) as GameObject;
                    obj.name = gen.m_popPointer.name + table.Length;
                    table = gen.GetComponentsInChildren<GuestPopPointerCtrl>();
                }
                EditorGUILayout.Space();

                foreach (GuestPopPointerCtrl unit in table)
                {
                    SerializedObject serobj = new SerializedObject(unit);
                    serobj.Update();
                    EditorGUILayout.ObjectField("PopPointer", unit.gameObject, typeof(GameObject), false);
                    serobj.FindProperty("m_cost").intValue = EditorGUILayout.IntField("優先度", unit.m_cost).MinLimitedZero();
                    serobj.ApplyModifiedProperties();
                }

            });

#if EDIT_BASIS_INSPECTOR
        EditorGUILayout.Space();
        m_basis.Invoke(
            "<Warning>BasisInstance",
            () =>
            {
                EditorGUILayout.HelpBox("ここからは制御しないでください", MessageType.Warning);
                this.DrawDefaultInspector();
            });
#endif
        ser.ApplyModifiedProperties();
    }

    private void EditStrategySlotValues(ref SerializedProperty slot, bool isNewcomer)
    {
        switch ((GuestPopStrategy.StrategyType)slot.FindPropertyRelative("m_strategyType").enumValueIndex)
        {
            case GuestPopStrategy.StrategyType.Wait:
                slot.FindPropertyRelative("m_values").ClearArray();
                slot.FindPropertyRelative("m_fvalues").ClearArray();
                break;
            case GuestPopStrategy.StrategyType.Standard:
                SerializedProperty values = slot.FindPropertyRelative("m_values");
                SerializedProperty fvalues = slot.FindPropertyRelative("m_fvalues");
                if (isNewcomer)
                {
                    int arraySize = 1;
                    EDUtilFunctions.ResizeConstArray(ref values, arraySize);
                    EDUtilFunctions.ResizeConstArray(ref fvalues, arraySize);
                }
                fvalues.GetArrayElementAtIndex(0).floatValue = EditorGUILayout.FloatField("出現間隔(Sec)", fvalues.GetArrayElementAtIndex(0).floatValue);
                values.GetArrayElementAtIndex(0).intValue = EditorGUILayout.IntField("同時出現数(Num)", values.GetArrayElementAtIndex(0).intValue).MinLimitedOne();
                break;
        }
    }

}

#endif

#endif
