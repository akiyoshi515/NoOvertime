
// Author     : Atuki Yoshinaga


#define XLOGGER_ENABLE

#define XLOGGER_SCREENLOG
#define XLOGGER_CONSOLELOG
#define XLOGGER_DUMPLOG

//#define XLOGGER_DUMPLOG_CSV
//#define XLOGGER_DUMPLOG_TEXT

#if DEBUG
#else
#undef XLOGGER_ENABLE
#endif

#if XLOGGER_ENABLE
#else
#undef XLOGGER_SCREENLOG
#undef XLOGGER_CONSOLELOG
#undef XLOGGER_DUMPLOG
#endif

#if UNITY_EDITER                // Unity Editer
#elif UNITY_STANDALONE_WIN      // Windows
#elif UNITY_STANDALONE_OSX      // MacOS
#elif UNITY_STANDALONE_LINUX    // Linux
#else
#undef XLOGGER_DUMPLOG
#endif

#if XLOGGER_DUMPLOG
#if XLOGGER_DUMPLOG_CSV
#undef XLOGGER_DUMPLOG_TEXT
#elif XLOGGER_DUMPLOG_TEST
#undef XLOGGER_DUMPLOG_CSV
#else
#define XLOGGER_DUMPLOG_CSV
#endif
#endif

namespace AkiVACO
{

#if XLOGGER_ENABLE

using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System;

public class XLogger{

    private const int LOG_CAPACITY = 20;
    private const int LOG_DUMP_CAPACITY = 300;

    private static XLoggerInternal.XLoggerConsole m_logger = null;
    private static XLoggerInternal.XLoggerLogList m_logList = null;
    
#if XLOGGER_DUMPLOG
    private static XLoggerInternal.XLoggerLogList m_logDumpList = null;
#endif

    private static XUnityString.RichStringInfo m_strInfoLog = null;
    private static XUnityString.RichStringInfo m_strInfoError = null;
    private static XUnityString.RichStringInfo m_strInfoWarning = null;
    private static XUnityString.RichStringInfo m_strInfoException = null;

    public static IEnumerator GetLogEnumerator()
    {
        if (m_logList == null)
        {
            return null;
        }
        else
        {
            return m_logList.GetEnumerator();
        }
    }

    public static void SetStringInfoLog(XUnityString.RichStringInfo rsi)
    {
        m_strInfoLog = new XUnityString.RichStringInfo(rsi);
    }

    public static void SetStringInfoError(XUnityString.RichStringInfo rsi)
    {
        m_strInfoError = new XUnityString.RichStringInfo(rsi);
    }

    public static void SetStringInfoWarning(XUnityString.RichStringInfo rsi)
    {
        m_strInfoWarning = new XUnityString.RichStringInfo(rsi);
    }

    public static void SetStringInfoException(XUnityString.RichStringInfo rsi)
    {
        m_strInfoException = new XUnityString.RichStringInfo(rsi);
    }

    private static void CreateLogger()
    {
        if (m_logger == null)
        {
            m_logger = new XLoggerInternal.XLoggerConsole();
#if XLOGGER_SCREENLOG
            m_logList = new XLoggerInternal.XLoggerLogList(LOG_CAPACITY);
#endif
#if XLOGGER_DUMPLOG
            m_logDumpList = new XLoggerInternal.XLoggerLogList(LOG_DUMP_CAPACITY);
#endif
            if (m_strInfoLog == null)
            {
                m_strInfoLog = new XUnityString.RichStringInfo();
                m_strInfoLog.Coloring(XUnityString.XColors.BLACK);
            }
            if (m_strInfoError == null)
            {
                m_strInfoError = new XUnityString.RichStringInfo();
                m_strInfoError.Coloring(XUnityString.XColors.RED);
            }
            if (m_strInfoWarning == null)
            {
                m_strInfoWarning = new XUnityString.RichStringInfo();
                m_strInfoWarning.Coloring(XUnityString.XColors.YELLOW);
            }
            if (m_strInfoException == null)
            {
                m_strInfoException = new XUnityString.RichStringInfo();
                m_strInfoException.Coloring(XUnityString.XColors.LIME);
                m_strInfoException.Italic();
            }
        }
    }

    public static void Log(object msg)
    {
        if (XLogger.IsEnable())
        {
            XLogger.CreateLogger();
            string str = m_strInfoLog.Collection(msg.ToString());
#if XLOGGER_CONSOLELOG
            m_logger.Log(str);
#endif
            XLogger.Push(str);
            XLogger.PushDump("Log", msg);
        }
    }

    public static void Log(object msg, UnityEngine.Object obj)
    {
        if (XLogger.IsEnable())
        {
            XLogger.CreateLogger();
            string str = m_strInfoLog.Collection(msg.ToString());
#if XLOGGER_CONSOLELOG
            m_logger.Log(str, obj);
#endif
            XLogger.Push(str);
            XLogger.PushDump("Log", msg, obj);
        }
    }

    public static void LogError(object msg)
    {
        if (XLogger.IsEnable())
        {
            XLogger.CreateLogger();
            string str = m_strInfoError.Collection(msg.ToString());
#if XLOGGER_CONSOLELOG
            m_logger.LogError(str);
#endif
            XLogger.Push(str);
            XLogger.PushDump("Error", msg);
        }
    }

    public static void LogError(object msg, UnityEngine.Object obj)
    {
        if (XLogger.IsEnable())
        {
            XLogger.CreateLogger();
            string str = m_strInfoError.Collection(msg.ToString());
#if XLOGGER_CONSOLELOG
            m_logger.LogError(str, obj);
#endif
            XLogger.Push(str);
            XLogger.PushDump("Error", msg, obj);
        }
    }

    public static void LogValidObject(bool b, object msg)
    {
        if (b)
        {
            XLogger.LogError(msg);
        }
    }

    public static void LogValidObject(bool b, object msg, UnityEngine.Object obj)
    {
        if (b)
        {
            XLogger.LogError(msg, obj);
        }
    }

    public static void LogValidObject(UnityEngine.Object validObj, object msg)
    {
        if (validObj == null)
        {
            XLogger.LogError(msg);
        }
    }

    public static void LogValidObject(UnityEngine.Object validObj, object msg, UnityEngine.Object obj)
    {
        if (validObj == null)
        {
            XLogger.LogError(msg, obj);
        }
    }

    public static void LogWarning(object msg)
    {
        if (XLogger.IsEnable())
        {
            XLogger.CreateLogger();
            string str = m_strInfoWarning.Collection(msg.ToString());
#if XLOGGER_CONSOLELOG
            m_logger.LogWarning(str);
#endif
            XLogger.Push(str);
            XLogger.PushDump("Warning", msg);
        }
    }

    public static void LogWarning(object msg, UnityEngine.Object obj)
    {
        if (XLogger.IsEnable())
        {
            XLogger.CreateLogger();
            string str = m_strInfoWarning.Collection(msg.ToString());
#if XLOGGER_CONSOLELOG
            m_logger.LogWarning(str, obj);
#endif
            XLogger.Push(str);
            XLogger.PushDump("Warning", msg, obj);
        }
    }

    public static void LogException(System.Exception exception)
    {
        if (XLogger.IsEnable())
        {
            XLogger.CreateLogger();
#if XLOGGER_CONSOLELOG
            m_logger.LogException(exception);
#endif
            string str = m_strInfoException.Collection(exception.ToString());
            XLogger.Push(str);
            XLogger.PushDump("Exception", exception.ToString());
        }
    }

    public static void LogException(System.Exception exception, UnityEngine.Object obj)
    {
        if (XLogger.IsEnable())
        {
            XLogger.CreateLogger();
#if XLOGGER_CONSOLELOG
            m_logger.LogException(exception, obj);
#endif
            string str = m_strInfoException.Collection(exception.ToString());
            XLogger.Push(str);
            XLogger.PushDump("Exception", exception.ToString(), obj);
        }
    }

    public static bool IsEnable()
    {
        return (UnityEngine.Debug.isDebugBuild);
    }

    public static void DumpLog()
    {
#if XLOGGER_DUMPLOG
        IEnumerator logs = XLogger.m_logDumpList.GetEnumerator();

        if (logs == null)
        {
            return;
        }

#if XLOGGER_DUMPLOG_TEXT
        StreamWriter sw = new StreamWriter("./LogFile.txt", true);
#endif
#if XLOGGER_DUMPLOG_CSV
        FileInfo fi = new FileInfo("./LogFile.csv");
        StreamWriter sw = fi.AppendText();
#endif
        sw.WriteLine("");   // Blank
        sw.WriteLine(DateTime.Now.ToLongTimeString() + "," + DateTime.Now.ToLongDateString());
        sw.WriteLine("Time" + "," + "Tags" + "," + "Object" + "," + "Message");

        do
        {
            try{
                sw.WriteLine(logs.Current.ToString());
            }catch{
            }
        } while (logs.MoveNext());
        
        sw.Flush();
        sw.Close();
        XLogger.m_logDumpList.Clear();
#endif
    }

    private static void Push(object msg)
    {
#if XLOGGER_SCREENLOG
        m_logList.Push(msg);
#endif
    }

    public static void PushDump(object tag, object msg)
    {
#if XLOGGER_DUMPLOG
        m_logDumpList.Push(
            UnityEngine.Time.realtimeSinceStartup.ToString() + ","
            + tag + "," 
            + "null" + ","
            + msg);
#endif
    }

    public static void PushDump(object tag, object msg, UnityEngine.Object obj)
    {
#if XLOGGER_DUMPLOG
        m_logDumpList.Push(
            UnityEngine.Time.realtimeSinceStartup.ToString() + "," 
            + tag + "," 
            + obj.ToString() + "," 
            + msg);
#endif
    }

}

#else   // XLOGGER_ENABLE

using System.Collections;

public class XLogger{

    public static IEnumerator GetLogEnumerator()
    {
        return null;
    }

    public static void SetStringInfoLog(XUnityString.RichStringInfo rsi)
    {
    }

    public static void SetStringInfoError(XUnityString.RichStringInfo rsi)
    {
    }

    public static void SetStringInfoWarning(XUnityString.RichStringInfo rsi)
    {
    }

    public static void SetStringInfoException(XUnityString.RichStringInfo rsi)
    {
    }

    private static void CreateLogger()
    {
    }

    public static void Log(object msg)
    {
    }

    public static void Log(object msg, UnityEngine.Object obj)
    {
    }

    public static void LogError(object msg)
    {
    }

    public static void LogError(object msg, UnityEngine.Object obj)
    {
    }
    
    public static void LogValidObject(bool b, object msg)
    {
    }

    public static void LogValidObject(bool b, object msg, UnityEngine.Object obj)
    {
    }

    public static void LogValidObject(UnityEngine.Object validObj, object msg)
    {
    }

    public static void LogValidObject(UnityEngine.Object validObj, object msg, UnityEngine.Object obj)
    {
    }

    public static void LogWarning(object msg)
    {
    }

    public static void LogWarning(object msg, UnityEngine.Object obj)
    {
    }

    public static void LogException(System.Exception exception)
    {
    }

    public static void LogException(System.Exception exception, UnityEngine.Object obj)
    {
    }

    public static bool IsEnable()
    {
        return false;
    }

    public static void DumpLog()
    {}

    private static void Push(object msg)
    {
    }

    private static void PushDump(object tag, object msg)
    {}

    private static void PushDump(object tag, object msg, UnityEngine.Object obj)
    {}
}

#endif

}