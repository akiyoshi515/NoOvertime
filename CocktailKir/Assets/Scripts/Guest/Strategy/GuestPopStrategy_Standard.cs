using UnityEngine;
using System.Collections;

namespace GuestPopStrategyInternal
{
    public class GuestPopStrategy_Standard : IGuestPopStrategy
    {
        private float m_remainTime = 0.0f;

        public void UpdatePopStrategy(GuestPopDestinationCtrl ctrl, IGuestTypeStrategy typeStrategy, int[] values, float[] fvalues)
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
                    int r = Random.Range(0, ctrl.baseSumCost+1);
                    int sum = 0;
                    foreach (GuestPopPointerCtrl unit in ctrl.pointTable)
                    {
                        if (unit.m_priority != 0)
                        {
                            sum += unit.m_priority;
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

                ctrl.SendPopGuest(typeStrategy.CalcType(ctrl), startPoint, ctrl.GetDestination(), endPoint);
            }
        }

        public GuestPopStrategy.PopStrategyType ToStrategyType()
        {
            return GuestPopStrategy.PopStrategyType.Standard;
        }
    }

}