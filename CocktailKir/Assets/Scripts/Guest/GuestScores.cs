
using System.Collections;
using System.Collections.Generic;

public class GuestScores 
{
    private static int[] m_scoreTable = new int[4]{0, 0, 0, 0};
    public static int[] scoreTable
    {
        get { return m_scoreTable; }
    }

    private static int m_maxScore = 0;
    public static int maxScore
    {
        get { return m_maxScore; }
    }

    public static int firstHitCharmBonus
    {
        get;
        protected set;
    }

    public static void Reset(int firstCharmBonus)
    {
        m_scoreTable[0] = 0;
        m_scoreTable[1] = 0;
        m_scoreTable[2] = 0;
        m_scoreTable[3] = 0;
        m_maxScore = 0;

        firstHitCharmBonus = firstCharmBonus;
    }

    public static void Add(UserID id)
    {
        int idx = (int)id;
        m_scoreTable[idx] += 1;
    }

    public static void Add(UserID id, UserID prevId)
    {
        int idx = (int)id;
        int prev = (int)prevId;
        m_scoreTable[idx] += 1;
        m_scoreTable[prev] -= 1;
    }

    public static void AddMax()
    {
        ++m_maxScore;
    }

    public static int GetScore(UserID id)
    {
        int idx = (int)id;
        return m_scoreTable[idx];
    }

}
