
// Author     : Atuki Yoshinaga

using UnityEngine;
using System;
using System.Collections;

namespace AkiVACO
{

namespace XUnityGameObject
{
    public static class UnityGameObjectExtensions
    {
        public static T GetSafeComponent<T>(this UnityEngine.GameObject obj, T target) where T : MonoBehaviour
        {
            T component = obj.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError("Expected to find component of type " + typeof(T) + " but found none", obj);
            }

            return component;
        }

        public static T GetSafeComponent<T>(this UnityEngine.Component obj, T target) where T : MonoBehaviour
        {
            T component = obj.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError("Expected to find component of type " + typeof(T) + " but found none", obj);
            }

            return component;
        }

        /// <summary>
        /// 指定した UnityEngine.GameObject の子を name で検索します。
        /// </summary>
        public static UnityEngine.GameObject FindChild(this UnityEngine.GameObject obj, string name)
        {
            Transform trans = obj.transform.FindChild(name);
            if (trans == null)
            {
                return null;
            }
            return trans.gameObject;
        }
        
        /// <summary>
        /// インデックスから子の transform を取得します。
        /// </summary>
        public static UnityEngine.GameObject GetChild(this UnityEngine.GameObject obj, int index)
        {
            Transform child =  obj.transform.GetChild(index);
            if (child == null)
            {
                return null;
            }
            return child.gameObject;
        }

        /// <summary>
        /// This の持つ子の数。
        /// </summary>
        public static int GetChildCount(this UnityEngine.GameObject obj)
        {
            return obj.transform.childCount;
        }

        /// <summary>
        /// This の親を設定します。
        /// </summary>
        public static void SetParent(this UnityEngine.GameObject obj, UnityEngine.GameObject parent, bool worldPositionStays = true)
        {
            obj.transform.SetParent(parent.transform, worldPositionStays);
        }

        /// <summary>
        /// This の親を取得します。
        /// </summary>
        public static UnityEngine.GameObject GetParent(this UnityEngine.GameObject obj)
        {
            if (obj.transform.parent == null)
            {
                return null;
            }
            return obj.transform.parent.gameObject;
        }

        /// <summary>
        /// 親( parent )の子かどうか。
        /// </summary>
        public static bool IsChildOf(this UnityEngine.GameObject obj, UnityEngine.GameObject parent)
        {
            return obj.transform.IsChildOf(parent.transform);
        }

        /// <summary>
        /// 全ての子オブジェクトを親オブジェクトから切り離します。
        /// </summary>
        public static void DetachChildren(this UnityEngine.GameObject obj)
        {
            obj.transform.DetachChildren();
        }

        // Unique functions
        /// <summary>
        /// 全ての子オブジェクトに対して処理を実行します。
        /// </summary>
        public static void ForeachChild(this UnityEngine.GameObject obj, Action<UnityEngine.GameObject> func)
        {
            for (int i = 0; i < obj.GetChildCount(); ++i)
            {
                func(obj.GetChild(i));
            }
        }

        /// <summary>
        /// 子オブジェクトの内<T>がアタッチされている全てオブジェクトに対して処理を実行します。
        /// </summary>
        public static void ForeachChild<T>(this UnityEngine.GameObject obj, Action<T> func)
        {
            for (int i = 0; i < obj.GetChildCount(); ++i)
            {
                T ty = obj.GetChild(i).GetComponent<T>();
                if (ty != null)
                {
                    func(ty);
                }
            }
        }


    }

}   // End of namespace XUnityString
}   // End of namespace AkiVACO