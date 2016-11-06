using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

using AkiVACO;
using AkiVACO.XValue;

[RequireComponent(typeof(LineRenderer))]
public class LauncherCtrl : MonoBehaviour {

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
    private float m_shotPower = 15.0f;

    [SerializeField]
    private float m_shot3WayAngle = 8.0f;

    [SerializeField]
    private float m_knockbackTime = 0.250f;

    [SerializeField]
    private float m_chargeShotTime = 1.0f;

    [SerializeField]
    private float m_hitPointOffset = 0.50f;

    [SerializeField]
    private float m_lineRenderTimeSlice = 0.20f;

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
    private UserCharCtrl m_charCtrl = null;

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
        XLogger.LogValidObject(m_parent == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Parent"), gameObject);
        XLogger.LogValidObject(m_bullet == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Bullte"), gameObject);
        XLogger.LogValidObject(m_bulletBouquet == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("BullteBouquet"), gameObject);
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
        m_charCtrl = m_parent.GetComponent<UserCharCtrl>();
    }
	
	// Update is called once per frame
    void Update()
    {
        UpdateReloadBullet();
        UpdateShotBullet();

        Vector2 vec = Vector2.zero;
        if (m_charCtrl.isLauncherStance)
        {
            vec = m_input.RotateLauncher();
        }
        m_pitchAngle = (m_pitchAngle + vec.y * m_pitchSpeed * Time.deltaTime).MaxLimited(m_maxPitchAngle).MinLimited(m_minPitchAngle);
        this.transform.localRotation = Quaternion.AngleAxis(-m_pitchAngle, Vector3.right);

        m_parent.Rotate(Vector3.up, vec.x * m_yawSpeed * Time.deltaTime, Space.World);

#if DEBUG
        if (m_input.Dbg_IsUnlimitedBullet())
        {
            m_magazine.StartUnlimitedBullet(10.0f); // TODO
        }

        if (m_input.Dbg_IsShot3Way())
        {
            m_magazine.ReloadBonus3WayBullet(3); // TODO
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
            if (m_charCtrl.isJumping)
            {
                return;
            }

            if (!m_magazine.isMax)
            {
                if (m_input.IsReload())
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

        if (m_input.IsShot() && m_charCtrl.isLauncherStance)
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
                        LaunchBouquet();
                    }
                    else if (m_magazine.bulletNum >= m_costBullet)
                    {
                        LaunchBullet();
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
        if (m_magazine.UseBonus3WayBullet())
        {
            act.Invoke(Quaternion.AngleAxis(-m_shot3WayAngle, Vector3.up) * this.transform.forward);
            act.Invoke(Quaternion.AngleAxis(m_shot3WayAngle, Vector3.up) * this.transform.forward);
        }
        else
        {
            m_magazine.SubBullet(m_costBullet);
        }

        m_knockback += m_knockbackTime;
    }

    private void LaunchBouquet()
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
        if (m_magazine.UseBonus3WayBullet())
        {
            act.Invoke(Quaternion.AngleAxis(-m_shot3WayAngle, Vector3.up) * this.transform.forward);
            act.Invoke(Quaternion.AngleAxis(m_shot3WayAngle, Vector3.up) * this.transform.forward);
        }
        else
        {
            m_magazine.SubBullet(m_costChargeBullet);
        }
        m_knockback += m_knockbackTime;
    }
}
