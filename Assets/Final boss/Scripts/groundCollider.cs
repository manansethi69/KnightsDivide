using UnityEngine;

public class groundCollider : MonoBehaviour
{
    private int damage = 35; // Damage per attack
    private PolygonCollider2D weaponCollider;  // Assuming GroundAttack has a PolygonCollider2D

    public bool attackingPlayer;

    void Awake()
    {
        // Get the PolygonCollider2D component attached to this GameObject
        weaponCollider = GetComponent<PolygonCollider2D>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false; // Disable the collider initially
        }

        attackingPlayer = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit " + damage);

            Health health;
            if (health = collider.GetComponent<Health>())
            {
                Debug.Log(collider.name + " damage: " + damage);
                health.GetHit(damage, transform.parent.gameObject); // Assume the parent is the boss
            }
        }
    }

    // Enable and disable the collider on GroundAttack
    public void EnableCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;  // Enable only the GroundAttack collider
        }
        attackingPlayer = true;
    }

    public void DisableCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false; // Disable only the GroundAttack collider
        }
        attackingPlayer = false;
    }
}