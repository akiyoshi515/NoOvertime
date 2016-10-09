using UnityEngine;
using System.Collections;

public class TestBalletCtrl : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag != "Ballet")
        {
            Destroy(this.gameObject);
        }
    }
}
