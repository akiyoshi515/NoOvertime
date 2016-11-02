using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeAlpha : MonoBehaviour {

    Image myImage;
    Color workColor;
    float num = 0.5f;

    const float MAX_ALPHA = 1.0f;
    const float MIN_ALPHA = 0.0f;

	// Use this for initialization
	void Start ()
    {
        myImage = gameObject.GetComponent<Image>();
        workColor = myImage.color;
	}

    /// <summary>
    /// 点滅
    /// </summary>
    public void Flash()
    {
        if ((workColor.a > MAX_ALPHA) || (workColor.a < MIN_ALPHA))
        {
            num *= (-1);
        }
        workColor.a += num * Time.deltaTime;
        //keyLogoColor = workColor;
        myImage.color = workColor;
    }

    /// <summary>
    /// α値増加
    /// </summary>
    public bool AddAlpha()
    {
        if(workColor.a < MAX_ALPHA)
        {
            workColor.a += num * Time.deltaTime;
            myImage.color = workColor;
        }
        else
        {
            return true;
        }
        return false;
    }
}
