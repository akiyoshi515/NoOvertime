using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AkiVACO;

[RequireComponent(typeof(LineRenderer))]
public class TestLauncherCtrl : MonoBehaviour {

    [SerializeField]
    private Transform m_parent = null;

    [SerializeField]
    private Vector3 m_launchPoint = Vector3.zero;

    [SerializeField]
    private float m_pitchSpeed = 30.0f;
    
    [SerializeField]
    private float m_yawSpeed = 30.0f;

    [SerializeField]
    private float m_shotPower = 10.0f;

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
    private GameObject m_balletEmpty = null;

    [SerializeField]
    private GameObject m_ballet = null;

    [SerializeField]
    private GameObject m_balletBouquet = null;

    [SerializeField]
    private GameObject m_hitPoint = null;

    [SerializeField]
    private GameObject m_efMaxCharge = null;

    [SerializeField]
    private GameObject m_efReload = null;

    private TestMagazine m_magazine = null;

    private LineRenderer m_lineRenderer = null;
    private IEffect m_csEfMaxCharge = null;
    private IEffect m_csEfReload = null;

    private float m_knockback = 0.0f;
    private float m_chargeTime = 0.0f;

    private int m_costBallet = 1;
    private int m_costChargeBallet = 1;

    void Awake()
    {
        XLogger.LogValidObject(m_parent == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Parent"), gameObject);
        XLogger.LogValidObject(m_balletEmpty == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("BallteEmpty"), gameObject);
        XLogger.LogValidObject(m_ballet == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Ballte"), gameObject);
        XLogger.LogValidObject(m_balletBouquet == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("BallteBouquet"), gameObject);
        XLogger.LogValidObject(m_hitPoint == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("HitPoint"), gameObject);

        XLogger.LogValidObject(m_efReload == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Effect Reload"), gameObject);
        XLogger.LogValidObject(m_efMaxCharge == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Effect MaxCharge"), gameObject);

        m_magazine = this.GetComponent<TestMagazine>();
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

        m_costBallet = this.m_ballet.GetComponent<TestBalletCtrl>().cost;
        m_costChargeBallet = this.m_balletBouquet.GetComponent<TestBalletCtrl>().cost;
    }
	
	// Update is called once per frame
    void Update()
    {
        IXVInput input = XVInput.GetInterface(UserID.User1);

        if (input.IsReload())
        {
            if (m_magazine.isMax)
            {
                m_csEfReload.SleepEffect();
                m_magazine.StopReload();
            }
            else
            {
                m_csEfReload.WakeupEffect();
                m_magazine.StartReload();
            }
        }
        else
        {
            m_csEfReload.SleepEffect();
            m_magazine.StopReload();
        }

        UpdateShotBallet(input.IsShot());

        Vector2 vec = input.RotateLauncher();

        this.transform.Rotate(Vector3.right, -vec.y * m_pitchSpeed * Time.deltaTime, Space.Self);
        m_parent.Rotate(Vector3.up, vec.x * m_yawSpeed * Time.deltaTime, Space.World);
    }

    void LateUpdate()
    {
        Queue<Vector3> que = new Queue<Vector3>();

        TestBalletLines.Draw(
            this.transform, m_shotPower, m_ballet.GetComponent<Rigidbody>().mass,
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

    void UpdateShotBallet(bool isShot)
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

        if (isShot)
        {
            m_chargeTime += Time.deltaTime;
        }
        else
        {
            if (m_chargeTime > 0.0f)
            {
                if (!m_magazine.isReloading)
                {
                    if ((m_chargeTime >= m_chargeShotTime) && (m_magazine.balletNum >= m_costChargeBallet))
                    {
                        LaunchBouquet();
                    }
                    else if (m_magazine.balletNum >= m_costBallet)
                    {
                        LaunchBallet();
                    }
                    else
                    {
                        LaunchEmptyBallet();
                    }
                }
                m_chargeTime = 0.0f;
            }
        }

        if (m_chargeTime >= m_chargeShotTime)
        {
            m_csEfMaxCharge.WakeupEffect();
        }
        else
        {
            m_csEfMaxCharge.SleepEffect();
        }
    }

    void LaunchBallet()
    {
        Quaternion rot = this.gameObject.transform.rotation;
        Vector3 pos = this.transform.position + (rot * m_launchPoint);

        GameObject obj = XFunctions.Instance(m_ballet, pos, rot);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * m_shotPower, ForceMode.Impulse);

        m_magazine.SubBallet(m_costBallet);

        m_knockback += m_knockbackTime;
    }

    void LaunchEmptyBallet()
    {
        Quaternion rot = this.gameObject.transform.rotation;
        Vector3 pos = this.transform.position + (rot * m_launchPoint);

        XFunctions.Instance(m_balletEmpty, pos, rot);
//        Rigidbody rb = obj.GetComponent<Rigidbody>();
//        rb.AddForce(this.transform.forward * m_shotPower, ForceMode.Impulse);

        m_knockback += m_knockbackTime;
    }

    void LaunchBouquet()
    {
        Quaternion rot = this.gameObject.transform.rotation;
        Vector3 pos = this.transform.position + (rot * m_launchPoint);

        GameObject obj = XFunctions.Instance(m_balletBouquet, pos, rot);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * m_shotPower, ForceMode.Impulse);

        m_magazine.SubBallet(m_costChargeBallet);

        m_knockback += m_knockbackTime;
    }

}
