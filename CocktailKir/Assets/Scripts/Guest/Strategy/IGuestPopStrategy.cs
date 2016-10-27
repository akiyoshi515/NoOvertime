using UnityEngine;
using System.Collections;

public interface IGuestPopStrategy
{
    void UpdatePopStrategy(GuestPopDestinationCtrl ctrl, int[] values, float[] fvalues);

    GuestPopStrategy.StrategyType ToStrategyType();
}
