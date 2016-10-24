using UnityEngine;
using System.Collections;

public class GuestScoreUnit
{
    private int[] m_charmTable = new int[4];
    public int[] charmTable
    {
        get { return m_charmTable; }
    }

    private int m_topUserId = -1;   // -1 = None
    public int topUserId
    {
        get { return m_topUserId; }
    }

    public void Initialize()
    {
        Reset();
        GuestScores.AddMax();
    }

    /// <summary>
    /// スコア等のリセット
    /// </summary>
    public void Reset()
    {
        m_charmTable[0] = 0;
        m_charmTable[1] = 0;
        m_charmTable[2] = 0;
        m_charmTable[3] = 0;
        m_topUserId = -1;
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="id">ユーザーID</param>
    /// <param name="s">加算スコア</param>
    /// <returns>スコアトップが更新されたか？</returns>
    public bool AddCharm(UserID id, int s)
    {
        int idx = (int)id;
        m_charmTable[idx] += s;
        if (m_topUserId == -1)
        {
            GuestScores.Add(id);
            m_topUserId = idx;

            // +Bonus
            m_charmTable[idx] += GuestScores.firstHitCharmBonus;
            return true;
        }
        else
        {
            // 上書きしたユーザーが元々一位なら何もしない
            // 後から来たUserと一位タイの場合は後のUserが優先
            if (m_topUserId == idx)
            {
                return true;
            }
            else if (m_charmTable[idx] >= m_charmTable[m_topUserId])
            {
                GuestScores.Add(id, (UserID)m_topUserId);
                m_topUserId = idx;
                return true;
            }
        }
        return false;
    }

}
