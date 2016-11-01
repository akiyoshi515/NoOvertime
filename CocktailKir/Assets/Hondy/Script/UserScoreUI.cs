using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UserScoreUI
    : 
    MonoBehaviour
{

    [SerializeField,Header("フォントのスプライト")]
    Sprite[] m_numberSpriteArray;

    ///
    /// <summary>   UIImage 0が一の位～3が百の位.   </summary>
    ///

    [SerializeField,Header("0から1の位,1は10の位....")]
    Image[] m_numberImage;

    ///
    /// <summary>   The user score. </summary>
    ///
    [SerializeField,Header("スコア入れたらUIにでるよ")]
    int m_userScore;

    // ユーザーのスコア ここに数値入れるとUIに反映されます
    public int UserScore
    {
        get { return m_userScore; }
        set { m_userScore = value; }
    }


    [SerializeField ,Header("ユーザーごとのフォント色")]
    Color[] m_userColor = new Color[4];

    ///
    /// <summary>   Identifier for the user.    </summary>
    ///
    [SerializeField]
    UserID m_userID;
    public UserID UserID
    {
        get
        {
            return m_userID;
        }
        set
        {
            m_userID = value;
            // 色変更
            for (int i = 0; i < m_numberImage.Length; i++)
            {
                m_numberImage[i].color = m_userColor[(int)m_userID];
            }
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // カウントアップ演出はアルファ終わってから?

        int temp = m_userScore;


        for (int i = 0; i < m_numberImage.Length;i++)
        {
            // 1の位
            m_numberImage[i].sprite = m_numberSpriteArray[temp % 10];

            // 10の位
            temp /= 10;

        }

    }
}
