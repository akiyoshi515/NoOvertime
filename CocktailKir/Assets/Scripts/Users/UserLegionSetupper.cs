using UnityEngine;
using System.Collections;

public class UserLegionSetupper : MonoBehaviour
{
    // Target
    public GameObject m_targetLegion = null;

    // Basis Object
    public GameObject m_baseUserCtrl = null;
    public GameObject[] m_baseUserMesh = new GameObject[4];
    public GameObject[] m_baseUserUIUserNo = new GameObject[4];
    public GameObject[] m_baseUserUIMagazine = new GameObject[4];
    public GameObject[] m_baseUserUIMagazineRender = new GameObject[4];

    public GameObject m_baseUserCamera = null;

    // UserChar Param
    // CharAnimateCtrl
    public float m_moveSpeed = 360.0f;
    public float m_jumpPower = 12.0f;

    // Launcher Param
    public float m_pitchSpeed = 30.0f;
    public float m_minPitchAngle = 0.0f;
    public float m_maxPitchAngle = 60.0f;
    public float m_yawSpeed = 30.0f;
    public float m_shotPower = 15.0f;
    public float m_shot3WayAngle = 8.0f;
    public float m_knockbackTime = 0.250f;
    public float m_chargeShotTime = 1.0f;

    // LauncherMagazine Param
    public float m_reloadTime = 2.0f;

    // UserCamera Param
    public float m_cameraRotateSpeed = 3.0f;
    public float m_cameraPivotLerpTime = 0.50f;

}

