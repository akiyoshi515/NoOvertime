﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharBaseCtrl : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 5.0f;

    [SerializeField]
    private float m_turnSpeedMoving = 360.0f;
    [SerializeField]
    private float m_turnSpeedStationary = 180.0f;
    [SerializeField]
    private float m_jumpPower = 12.0f;
    
    [SerializeField]
    private float m_gravityMult = 2.0f;
    [SerializeField]
    private float m_groundCheckDistance = 0.1f;

    public float moveSpeed
    {
        get { return m_moveSpeed; }
    }

    private Rigidbody m_rigidbody = null;

    private Vector3 m_groundNormal;
    private float m_oriGroundCheckDistance;
    private float m_turnAmount;
    private float m_forwardAmount;
    private bool m_isGrounded;

    void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();   
        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        m_oriGroundCheckDistance = m_groundCheckDistance;
    }

    public void Move(Vector3 move, bool jump, bool isWalk)
    {
        if (move.magnitude > 1.0f)
        {
            move.Normalize();
        }

        Vector3 vecMove = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        vecMove = Vector3.ProjectOnPlane(vecMove, m_groundNormal);
        m_turnAmount = Mathf.Atan2(vecMove.x, vecMove.z);
        m_forwardAmount = vecMove.z;

        UpdateExTurnRotation();

        Vector3 v = move * (m_moveSpeed * Time.deltaTime);
        v.y = m_rigidbody.velocity.y;
        m_rigidbody.velocity = v;

        if (m_isGrounded)
        {
            UpdateGroundedMovement(vecMove, jump);
        }
        else
        {
            UpdateAirMovement();
        }
    }

    void UpdateAirMovement()
    {
        Vector3 gravityForce = (Physics.gravity * m_gravityMult) - Physics.gravity;
        m_rigidbody.AddForce(gravityForce);

        m_groundCheckDistance = m_rigidbody.velocity.y < 0.0f ? m_oriGroundCheckDistance : 0.01f;
    }

    void UpdateGroundedMovement(Vector3 vecMove, bool jump)
    {
        if (jump)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, m_jumpPower, m_rigidbody.velocity.z);
            m_isGrounded = false;
            m_groundCheckDistance = 0.1f;
        }
    }

    void UpdateExTurnRotation()
    {
        float turnSpeed = Mathf.Lerp(m_turnSpeedStationary, m_turnSpeedMoving, m_forwardAmount);
        transform.Rotate(0.0f, m_turnAmount * turnSpeed * Time.deltaTime, 0.0f);
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(
            this.transform.position + (Vector3.up * 0.1f), 
            Vector3.down, out hitInfo, m_groundCheckDistance))
        {
            m_groundNormal = hitInfo.normal;
            m_isGrounded = true;
        }
        else
        {
            m_isGrounded = false;
            m_groundNormal = Vector3.up;
        }
    }
}
