using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

using AkiVACO;
using AkiVACO.XValue;

[RequireComponent(typeof(LineRenderer))]
public class TestLauncherCtrl : MonoBehaviour {

    [SerializeField]
    private Transform m_parent = null;

    [SerializeField]
    private Vector3 m_launchPoint = Vector3.zero;

    [SerializeField]
    private float m_pitchSpeed = 30.0f;

    [SerializeField]
    private float m_minPitchAngle = 0.0f;

    [SerializeField]
    private float m_maxPitchAngle = 60.0f;

    [SerializeField]
    private float m_yawSpeed = 30.0f;

    [SerializeField]
    private float m_shotPower = 10.0f;

    [SerializeField]
    private float m_shot3WayAngle = 8.0f;

    [SerializeField]
    private float m_knockbackTime = 0.250f;

    [SerializeField]
    private float m_chargeShotTime = 1.0f;

    [SerializeField]
    private float m_hitPointOffset = 1.0f;

    [SerializeField]
    private float m_lineRenderTimeSlice = 0.250f;

    [SerializeField]
    private float m_lineWidth = 0.250f;

    [SerializeField]
    private GameObject m_bullet = null;

    [SerializeField]
    private GameObject m_bulletBouquet = null;

    [SerializeField]
    private GameObject m_hitPoint = null;

    [SerializeField]
    private GameObject m_efMaxCharge = null;

    [SerializeField]
    private GameObject m_efReload = null;

    private IXVInput m_input = null;

    private LauncherMagazine m_magazine = null;

    private LineRenderer m_lineRenderer = null;
    private IEffect m_csEfMaxCharge = null;
    private IEffect m_csEfReload = null;

    private float m_pitchAngle = 0.0f;
    private float m_knockback = 0.0f;
    private float m_chargeTime = 0.0f;

    private int m_costBullet = 1;
    private int m_costChargeBullet = 1;

    void Awake()
    {
        XLogger.LogError("Illegal Awake! TestCS", gameObject);

        XLogger.LogValidObject(m_parent == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Parent"), gameObject);
        XLogger.LogValidObject(m_bullet == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Ballte"), gameObject);
        XLogger.LogValidObject(m_bulletBouquet == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("BallteBouquet"), gameObject);
        XLogger.LogValidObject(m_hitPoint == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("HitPoint"), gameObject);

        XLogger.LogValidObject(m_efReload == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Effect Reload"), gameObject);
        XLogger.LogValidObject(m_efMaxCharge == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Effect MaxCharge"), gameObject);

        m_magazine = this.GetComponent<LauncherMagazine>();
        m_lineRenderer = this.GetComponent<LineRenderer>();

        GameObject efObj = XFunctions.InstanceChild(m_efMaxCharge, Vector3.zero, Quaternion.identity, this.gameObject);
        GameObject efReloadObj = XFunctions.InstanceChild(m_efReload, Vector3.zero, Quaternion.identity, this.gameObject);

        m_csEfMaxCharge = efObj.GetComponent<IEffect>();
        m_csEfReload = efReloadObj.GetComponent<IEffect>();
    }

	// Use this for initialization
    void Start()
    {
        m_lineRenderer.SetWidth(m_lineWidth, m_lineWidth);

        m_costBullet = this.m_bullet.GetComponent<BulletCtrl>().cost;
        m_costChargeBullet = this.m_bulletBouquet.GetComponent<BulletCtrl>().cost;

        m_pitchAngle = m_minPitchAngle;
        this.transform.Rotate(Vector3.right, m_pitchAngle, Space.Self);

        m_input = m_parent.GetComponent<UserData>().input;
    }
	
	// Update is called once per frame
    void Update()
    {
        UpdateReloadBullet();
        UpdateShotBullet();

        Vector2 vec = m_input.RotateLauncher();
        m_pitchAngle = (m_pitchAngle + vec.y * m_pitchSpeed * Time.deltaTime).MaxLimited(m_maxPitchAngle).MinLimited(m_minPitchAngle);
        this.transform.localRotation = Quaternion.AngleAxis(-m_pitchAngle, Vector3.right);

        m_parent.Rotate(Vector3.up, vec.x * m_yawSpeed * Time.deltaTime, Space.World);

#if DEBUG
        if (m_input.Dbg_IsUnlimitedBullet())
        {
            m_magazine.StartUnlimitedBullet(10.0f); // TODO
        }

        if (m_input.Dbg_IsReloadBonusCharm())
        {
            m_magazine.ReloadBonusCharmBullet(6); // TODO
        }
#endif

    }

    void LateUpdate()
    {
        Queue<Vector3> que = new Queue<Vector3>();

        ParabolaLines.Draw(
            this.transform, m_shotPower, m_bullet.GetComponent<Rigidbody>().mass,
            m_lineRenderTimeSlice, m_hitPointOffset,
            m_hitPoint.transform,
            (idx, pos) =>
            {
                que.Enqueue(pos);
            });
        Vector3[] vec = que.ToArray();
        m_lineRenderer.SetVertexCount(vec.Length);
        m_lineRenderer.SetPositions(vec);
    }

    void UpdateReloadBullet()
    {
        if (m_magazine.isReloading)
        {
            // Empty
        }
        else
        {
            if (!m_magazine.isMax)
            {
                if ((m_input.IsLauncherStance()) && (m_input.IsReload()))
                {
                    StartReload();
                }
            }
        }
    }

    void UpdateShotBullet()
    {
        if (m_knockback > 0.0f)
        {
            m_knockback -= Time.deltaTime;
            return;
        }

        if (m_magazine.isReloading)
        {
            return;
        }

        if (m_input.IsShot())
        {
            m_chargeTime += Time.deltaTime;
        }
        else
        {
            if (m_chargeTime > 0.0f)
            {
                if (!m_magazine.isReloading)
                {
                    if ((m_chargeTime >= m_chargeShotTime) && (m_magazine.bulletNum >= m_costChargeBullet))
                    {
                        bool isShot3WayBullet = m_input.Dbg_IsShot3Way();
                        if (isShot3WayBullet)
                        {
                            Launch3WayBouquet();
                        }
                        else
                        {
                            LaunchBouquet();
                        }
                    }
                    else if (m_magazine.bulletNum >= m_costBullet)
                    {
                        bool isShot3WayBullet = m_input.Dbg_IsShot3Way();
                        if (isShot3WayBullet)
                        {
                            Launch3WayBullet();
                        }
                        else
                        {
                            LaunchBullet();
                        }
                    }
                    else
                    {
                        StartReload();
                    }
                }
                m_chargeTime = 0.0f;
            }
        }

        if ((m_chargeTime >= m_chargeShotTime) && (m_magazine.bulletNum >= m_costChargeBullet))
        {
            m_csEfMaxCharge.WakeupEffect();
        }
        else
        {
            m_csEfMaxCharge.SleepEffect();
        }
    }

    private void StartReload()
    {
        m_csEfReload.WakeupEffect();
        m_magazine.StartReload(() => { m_csEfReload.SleepEffect(); });
    }

    private void LaunchBullet()
    {
        Quaternion rot = this.gameObject.transform.rotation;
        Vector3 pos = this.transform.position + (rot * m_launchPoint);

        GameObject obj = XFunctions.Instance(m_bullet, pos, rot);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * m_shotPower, ForceMode.Impulse);
        BulletCtrl ctrl = obj.GetComponent<BulletCtrl>();
        ctrl.SetUserID(m_parent.GetComponent<UserData>().userID);
        ctrl.AddBonusCharm(m_magazine.UseBonusCharmBullet());

        m_magazine.SubBullet(m_costBullet);

        m_knockback += m_knockbackTime;
    }

    private void LaunchBouquet()
    {
        Quaternion rot = this.gameObject.transform.rotation;
        Vector3 pos = this.transform.position + (rot * m_launchPoint);

        GameObject obj = XFunctions.Instance(m_bulletBouquet, pos, rot);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * m_shotPower, ForceMode.Impulse);
        BulletCtrl ctrl = obj.GetComponent<BulletCtrl>();
        ctrl.SetUserID(m_parent.GetComponent<UserData>().userID);
        ctrl.AddBonusCharm(m_magazine.UseBonusCharmBullet());

        m_magazine.SubBullet(m_costChargeBullet);

        m_knockback += m_knockbackTime;
    }

    private void Launch3WayBullet()
    {
        Quaternion rot = this.gameObject.transform.rotation;
        Vector3 pos = this.transform.position + (rot * m_launchPoint);

        int bonusCharm = m_magazine.UseBonusCharmBullet();

        UnityAction<Vector3> act = (vec) =>
        {
            GameObject obj = XFunctions.Instance(m_bullet, pos, rot);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(vec * m_shotPower, ForceMode.Impulse);
            BulletCtrl ctrl = obj.GetComponent<BulletCtrl>();
            ctrl.SetUserID(m_parent.GetComponent<UserData>().userID);
            ctrl.AddBonusCharm(bonusCharm);
        };

        act.Invoke(this.transform.forward);
        act.Invoke(Quaternion.AngleAxis(-m_shot3WayAngle, Vector3.up) * this.transform.forward);
        act.Invoke(Quaternion.AngleAxis(m_shot3WayAngle, Vector3.up) * this.transform.forward);

        // TODO
        m_magazine.SubBullet(m_costBullet);

        m_knockback += m_knockbackTime;
    }

    private void Launch3WayBouquet()
    {
        Quaternion rot = this.gameObject.transform.rotation;
        Vector3 pos = this.transform.position + (rot * m_launchPoint);

        int bonusCharm = m_magazine.UseBonusCharmBullet();

        UnityAction<Vector3> act = (vec) => 
        {
            GameObject obj = XFunctions.Instance(m_bulletBouquet, pos, rot);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(vec * m_shotPower, ForceMode.Impulse);
            BulletCtrl ctrl = obj.GetComponent<BulletCtrl>();
            ctrl.SetUserID(m_parent.GetComponent<UserData>().userID);
            ctrl.AddBonusCharm(bonusCharm);
        };

        act.Invoke(this.transform.forward);
        act.Invoke(Quaternion.AngleAxis(-m_shot3WayAngle, Vector3.up) * this.transform.forward);
        act.Invoke(Quaternion.AngleAxis(m_shot3WayAngle, Vector3.up) * this.transform.forward);

        // TODO
        m_magazine.SubBullet(m_costChargeBullet);

        m_knockback += m_knockbackTime;
    }
}
