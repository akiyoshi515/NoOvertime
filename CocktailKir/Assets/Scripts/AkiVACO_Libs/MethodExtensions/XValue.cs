
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

namespace XValue
{
    public static class IntExtensions
    {
        public static int MinLimited(this int n, int value)
        {
            return Mathf.Max(n, value);
        }

        public static int MaxLimited(this int n, int value)
        {
            return Mathf.Min(n, value);
        }

        public static int MinLimitedZero(this int n)
        {
            return n.MinLimited(0);
        }

        public static int MaxLimitedZero(this int n)
        {
            return n.MaxLimited(0);
        }

        public static int MinLimitedOne(this int n)
        {
            return n.MinLimited(1);
        }

        public static int MaxLimitedOne(this int n)
        {
            return n.MaxLimited(1);
        }
    }

    public static class FloatExtensions
    {
        public static float MinLimited(this float f, float value)
        {
            return Mathf.Max(f, value);
        }

        public static float MaxLimited(this float f, float value)
        {
            return Mathf.Min(f, value);
        }

        public static float MinLimitedZero(this float f)
        {
            return f.MinLimited(0.0f);
        }

        public static float MaxLimitedZero(this float f)
        {
            return f.MaxLimited(0.0f);
        }

        public static bool IsNaN(this float f)
        {
            return (float.IsNaN(f));
        }

        public static bool IsInfinity(this float f)
        {
            return (float.IsInfinity(f));
        }

        public static bool IsNegativeInfinity(this float f)
        {
            return (float.IsNegativeInfinity(f));
        }

        public static bool IsPositiveInfinity(this float f)
        {
            return (float.IsPositiveInfinity(f));
        }
    }
}

}