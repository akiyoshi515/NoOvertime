using UnityEngine;
using System.Collections;

public interface IGuestTypeStrategy
{
    GuestType CalcType(GuestPopDestinationCtrl ctrl);
}
