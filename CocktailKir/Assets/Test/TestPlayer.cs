using UnityEngine;
using System.Collections;

using AkiVACO;

public class TestPlayer : MonoBehaviour {

    [SerializeField]
    private float m_moveSpeed = 5.0f;

    private CharacterController m_ctrl;

    void Awake()
    {
        m_ctrl = this.GetComponent<CharacterController>();
        XLogger.LogValidObject(m_ctrl == null, LibConstants.ErrorMsg.GetMsgNotBoundComponent("CharacterController"), gameObject);


    }

	// Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
    void Update()
    {
        Vector3 vecMove = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            vecMove.z = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vecMove.z = -1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vecMove.x = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vecMove.x = 1.0f;
        }
        vecMove.y = Physics.gravity.y;

        vecMove = vecMove.normalized * (m_moveSpeed * Time.deltaTime);
        vecMove = this.transform.TransformDirection(vecMove);
        m_ctrl.Move(vecMove);
        
    }
}
