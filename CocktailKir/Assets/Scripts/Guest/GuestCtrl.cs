﻿using UnityEngine;
using System.Collections;

using AkiVACO;

public class GuestCtrl : MonoBehaviour
{
    private GuestScoreUnit m_unit = new GuestScoreUnit();
    public GuestScoreUnit unit
    {
        get { return m_unit; }
    }

    void Awake()
    {
        m_unit.Reset();
    }

    // Use this for initialization
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ballet")
        {
            col.gameObject.GetComponent<TestBalletCtrl>().SendHit();
        }
        else if (col.tag == "BalletTrigger")
        {
            AkiVACO.XLogger.Log("Hit!");
            BalletTrigger tr = col.GetComponent<BalletTrigger>();
            if (m_unit.AddCharm(tr.userID, tr.charm))
            {
                this.transform.GetChild(0).GetComponent<MeshMaterialCtrl>().SetMaterial(m_unit.topUserId + 1);  // -1 -> 0
            }
        }
    }
}
