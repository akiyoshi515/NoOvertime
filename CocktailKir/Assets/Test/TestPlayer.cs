using UnityEngine;
using System.Collections;

using AkiVACO;

public class TestPlayer : MonoBehaviour {

    void Awake()
    {
        XVInput.CreateInterface(UserID.User1, XVInputType.Keyboard);
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
