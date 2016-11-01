using UnityEngine;
using System.Collections;

public interface IGuestPopStrategy
{
    void UpdatePopStrategy(GuestPopDestinationCtrl ctrl, IGuestTypeStrategy typeStrategy, int[] values, float[] fvalues);

    GuestPopStrategy.PopStrategyType ToStrategyType();
}
