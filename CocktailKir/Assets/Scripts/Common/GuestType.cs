using UnityEngine;
using System.Collections;

public enum GuestType 
{
    InvalidType = -1,

    Standard = 0,
    Gentle = 1,
    Impatient = 2,
    StayBehind = 3,
}

public static class GuestConstParam
{
    public const int SumGuestType = 4;
}