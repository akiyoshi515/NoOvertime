
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

namespace XLoggerInternal
{
    public class XLoggerLogList
    {
        private readonly int m_capacity = 10;

        private System.Collections.Queue m_logQueue = null;

        public XLoggerLogList(int capacity)
        {
            m_capacity = capacity;
            m_logQueue = new System.Collections.Queue();
        }

        public IEnumerator GetEnumerator()
        {
            return m_logQueue.GetEnumerator();
        }

        public void Push(object msg)
        {
            m_logQueue.Enqueue(msg);
            for (; m_logQueue.Count >= m_capacity; )
            {
                m_logQueue.Dequeue();
            }
        }

        public void Clear()
        {
            m_logQueue.Clear();
        }

    }

}


}