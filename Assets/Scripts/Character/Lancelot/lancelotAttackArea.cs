using UnityEngine;
using System.Collections;

public class lancelotAttackArea : MonoBehaviour
{
    public Transform circleOrigin;
    public float radius;
    public int damage;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            
            Health health;
            if(health = collider.GetComponent<Health>())
            {
                Debug.Log(collider.name + "damage: " + damage);
                health.GetHit(damage, transform.parent.gameObject);
            }
        }
    }
}
