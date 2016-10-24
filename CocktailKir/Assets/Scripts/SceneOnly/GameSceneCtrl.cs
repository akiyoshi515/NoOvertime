using UnityEngine;
using System.Collections;

public class GameSceneCtrl : MonoBehaviour
{
    [SerializeField]
    private int m_firstHitCharmBonus = 3;

    void Awake()
    {
        GuestScores.Reset(m_firstHitCharmBonus);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
