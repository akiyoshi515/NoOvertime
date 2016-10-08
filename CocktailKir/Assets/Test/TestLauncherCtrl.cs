using UnityEngine;
using System.Collections;

using AkiVACO;

public class TestLauncherCtrl : MonoBehaviour {

    [SerializeField]
    private Vector3 m_launchPoint = Vector3.zero;

    [SerializeField]
    private float m_pitchSpeed = 30.0f;
    
    [SerializeField]
    private float m_yawSpeed = 30.0f;

    [SerializeField]
    private float m_shotPower = 10.0f;

    [SerializeField]
    private float m_hitPointOffset = 1.0f;

    [SerializeField]
    private float m_lineRenderTimeSlice = 0.250f;

    [SerializeField]
    private GameObject m_ballet = null;

    [SerializeField]
    private GameObject m_hitPoint = null;

    void Awake()
    {
        XLogger.LogValidObject(m_ballet == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("Ballte"), gameObject);
        XLogger.LogValidObject(m_hitPoint == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("HitPoint"), gameObject);

    }

	// Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBallet();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Rotate(Vector3.right, -m_pitchSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Rotate(Vector3.right, m_pitchSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up, -m_yawSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.up, m_yawSpeed * Time.deltaTime, Space.World);
        }

    }

    void LateUpdate()
    {
        Vector3 vec = TestBalletLines.Draw(
            this.transform, m_shotPower, m_ballet.GetComponent<Rigidbody>().mass,
            m_lineRenderTimeSlice, m_hitPointOffset);

        m_hitPoint.transform.position = vec;
    }

    void LaunchBallet()
    {
        Quaternion rot = this.gameObject.transform.localRotation;
        Vector3 pos = this.transform.position + (rot * m_launchPoint);

        GameObject obj = XFunctions.Instance(m_ballet, pos, rot);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * m_shotPower, ForceMode.Impulse);
    }

}
