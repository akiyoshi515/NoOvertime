using UnityEngine;
using System.Collections;

using AkiVACO;
using XVInputInternal;

public static class XVInput
{
    private static IXVInput[] m_input = new IXVInput[4];

    public static IXVInput GetInterface(int i)
    {
        XLogger.LogValidObject(!IsValidSlotIndex(i), "Invalid Argument: " + i.ToString());
        return (m_input[i]);
    }

    public static bool CreateInterface(int i, XVInputType type)
    {
        XLogger.LogValidObject(!IsValidSlotIndex(i), "Invalid Argument: " + i.ToString());

        switch (type)
        {
            case XVInputType.None:
                XLogger.LogWarning("Create interface= XVInputNone: id= " + i.ToString());
                m_input[i] = new XVInputNone();
                break;
            case XVInputType.Keyboard:
                m_input[i] = new XVInputKeyboard();
                break;
            case XVInputType.Controller:
                XLogger.LogWarning("Create interface= XVInputController: id= " + i.ToString());
                m_input[i] = new XVInputController();
                break;
        }

        return true;
    }

    public static bool IsValidSlotIndex(int i)
    {
        if (((i < 0) || (i > 3)))
        {
            return false;
        }
        return true;
    }

}
