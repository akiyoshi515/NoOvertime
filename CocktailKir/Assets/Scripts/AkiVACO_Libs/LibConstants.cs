
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

    public class LibConstants
    {

        public class Tag
        {
            public const string SoundPlayer = "SoundPlayer";
            public const string SEUnit = "SEUnit";
        }

        public class ErrorMsg
        {
            public const string NotFoundObject = "The specified object was not found ";
            public const string NotBoundComponent = "Component is not bound ";

            public static string GetMsgNotFoundObject(string objname)
            {
                return NotFoundObject + "<" + objname + ">";
            }

            public static string GetMsgNotBoundComponent(string objname)
            {
                return NotBoundComponent + "<" + objname + ">";
            }

        };

    }

}