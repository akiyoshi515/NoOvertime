using UnityEngine;
using System.Collections;

using GuestPopStrategyInternal;

public class GuestPopStrategy
{
    public enum StrategyType
    {
        Wait,
        Standard,
        // TODO
    }


    public static IGuestPopStrategy CreatePopStrategy(StrategyType type)
    {
        switch(type)
        {
            case StrategyType.Wait:
                return new GuestPopStrategy_Wait();
            case StrategyType.Standard:
                return new GuestPopStrategy_Standard();
        }

        // TODO
        return null;
    }

}
