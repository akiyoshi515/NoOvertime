using UnityEngine;
using System.Collections;

using AkiVACO;
using XVInputInternal;

public static class XVInput
{
    private static IXVInput[] m_input = new IXVInput[4];

    public static IXVInput GetInterface(UserID id)
    {
        return (m_input[(int)id]);
    }

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
                XLogger.LogWarning("Create interface= XVInputController: id= " + i.ToString());
                m_input[i] = new XVInputController();
                break;
        }

        return true;
    }

}
