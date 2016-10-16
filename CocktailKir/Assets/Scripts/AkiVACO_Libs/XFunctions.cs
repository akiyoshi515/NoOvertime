
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

public class XFunctions
{
    public static UnityEngine.GameObject Instance(
        UnityEngine.GameObject obj,
        Vector3 vec,
        Quaternion rotate)
    {
        UnityEngine.GameObject reobj = Object.Instantiate(obj, vec, rotate) as GameObject;
        reobj.name = obj.name;
        return reobj;
    }

    public static UnityEngine.GameObject InstanceRename(
        UnityEngine.GameObject obj,
        Vector3 vec,
        Quaternion rotate,
        string rename)
    {
        UnityEngine.GameObject reobj = Object.Instantiate(obj, vec, rotate) as GameObject;
        reobj.name = rename;
        return reobj;
    }

    public static UnityEngine.GameObject InstanceChild(
        UnityEngine.GameObject obj,
        Vector3 vec,
        Quaternion rotate,
        UnityEngine.GameObject objParent)
    {
        UnityEngine.GameObject reobj = XFunctions.Instance(obj, vec, rotate);
        reobj.transform.SetParent(objParent.transform);
        reobj.transform.localPosition = vec;
        reobj.transform.localRotation = rotate;
        return reobj;
    }

    public static UnityEngine.GameObject InstanceChildRename(
        UnityEngine.GameObject obj,
        Vector3 vec,
        Quaternion rotate,
        string rename,
        UnityEngine.GameObject objParent)
    {
        UnityEngine.GameObject reobj = XFunctions.InstanceRename(obj, vec, rotate, rename);
        reobj.transform.SetParent(objParent.transform);
        return reobj;
    }

    public static UnityEngine.GameObject FindSingleObjectWithTag(string tag)
    {
        GameObject[] table = GameObject.FindGameObjectsWithTag(tag);
        if (table.Length != 1)
        {
            if (table.Length == 0)
            {
                XLogger.LogWarning("No instance object Tag:" + tag);
                return null;
            }
            else
            {
                XLogger.LogWarning("No single instance object Tag:" + tag);
            }
        }
        return table[0];
    }

    public static UnityEngine.GameObject FindSingleObjectWithTag(string tag, string name)
    {
        GameObject[] table = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject obj in table)
        {
            if(obj.name == name){
                return obj;
            }
        }
        if(table.Length == 1){
            return table[0];
        }
        return null;
    }

}

}