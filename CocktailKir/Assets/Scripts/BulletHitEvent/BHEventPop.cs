using UnityEngine;
using System.Collections;

public class BHEventPop : BHEventBase
{
    [SerializeField]
    private GameObject[] m_popObject = null;

    protected override void OnHitEvent(GameObject obj)
    {
        foreach (GameObject popObj in m_popObject)
        {
            AkiVACO.XFunctions.Instance(popObj, this.transform.position, this.transform.rotation);
        }
    }
}
