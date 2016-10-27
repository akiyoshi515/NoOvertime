
#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AkiVACO
{
namespace EditorUtil
{

public class EditorUtilFoldout
{
    public bool collapsed
    {
        get;
        protected set;
    }

    public EditorUtilFoldout(bool startValue = false)
    {
        collapsed = startValue;
    }

    public void Invoke(string label, UnityEngine.Events.UnityAction customFunc)
    {
        if (customFunc == null)
        {
            throw new System.ArgumentNullException();
        }

        collapsed = EditorGUILayout.Foldout(collapsed, label);

        if (collapsed)
        {
            EditorGUI.indentLevel++;
            customFunc.Invoke();
            EditorGUI.indentLevel--;
        }
    }

}

}   // End of namespace EditorUtil
}   // End of namespace AkiVACO

#endif