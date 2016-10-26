using UnityEngine;
using System.Collections;

namespace AkiVACO
{

public class SBBlankPrefab : MonoBehaviour
{
    void Awake()
    {
        XLogger.LogWarning("Instantiate BlankPrefab", gameObject);
    }

}

}
