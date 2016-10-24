using UnityEngine;
using System.Collections;

using AkiVACO;

public class TestPlayer : MonoBehaviour {

    void Awake()
    {
        if (XVInput.GetConnectedNum() == 0)
        {
            XVInput.CreateInterface(UserID.User1, XVInputType.Keyboard);
        }
        else
        {
            string[] table = XVInput.GetConnectedNames();

            XLogger.Log("Enumerate Controller");
            foreach (string str in table)
            {
                XLogger.Log(str);
            }

            XVInput.CreateInterface(UserID.User1, XVInputType.Controller);
        }

        // TODO
        XVInput.CreateInterface(UserID.User2, XVInputType.None);
        XVInput.CreateInterface(UserID.User3, XVInputType.None);
        XVInput.CreateInterface(UserID.User4, XVInputType.None);
    }

	// Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
    void Update()
    {
    }
}
