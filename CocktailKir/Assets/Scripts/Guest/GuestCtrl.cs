using UnityEngine;
using System.Collections;

public class GuestCtrl : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ballet")
        {
            col.gameObject.GetComponent<TestBalletCtrl>().SendHit();
        }
        else if (col.tag == "BalletTrigger")
        {
            AkiVACO.XLogger.Log("Hit!");
        }
    }

}
