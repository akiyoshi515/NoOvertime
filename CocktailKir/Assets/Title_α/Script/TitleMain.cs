using UnityEngine;
using System.Collections;

public class TitleMain : MonoBehaviour {

    //AudioSource bgm;
    ChangeAlpha key;
    ChangeAlpha fade;
    TitleSE     se;

    enum TITLE_STATE
    {
        MAIN = 0,
        FADE_IN,
        UNINIT,
        MAX_NUM
    };
    TITLE_STATE _titleState;



	// Use this for initialization
	void Start ()
    {
        //bgm     = gameObject.GetComponent<AudioSource>();
        se      = GameObject.Find("SoundEffect").GetComponent<TitleSE>();

        key     = GameObject.Find("PressAnyKey").GetComponent<ChangeAlpha>();
        fade    = GameObject.Find("FadePanel").GetComponent<ChangeAlpha>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        key.Flash();
        switch (_titleState)
        {
            case TITLE_STATE.MAIN:
                if(Input.anyKeyDown)
                {
                    se.PlaySE();
                    _titleState = TITLE_STATE.FADE_IN;
                }
                break;

            case TITLE_STATE.FADE_IN:
                if (fade.AddAlpha())
                {
                    _titleState = TITLE_STATE.UNINIT;
                }
                break;

            case TITLE_STATE.UNINIT:
                Debug.Log("load");
                //TODO ロード

                break;
        }
	}
}
