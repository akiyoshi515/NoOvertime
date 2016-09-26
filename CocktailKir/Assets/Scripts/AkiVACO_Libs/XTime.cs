
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

    public class XTime
    {
        private static float m_timeScale = 1.0f;

        public static float timeScale
        {
            get { return m_timeScale; }
            set { m_timeScale = value; }
        }

        public static float deltaTime
        {
            get { return Time.deltaTime * m_timeScale; }
        }

    }

}