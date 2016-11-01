using UnityEngine;
using System.Collections;

using GuestPopStrategyInternal;

public class GuestPopStrategy
{
    public enum PopStrategyType
    {
        Wait,
        Standard,
        // TODO
    }

    public static IGuestPopStrategy CreatePopStrategy(PopStrategyType type)
    {
        switch(type)
        {
            case PopStrategyType.Wait:
                return new GuestPopStrategy_Wait();
            case PopStrategyType.Standard:
                return new GuestPopStrategy_Standard();
        }

        // TODO
        return null;
    }

}
