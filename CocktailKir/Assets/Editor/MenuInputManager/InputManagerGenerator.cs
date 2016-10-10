
// 引用元：http://notargs.com/blog/blog/2015/01/23/92/

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;

namespace NotargsInputManagerUtil
{
    public enum AxisType
    {
        KeyOrMouseButton = 0,
        MouseMovement = 1,
        JoystickAxis = 2
    };

    /// <summary>
    /// 入力の情報
    /// </summary>
    public class InputAxis
    {
        public string name = "";
        public string descriptiveName = "";
        public string descriptiveNegativeName = "";
        public string negativeButton = "";
        public string positiveButton = "";
        public string altNegativeButton = "";
        public string altPositiveButton = "";

        public float gravity = 0;
        public float dead = 0;
        public float sensitivity = 0;

        public bool snap = false;
        public bool invert = false;

        public AxisType type = AxisType.KeyOrMouseButton;

        // 1から始まる。
        public int axis = 1;
        // 0なら全てのゲームパッドから取得される。1以降なら対応したゲームパッド。
        public int joyNum = 0;

        /// <summary>
        /// 押すと1になるキーの設定データを作成する
        /// </summary>
        public static InputAxis CreateButton(string name, string positiveButton, string altPositiveButton)
        {
            var axis = new InputAxis();
            axis.name = name;
            axis.positiveButton = positiveButton;
            axis.altPositiveButton = altPositiveButton;
            axis.gravity = 1000;
            axis.dead = 0.001f;
            axis.sensitivity = 1000;
            axis.type = AxisType.KeyOrMouseButton;

            return axis;
        }

        /// <summary>
        /// ゲームパッド用の軸の設定データを作成する
        /// </summary>
        public static InputAxis CreatePadAxis(string name, int joystickNum, int axisNum)
        {
            var axis = new InputAxis();
            axis.name = name;
            axis.dead = 0.2f;
            axis.sensitivity = 1;
            axis.type = AxisType.JoystickAxis;
            axis.axis = axisNum;
            axis.joyNum = joystickNum;

            return axis;
        }

        /// <summary>
        /// キーボード用の軸の設定データを作成する
        /// </summary>
        public static InputAxis CreateKeyAxis(string name, string negativeButton, string positiveButton, string altNegativeButton, string altPositiveButton)
        {
            var axis = new InputAxis();
            axis.name = name;
            axis.negativeButton = negativeButton;
            axis.positiveButton = positiveButton;
            axis.altNegativeButton = altNegativeButton;
            axis.altPositiveButton = altPositiveButton;
            axis.gravity = 3;
            axis.sensitivity = 3;
            axis.dead = 0.001f;
            axis.type = AxisType.KeyOrMouseButton;

            return axis;
        }
    }

    /// <summary>
    /// InputManager生成器
    /// </summary>
    public class InputManagerGenerator
    {
        protected SerializedObject m_serializedObject = null;
        protected SerializedProperty m_axesProperty = null;

        public InputManagerGenerator()
        {
            Object[] table = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset");
            m_serializedObject = new SerializedObject(table[0]);
            m_axesProperty = m_serializedObject.FindProperty("m_Axes");
        }

        public void AddAxis(InputAxis axis)
        {
            if (axis.axis < 1)
            {
                Debug.LogError("Invalid argument value : axis < 1");
            }

            SerializedProperty axesProperty = m_serializedObject.FindProperty("m_Axes");

            axesProperty.arraySize++;
            m_serializedObject.ApplyModifiedProperties();

            SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

            GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
            GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
            GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
            GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
            GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
            GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
            GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
            GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
            GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
            GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
            GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
            GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
            GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
            GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
            GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

            m_serializedObject.ApplyModifiedProperties();

        }

        public void Clear()
        {
            m_axesProperty.ClearArray();
            m_serializedObject.ApplyModifiedProperties();
        }

        private SerializedProperty GetChildProperty(SerializedProperty parent, string name)
        {
            SerializedProperty child = parent.Copy();
            child.Next(true);
            do
            {
                if (child.name == name)
                {
                    return child;
                }
            }
            while (child.Next(false));

            return null;
        }

    }

}

#endif