using UnityEngine;
using System.Collections;

namespace GuestPopStrategyInternal
{
    public class GuestTypeStrategy_Random : IGuestTypeStrategy
    {
        public GuestType CalcType(GuestPopDestinationCtrl ctrl)
        {
            int maxPriority = 0;
            
            foreach(GuestPopDestinationCtrl.GuestParam param in ctrl.m_param)
            {
                if (param.IsCapacity())
                {
                    maxPriority += param.m_priority;
                }
            }

            if (maxPriority <= 0)
            {
                return GuestType.InvalidType;
            }

            int r = Random.Range(0, maxPriority);
            maxPriority = 0;
            for (int i = 0; i < GuestConstParam.SumGuestType; ++i )
            {
                if (ctrl.m_param[i].IsCapacity())
                {
                    maxPriority += ctrl.m_param[i].m_priority;
                    if (r <= maxPriority)
                    {
                        return (GuestType)i;
                    }
                }
            }

            AkiVACO.XLogger.LogWarning("Invalid operate PopStrategyType.");
            return GuestType.InvalidType;
        }
    }
}