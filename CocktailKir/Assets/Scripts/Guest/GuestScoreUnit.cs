using UnityEngine;
using System.Collections;

public class GuestScoreUnit
{
    private int[] m_scoreTable = new int[4];
    private int m_topUserId = -1;   // -1 = None

    public int topUserId
    {
        get { return m_topUserId; }
    }

    public GuestScoreUnit()
    {
        Reset();
        GuestScores.AddMax();
    }

    /// <summary>
    /// スコア等のリセット
    /// </summary>
    public void Reset()
    {
        m_scoreTable[0] = 0;
        m_scoreTable[1] = 0;
        m_scoreTable[2] = 0;
        m_scoreTable[3] = 0;
        m_topUserId = -1;
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="id">ユーザーID</param>
    /// <param name="s">加算スコア</param>
    /// <returns>スコアトップが更新されたか？</returns>
    public bool Add(UserID id, int s)
    {
        int idx = (int)id;
        m_scoreTable[idx] += s;
        if (m_topUserId == -1)
        {
            GuestScores.Add(id);
            m_topUserId = idx;
            return true;
        }
        else
        {
            // HACK 後から来たUserと一位タイの場合は前のUserが優先
            if (m_scoreTable[idx] > m_scoreTable[m_topUserId])
            {
                GuestScores.Add(id, (UserID)m_topUserId);
                m_topUserId = idx;
                return true;
            }
        }
        return false;
    }

}
