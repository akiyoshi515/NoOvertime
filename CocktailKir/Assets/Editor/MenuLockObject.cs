using UnityEngine;

using System.Collections;

using UnityEditor;

public class LockObject : EditorWindow
{
    static LockObject instance;

    [MenuItem("Window/LockObject")]

    static void OpenLockObject()
    {
        if (instance != null)
        {
            instance.Close();
        }

        instance = EditorWindow.CreateInstance<LockObject>();
        instance.Show();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    void OnGUI()
    {
        GameObject active = Selection.activeGameObject;
        if (active == null)
        {
            return;
        }

        Component[] components = active.GetComponents<Component>();

        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField(active.name);
        bool isEditable = (active.hideFlags & HideFlags.NotEditable) == HideFlags.NotEditable;
        bool isBlock = GUILayout.Button(isEditable ? "unlock" : "lock");

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        foreach (Component component in components)
        {
            DrawLine(() =>
            {
                EditorGUILayout.LabelField(component.GetType().Name);

                bool isComponentEditable = (component.hideFlags & HideFlags.NotEditable) == HideFlags.NotEditable;
                bool isComponentTogleResult = !EditorGUILayout.Toggle(isComponentEditable);

                if (isComponentTogleResult)
                {
                    component.hideFlags |= HideFlags.NotEditable;
                }
                else
                {
                    component.hideFlags &= ~HideFlags.NotEditable;
                }
            });
        }

        if (isBlock)
        {
            if (isEditable)
            {
                active.hideFlags &= ~HideFlags.NotEditable;
            }
            else
            {
                active.hideFlags |= HideFlags.NotEditable;
            }
        }
    }

    void DrawLine(System.Action action)
    {
        EditorGUILayout.BeginHorizontal();
        action();
        EditorGUILayout.EndHorizontal();
    }
}
