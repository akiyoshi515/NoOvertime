using UnityEngine;
using System.Collections;

public abstract class BHEventOneShotBase : MonoBehaviour
{
    protected abstract void OnHitEvent(GameObject obj);

    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "Bullet") || (col.gameObject.tag == "BulletTrigger"))
        {
            OnHitEvent(col.gameObject);
            this.enabled = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Bullet") || (col.gameObject.tag == "BulletTrigger"))
        {
            OnHitEvent(col.gameObject);
            this.enabled = false;
        }
    }

    public void Restart()
    {
        this.enabled = true;
    }

}
