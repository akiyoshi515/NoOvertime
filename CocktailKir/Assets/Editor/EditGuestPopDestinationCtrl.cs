
//#define EDIT_BASIS_INSPECTOR

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
    private static EditorUtilFoldout m_param = new EditorUtilFoldout();
    private static EditorUtilFoldout m_slotStrategy = new EditorUtilFoldout();
    private static EditorUtilFoldout m_costPointer = new EditorUtilFoldout();

#if EDIT_BASIS_INSPECTOR
    private static EditorUtilFoldout m_basis = new EditorUtilFoldout();
#endif

    public override void OnInspectorGUI()
    {
        GuestPopDestinationCtrl gen = target as GuestPopDestinationCtrl;

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
            gen.m_radius = radius;
        }

        m_param.Invoke(
            "お客さんのパラメータ",
            () =>
            {
                if (gen.m_param.Length != GuestConstParam.SumGuestType)
                {
                    GuestPopDestinationCtrl.GuestParam[] newParam = new GuestPopDestinationCtrl.GuestParam[GuestConstParam.SumGuestType];
                    for (int i = 0; i < newParam.Length; ++i)
                    {
                        newParam[i] = new GuestPopDestinationCtrl.GuestParam();
                    }

                    for (int i = 0; (i < GuestConstParam.SumGuestType) && (i < gen.m_param.Length); ++i)
                    {
                        newParam[i] = gen.m_param[i];
                    }
                    gen.m_param = newParam;
                }

                gen.m_param[0].m_capacity = EditorGUILayout.IntField("キャパシティ", gen.m_param[0].m_capacity);

                gen.m_param[0].m_num = EditorGUILayout.IntField("残人数", gen.m_param[0].m_num);
            });

        m_slotStrategy.Invoke(
            "戦略データ",
            () =>
            {
                int size = EditorGUILayout.IntField("個数", gen.m_slotStrategy.Length).MinLimitedZero();
                if (gen.m_slotStrategy.Length != size)
                {
                    GuestPopDestinationCtrl.StrategySlot[] newParam = new GuestPopDestinationCtrl.StrategySlot[size];
                    for (int i = 0; i < newParam.Length; ++i)
                    {
                        newParam[i] = new GuestPopDestinationCtrl.StrategySlot();
                        newParam[i].m_strategy = new GuestPopStrategyInternal.GuestPopStrategy_Wait();
                    }

                    for (int i = 0; (i < size) && (i < gen.m_slotStrategy.Length); ++i)
                    {
                        newParam[i] = gen.m_slotStrategy[i];
                    }
                    gen.m_slotStrategy = newParam;
                }

                if (gen.m_slotStrategy.Length == 0)
                {
                    EditorGUILayout.HelpBox("戦略Slotが設定されていません", MessageType.Error);
                }
                for (int i = 0; i < gen.m_slotStrategy.Length; ++i)
                {
                    EditorGUILayout.LabelField("戦略Slot" + (i + 1).ToString());
                    EditorGUI.indentLevel++;
                    GuestPopStrategy.StrategyType oldSelectedType = GuestPopStrategy.StrategyType.Wait;
                    if (gen.m_slotStrategy[i] != null)
                    {
                        if (gen.m_slotStrategy[i].m_strategy != null)
                        {
                            oldSelectedType = gen.m_slotStrategy[i].m_strategy.ToStrategyType();
                        }
                    }
                    GuestPopStrategy.StrategyType selectedType = (GuestPopStrategy.StrategyType)EditorGUILayout.EnumPopup(
                        "戦略タイプ", oldSelectedType);
                    bool isNewcomer = true;
                    if (gen.m_slotStrategy[i].m_strategy != null)
                    {
                        if (gen.m_slotStrategy[i].m_strategy.ToStrategyType() == selectedType)
                        {
                            isNewcomer = false;
                        }
                    }
                    gen.m_slotStrategy[i].m_strategy = GuestPopStrategy.CreatePopStrategy(selectedType);
                    gen.m_slotStrategy[i].m_time = EditorGUILayout.FloatField("実行時間", gen.m_slotStrategy[i].m_time);
                    EditStrategySlotValues(ref gen.m_slotStrategy[i], isNewcomer);
                    EditorGUI.indentLevel--;
                }

            });

        gen.m_isLoop = EditorGUILayout.Toggle("戦略データはループするか？", gen.m_isLoop);

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
                    EditorGUILayout.ObjectField("PopPointer", unit.gameObject, typeof(GameObject), false);
                    unit.m_cost = EditorGUILayout.IntField("コスト", unit.m_cost).MinLimitedZero();
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
    }

    private void EditStrategySlotValues(ref GuestPopDestinationCtrl.StrategySlot slot, bool isNewcomer)
    {
        switch(slot.m_strategy.ToStrategyType())
        {
            case GuestPopStrategy.StrategyType.Wait:
                slot.m_values = null;
                slot.m_fvalues = null;
                break;
            case GuestPopStrategy.StrategyType.Standard:
                if (isNewcomer)
                {
                    slot.m_fvalues = new float[1];
                    slot.m_values = new int[1];
                }
                slot.m_fvalues[0] = EditorGUILayout.FloatField("出現間隔(Sec)", slot.m_fvalues[0]);
                slot.m_values[0] = EditorGUILayout.IntField("同時出現数(Num)", slot.m_values[0]).MinLimitedOne();
                break;
        }
    }

}

#endif

#endif
