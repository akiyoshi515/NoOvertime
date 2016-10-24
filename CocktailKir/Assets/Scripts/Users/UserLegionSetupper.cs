using UnityEngine;
using System.Collections;

public class UserLegionSetupper : MonoBehaviour
{
    // Target
    public GameObject m_targetLegion = null;

    // Basis Object
    public GameObject m_baseUserCtrl = null;
    public GameObject[] m_baseUserMesh = new GameObject[4];

    public GameObject m_baseUserCamera = null;

    // UserChar Param
    // CharAnimateCtrl
    public float m_moveSpeed = 360.0f;
    public float m_jumpPower = 12.0f;


    // Launcher Param

    // LauncherMagazine Param
    public float m_reloadTime = 2.0f;

    // UserCamera Param
    public float m_cameraRotateSpeed = 3.0f;
    public float m_cameraPivotLerpTime = 0.50f;

}

