using UnityEngine;
using System.Collections;

namespace GuestPopStrategyInternal
{
    public class GuestPopStrategy_Standard : IGuestPopStrategy
    {
        private float m_remainTime = 0.0f;

        public void UpdatePopStrategy(GuestPopDestinationCtrl ctrl, int[] values, float[] fvalues)
        {
            m_remainTime -= Time.deltaTime;
            if (m_remainTime > 0.0f)
            {
                return;
            }

            m_remainTime = fvalues[0];

            int count = values[0];

            for (int i = 0; i < count; ++i)
            {
                GuestPopPointerCtrl startPoint = null;
                GuestPopPointerCtrl endPoint = null;

                {
                    int r = Random.Range(0, ctrl.baseSumCost);
                    int sum = 0;
                    foreach (GuestPopPointerCtrl unit in ctrl.pointTable)
                    {
                        if (unit.m_cost != 0)
                        {
                            sum += unit.m_cost;
                            if (r <= sum)
                            {
                                startPoint = unit;
                                break;
                            }
                        }
                    }
                }
                {
                    int r = Random.Range(0, ctrl.pointTable.Length);
                    endPoint = ctrl.pointTable[r];
                }

                // TODO GuestType
                startPoint.SendPopGuest(GuestType.Standard, ctrl.GetDestination(), endPoint.transform);
            }
        }

        public GuestPopStrategy.StrategyType ToStrategyType()
        {
            return GuestPopStrategy.StrategyType.Standard;
        }
    }

}