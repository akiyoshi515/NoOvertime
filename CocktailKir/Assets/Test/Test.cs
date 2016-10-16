using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKey(KeyCode.A)){
            this.transform.Translate(-1.0f * Time.deltaTime, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(1.0f * Time.deltaTime, 0.0f, 0.0f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Hit Collision");
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hit Collider");
    }

}
