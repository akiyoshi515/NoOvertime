using UnityEngine;
using System.Collections;

namespace GuestPopStrategyInternal
{

    public class GuestPopStrategy_Wait : IGuestPopStrategy
    {
        public void UpdatePopStrategy(GuestPopDestinationCtrl ctrl, int[] values, float[] fvalues)
        {
            // Empty
        }

        public GuestPopStrategy.StrategyType ToStrategyType()
        {
            return GuestPopStrategy.StrategyType.Wait;
        }
    }

}