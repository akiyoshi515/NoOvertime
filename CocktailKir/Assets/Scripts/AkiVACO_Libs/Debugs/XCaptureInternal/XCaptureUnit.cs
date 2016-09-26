
// Author     : Atuki Yoshinaga


using UnityEngine;
using System.Collections;

namespace AkiVACO
{

public class XCaptureUnit : MonoBehaviour
{
    public KeyCode m_keyCode = KeyCode.F;

    void Update()
    {
        if (Input.GetKeyDown(m_keyCode))
        {
            XCapture.Capture(
                "SS_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss_ffffff"),
                "ScreenShot");
        }
    }

}

}