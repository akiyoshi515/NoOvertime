﻿using UnityEngine;
using System.Collections;

using AkiVACO;
using XVInputInternal;

public static class XVInput
{
    private static IXVInput[] m_input = new IXVInput[4];

    /// <summary>
    /// Inputのインターフェースを取得
    /// </summary>
    public static IXVInput GetInterface(UserID id)
    {
        return (m_input[(int)id]);
    }

    /// <summary>
    /// Inputのインターフェースを作成
    /// </summary>
    /// <param name="id">ユーザーID</param>
    /// <param name="type">入力のタイプ</param>
    /// <returns>作成成功か？</returns>
    public static bool CreateInterface(UserID id, XVInputType type)
    {
        int i = (int)id;

        switch (type)
        {
            case XVInputType.None:
                XLogger.LogWarning("Create interface= XVInputNone: id= " + i.ToString());
                m_input[i] = new XVInputNone();
                break;
            case XVInputType.Keyboard:
                XLogger.Log("Create interface= XVInputKeyboard: id= " + i.ToString());
                m_input[i] = new XVInputKeyboard();
                break;
            case XVInputType.Controller:
#if DEBUG
                if (XVInput.GetConnectedNum() == 0)
                {
                    if (id == UserID.User1)
                    {
                        m_input[i] = new XVInputKeyboard();
                        XLogger.LogWarning("Debug Create interface= XVInputKeyboard: id= " + i.ToString());
                        return true;
                    }
                }
#endif
                XLogger.Log("Create interface= XVInputController: id= " + i.ToString());
                m_input[i] = new XVInputController(id);
                break;
        }

        return true;
    }

    /// <summary>
    /// XBoxInputのインターフェースを作成
    /// </summary>
    /// <param name="id">ユーザーID</param>
    /// <param name="type">XBoxInputのUserID</param>
    /// <returns>作成成功か？</returns>
    public static bool CreateXBoxInterface(UserID id, XBoxInput.XBKeyCode.UserCode user)
    {
        int i = (int)id;

        XLogger.Log("Create interface= XVInputController: id= " + i.ToString());
        m_input[i] = new XVInputController(user);

        return true;
    }

    /// <summary>
    /// Inputのインターフェースを削除
    /// </summary>
    public static void ClearInterface()
    {
        m_input[0] = null;
        m_input[1] = null;
        m_input[2] = null;
        m_input[3] = null;
    }

    public static string[] GetConnectedNames()
    {
        return Input.GetJoystickNames();
    }

    public static int GetConnectedNum()
    {
        string[] table = Input.GetJoystickNames();

        if (table.Length == 1)
        {
            if (table[0] == "")
            {
                return 0;
            }
        }

        return table.Length;
    }

}
