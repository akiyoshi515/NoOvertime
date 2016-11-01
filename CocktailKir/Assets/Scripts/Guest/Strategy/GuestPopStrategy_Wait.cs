using UnityEngine;
using System.Collections;

namespace GuestPopStrategyInternal
{

    public class GuestPopStrategy_Wait : IGuestPopStrategy
    {
        public void UpdatePopStrategy(GuestPopDestinationCtrl ctrl, IGuestTypeStrategy typeStrategy, int[] values, float[] fvalues)
        {
            // Empty
        }

        public GuestPopStrategy.PopStrategyType ToStrategyType()
        {
            return GuestPopStrategy.PopStrategyType.Wait;
        }
    }

}