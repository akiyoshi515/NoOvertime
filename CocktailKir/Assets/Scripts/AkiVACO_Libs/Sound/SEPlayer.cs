
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

    public class SEPlayer
    {

        private SEPlayerUnit m_objPlayerUnit = null;

        public void Initialize()
        {
            GameObject obj = XFunctions.FindSingleObjectWithTag(LibConstants.Tag.SoundPlayer, "SEPlayer");
            XLogger.LogValidObject(obj, LibConstants.ErrorMsg.GetMsgNotFoundObject("Tag:SEPlayer"));

            m_objPlayerUnit = obj.GetComponent<SEPlayerUnit>();
            XLogger.LogValidObject(m_objPlayerUnit, LibConstants.ErrorMsg.GetMsgNotBoundComponent("SEPlayerUnit"));

        }

        public SEUnit Play(AudioClip clip, Vector3 position, Quaternion rotate)
        {
            return m_objPlayerUnit.AllocPlayer(clip, position, rotate);
        }

        public SEUnit Play(AudioClip clip, Vector3 position, Quaternion rotate, float volumeScale)
        {
            return m_objPlayerUnit.AllocPlayer(clip, position, rotate, volumeScale);
        }

        public SEUnit PlayLoop(AudioClip clip, Vector3 position, Quaternion rotate)
        {
            return m_objPlayerUnit.AllocLoopPlayer(clip, position, rotate);
        }

        public SEUnit PlayLoop(AudioClip clip, Vector3 position, Quaternion rotate, float volumeScale)
        {
            return m_objPlayerUnit.AllocLoopPlayer(clip, position, rotate, volumeScale);
        }

    }

}