using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UserScoreUI
    : 
    MonoBehaviour
{

    [SerializeField]
    Sprite[] m_numberSpriteArray;

    ///
    /// <summary>   UIImage 0が一の位～3が百の位.   </summary>
    ///

    [SerializeField]
    Image[] m_numberImage;

    ///
    /// <summary>   The user score. </summary>
    ///

    int m_userScore;

    // ユーザーのスコア ここに数値入れるとUIに反映されます
    public int UserScore
    {
        get { return m_userScore; }
        set { m_userScore = value; }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // カウントアップ演出はアルファ終わってから

        int temp = m_userScore;

        // 1の位
        m_numberImage[0].sprite = m_numberSpriteArray[temp % 10];

        // 10の位
        temp /= 10;
        m_numberImage[1].sprite = m_numberSpriteArray[temp % 10];

        // 100の位
        temp /= 10;
        m_numberImage[2].sprite = m_numberSpriteArray[temp % 10];

    }
}
