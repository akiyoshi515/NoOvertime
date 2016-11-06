using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartCountDownUI : MonoBehaviour {

    [SerializeField]
    bool m_isUpdate;

    [SerializeField]
    bool m_isEnableDisplay;

    [SerializeField]
    float m_count;


    [SerializeField]
    Sprite[] m_numberSprites = new Sprite[10];

    [SerializeField]
    Image[] m_images = new Image[2];
    [SerializeField]
    Image[] m_Textimages = new Image[2];

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_isUpdate)
        {
            for (int i = 0; i < m_images.Length; i++)
            {
                m_images[i].enabled = m_isEnableDisplay;
            }
            for (int i = 0; i < m_Textimages.Length; i++)
            {
                m_Textimages[i].enabled = m_isEnableDisplay;
            }

            m_count -= AkiVACO.XTime.deltaTime;
            int temp = (int)m_count;
            for (int i = 0; i < m_images.Length; i++)
            {
                m_images[i].sprite = m_numberSprites[temp % 10];
                temp /= 10;
            }
        }
    }
}
