using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System;

public class ResultSceneCtrl : MonoBehaviour {

    [SerializeField]
    Text[] m_userScores;


    int[] m_integerScores;

    struct UserScore
    {
        public UserID id;
        public int score;
    }
    [SerializeField]
    Color[] m_userColor;

    UserScore[] scores = new UserScore[4];
	// Use this for initialization
	void Start ()
    {
        
        for (int i = 0;i < m_userScores.Length;i++)
	    {
            scores[i].id = (UserID)(i);
            scores[i].score = GuestScores.GetScore(scores[i].id);
	    }


        Array.Sort(scores, (a, b) => b.score - a.score);

        for (int i = 0; i < m_userScores.Length; i++)
        {
            switch (scores[i].id)
            {
                case UserID.User1:
                    m_userScores[i].color = m_userColor[0];
                    m_userScores[i].text += " 1P " + scores[i].score.ToString();
                    break;
                case UserID.User2:
                    m_userScores[i].color = m_userColor[1];
                    m_userScores[i].text += " 2P " + scores[i].score.ToString();
                    break;
                case UserID.User3:
                    m_userScores[i].color = m_userColor[2];
                    m_userScores[i].text += " 3P " + scores[i].score.ToString();
                    break;
                case UserID.User4:
                    m_userScores[i].color = m_userColor[3];
                    m_userScores[i].text += " 4P " + scores[i].score.ToString();
                    break;
                default:
                    break;

            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
