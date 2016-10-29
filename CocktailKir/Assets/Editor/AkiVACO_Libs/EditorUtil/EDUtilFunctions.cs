
#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Collections;

namespace AkiVACO
{
namespace EditorUtil
{

public static class EDUtilFunctions
{
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