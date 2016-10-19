using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PhysicCollisionImpulseReflecter : MonoBehaviour
{
    public string[] m_targetTag = new string[1];

    void OnCollisionStay(Collision col)
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        foreach(string strTag in m_targetTag){
            if (col.gameObject.tag == strTag)
            {
                rb.AddForce(col.impulse, ForceMode.Impulse);
            }
        }
    }
}
