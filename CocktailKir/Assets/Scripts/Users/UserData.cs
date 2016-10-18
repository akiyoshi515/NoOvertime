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
        protected set { m_userID = value; }
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
        if (!(XVInput.CreateInterface(userID, inputType)))
        {
            XLogger.LogError("Create XVInputInterface " + userID.ToString() + " : " + inputType.ToString());
        }
    }

}
