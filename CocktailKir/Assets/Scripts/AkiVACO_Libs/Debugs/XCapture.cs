
// Author     : Atuki Yoshinaga


#define XCAPTURE_ENABLE

#if UNITY_ANDROID
#undef XCAPTURE_ENABLE
#endif

namespace AkiVACO
{

#if XCAPTURE_ENABLE

using UnityEngine;
using System.Collections;
using System.IO;

public class XCapture {

    public static void Capture(string filename, string pass)
    {
        XLogger.Log("Capture Screenshot! : pass = " + pass);
        
        if(!(Directory.Exists(pass))){
            Directory.CreateDirectory(pass);
        }

        if(Application.platform == RuntimePlatform.WindowsEditor){
            Application.CaptureScreenshot(pass + "/" + filename + ".png");
        }else if(Application.platform == RuntimePlatform.WindowsPlayer){
            Application.CaptureScreenshot("../" + pass + "/" + filename + ".png");
        }
    }

}

#else

public class XCapture {

    public static void Capture(string filename, string pass)
    {
    }

}

#endif

}
