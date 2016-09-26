
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

namespace XLoggerInternal
{
    public class XLoggerConsole
    {
        public void Log(object msg)
        {
            UnityEngine.Debug.Log(msg);
        }

        public void Log(object msg, UnityEngine.Object obj)
        {
            UnityEngine.Debug.Log(msg, obj);
        }

        public void LogFormat(string fmt, params object[] args)
        {
            UnityEngine.Debug.LogFormat(fmt, args);
        }

        public void LogFormat(UnityEngine.Object obj, string fmt, params object[] args)
        {
            UnityEngine.Debug.LogFormat(obj, fmt, args);
        }

        public void LogError(object msg)
        {
            UnityEngine.Debug.LogError(msg);
        }

        public void LogError(object msg, UnityEngine.Object obj)
        {
            UnityEngine.Debug.LogError(msg, obj);
        }

        public void LogWarning(object msg)
        {
            UnityEngine.Debug.LogWarning(msg);
        }

        public void LogWarning(object msg, UnityEngine.Object obj)
        {
            UnityEngine.Debug.LogWarning(msg, obj);
        }

        public void LogException(System.Exception exception)
        {
            UnityEngine.Debug.LogException(exception);
        }

        public void LogException(System.Exception exception, UnityEngine.Object obj)
        {
            UnityEngine.Debug.LogException(exception, obj);
        }


    }

}
}