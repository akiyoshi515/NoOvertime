
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

using AkiVACO.XValue;

namespace AkiVACO
{

    public class ResolutionCtrl : MonoBehaviour
    {
        [SerializeField]
        private float BaseWidth = 1280.0f;

        [SerializeField]
        private int RefreshRate = 20;

        void Awake()
        {
            float scale = BaseWidth / Screen.width;
            scale.MaxLimited(1.0f);
            int width = (int)(Screen.width * scale);
            int height = (int)(Screen.height * scale);
            Screen.SetResolution(width, height, true, RefreshRate);
            XLogger.Log("ChangeResolution Base:" + BaseWidth.ToString() + " Rate:" + RefreshRate.ToString());
        }

    }

}