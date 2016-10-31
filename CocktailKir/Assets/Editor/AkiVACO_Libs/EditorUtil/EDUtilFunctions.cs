
#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Collections;

using AkiVACO.XValue;

namespace AkiVACO
{
namespace EditorUtil
{

public static class EDUtilFunctions
{
    public static void ResizeArray(ref SerializedProperty target, string resizemsg)
    {
        int arraySize = EditorGUILayout.IntField(resizemsg, target.arraySize).MinLimitedZero();
        ResizeConstArray(ref target, arraySize);
    }

    public static void ResizeConstArray(ref SerializedProperty target, int arraySize)
    {
        if (arraySize != target.arraySize)
        {
            if (arraySize > target.arraySize)
            {
                for (int i = target.arraySize; i < arraySize; ++i)
                {
                    target.InsertArrayElementAtIndex(i);
                }
            }
            else
            {
                if (target.arraySize == 0)
                {
                    target.InsertArrayElementAtIndex(0);
                }
                else
                {
                    for (int i = target.arraySize; i > arraySize; --i)
                    {
                        target.DeleteArrayElementAtIndex(i-1);
                    }
                }
            }
        }
    }

    public static void EditSerializedObject<T>(GameObject baseobject, UnityAction<SerializedObject> callback)
    {
        T[] table = baseobject.GetComponentsInChildren<T>();
        foreach (T cs in table)
        {
            SerializedObject ser = new SerializedObject(cs as UnityEngine.Object);
            ser.Update();
            callback.Invoke(ser);
            ser.ApplyModifiedProperties();
        }
    }

    public static void EditApplySerializedPrefab<T>(GameObject baseprefab, UnityAction<SerializedObject> callback)
    {
        GameObject prefab = PrefabUtility.InstantiatePrefab(baseprefab) as GameObject;
        EditSerializedObject<T>(prefab, callback);
        PrefabUtility.ReplacePrefab(prefab, baseprefab);
        GameObject.DestroyImmediate(prefab);
    }


}

}   // End of namespace EditorUtil
}   // End of namespace AkiVACO

#endif