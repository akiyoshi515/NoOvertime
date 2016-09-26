
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

namespace XLoggerInternal
{

    public class XLoggerGUIUnit : MonoBehaviour
    {
        public bool isEnableView = true;

        public Color m_backgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        public float m_positionX = 10.0f;
        public float m_positionY = 5.0f;
        public float m_weight = 150.0f;

        public float m_moveSpeedX = 3.0f;
        public float m_moveSpeedY = 2.0f;

        public KeyCode m_enableKey = KeyCode.L;
        private XVRKeyCode m_moveLogMenuKey = XVRKeyCode.Num11;
        public KeyCode m_dumpLogMenuKey = KeyCode.P;

        private float m_height = 12.0f;
        private float m_positionStride = 12.0f;

        private GUIStyle m_style = null;
        private GUIStyleState m_styleState = null;

        private bool m_moveLog = false;

        // Awake is called when the script instance is being loaded.
        void Awake()
        {
            m_style = new GUIStyle();
            m_styleState = new GUIStyleState();

            Application.logMessageReceived += this.LogCallBackHandler;
        }

        void OnDestroy()
        {
            Application.logMessageReceived -= this.LogCallBackHandler;
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(m_enableKey))
            {
                this.isEnableView = !(this.isEnableView);
            }

            if (this.isEnableView)
            {
                if (XVRInput.IsTrigForced(m_moveLogMenuKey))
                {
                    if (m_moveLog || (!XVRInput.enabledDebug))
                    {
                        m_moveLog = !m_moveLog;
                        XVRInput.EnableDebug(m_moveLog);
                    }
                }
                if (Input.GetKeyDown(m_dumpLogMenuKey))
                {
                    XLogger.Log("DebugSystem:DumpLog");
                    XLogger.DumpLog();
                }
                if (m_moveLog)
                {
                    if (XVRInput.IsHoldDbg(XVRKeyCode.Up))
                    {
                        m_positionY -= m_moveSpeedY;
                    }
                    if (XVRInput.IsHoldDbg(XVRKeyCode.Down))
                    {
                        m_positionY += m_moveSpeedY;
                    }
                    if (XVRInput.IsHoldDbg(XVRKeyCode.Left))
                    {
                        m_positionX -= m_moveSpeedX;
                    }
                    if (XVRInput.IsHoldDbg(XVRKeyCode.Right))
                    {
                        m_positionX += m_moveSpeedX;
                    }
                }
            }

        }

        void OnGUI()
        {
            if (!isEnableView){
                return;
            }

            IEnumerator logs = XLogger.GetLogEnumerator();

            if (logs == null){
                return;
            }

            Color prevColor = GUI.backgroundColor;
            Color setColor = m_backgroundColor;
            if (m_moveLog)
            {
                setColor.a = 1.0f;
            }
            GUI.backgroundColor = setColor;

            m_styleState.background = Texture2D.whiteTexture;
            m_style.normal = m_styleState;

            float positionY = m_positionY;

            do{
                try{
                    string msg = logs.Current.ToString();
                    GUI.Label(new Rect(m_positionX, positionY, m_weight, m_height), msg, m_style);
                    positionY += m_positionStride;
                }catch{
                }
            } while (logs.MoveNext());

            GUI.backgroundColor = prevColor;
        }
        
        void LogCallBackHandler(string condition, string stackTrace, LogType type)
        {
            if (condition[0] == '<')    // RichText
            {
                return;
            }

            switch(type)
            {
                case LogType.Log:
                    XLogger.PushDump("Log", condition);
                    break;
                case LogType.Warning:
                    XLogger.PushDump("Warning", condition);
                    break;
                case LogType.Error:
                    XLogger.PushDump("Error", condition);
                    break;
                case LogType.Exception:
                    XLogger.PushDump("Exception", condition);
                    break;
                case LogType.Assert:
                    XLogger.PushDump("Assert", condition);
                    break;
            }
        }

        void EnableView()
        {
            isEnableView = true;
        }

        void DisableView()
        {
            isEnableView = false;
        }

    }

}
}