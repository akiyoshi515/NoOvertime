
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

    public class XLimitedNumber
    {
        public static void Add(ref int value, int addValue, int max)
        {
            value = (value + addValue > max ? max : value + addValue);
        }

        public static void Sub(ref int value, int subValue, int min)
        {
            value = (value - subValue < min ? min : value - subValue);
        }

        public static void Add(ref float value, float addValue, float max)
        {
            value = (value + addValue > max ? max : value + addValue);
        }

        public static void Sub(ref float value, float subValue, float min)
        {
            value = (value - subValue < min ? min : value - subValue);
        }

        public static bool HasValue(int now, int value)
        {
            return (now >= value);
        }

        public static bool HasValue(float now, float value)
        {
            return (now >= value);
        }

    }

}