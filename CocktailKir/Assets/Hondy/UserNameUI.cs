using UnityEngine;
using System.Collections;

public class UserNameUI : MonoBehaviour {



    void Start()
    {
        this_t_ = this.transform;
    }

    public Camera targetCamera;

    Transform this_t_;

    void Awake()
    {
        this_t_ = this.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position - targetCamera.transform.rotation * Vector3.back, targetCamera.transform.rotation * Vector3.up);

    }

}
