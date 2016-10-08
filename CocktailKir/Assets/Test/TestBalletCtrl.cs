using UnityEngine;
using System.Collections;

public class TestBalletCtrl : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Destroy(this.gameObject);
    }

}
