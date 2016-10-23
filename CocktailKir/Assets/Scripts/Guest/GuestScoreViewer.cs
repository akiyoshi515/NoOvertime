using UnityEngine;
using System.Collections;

public class GuestScoreViewer : MonoBehaviour
{
    [SerializeField]
    private bool dummy = false;

    void LateUpdate()
    {
        dummy = !dummy;
    }
}
