using UnityEngine;
using System.Collections;

public class TitleSE : MonoBehaviour {

    AudioSource se;

	// Use this for initialization
	void Start ()
    {
        se = gameObject.GetComponent<AudioSource>();	
	}

    public void PlaySE()
    {
        se.Play();
    }

}
