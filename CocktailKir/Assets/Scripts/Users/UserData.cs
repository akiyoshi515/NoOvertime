
using UnityEngine;
using System.Collections;

using AkiVACO;

public class UserData : MonoBehaviour
{
    [SerializeField]
    private UserID m_userID = UserID.User1;
    public UserID userID
    {
        get { return m_userID; }
#if UNITY_EDITOR
        set { m_userID = value; }
#else
        protected set { m_userID = value; }
#endif
    }

    [SerializeField]
    private XVInputType m_inputType = XVInputType.None;
    public XVInputType inputType
    {
        get { return m_inputType; }
        protected set { m_inputType = value; }
    }

    public IXVInput input
    {
        get { return XVInput.GetInterface(m_userID); }
    }

    void Awake()
    {
        if (XVInput.GetInterface(m_userID) == null)
        {
            XLogger.LogWarning("Debug Create XVInputInterface " + userID.ToString() + " : " + inputType.ToString());
            if (!(XVInput.CreateInterface(userID, inputType)))
            {
                XLogger.LogError("Error Create XVInputInterface " + userID.ToString() + " : " + inputType.ToString());
            }
        }
    }

}
