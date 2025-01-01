using UnityEngine;
using System.Collections;
public class rangedImpact : MonoBehaviour
{
    private int damage = 50; // Damage per attack
    private Collider2D weaponCollider;
    public float moveSpeed = 5f; // Movement speed towards the player
    private Vector2 targetPosition;

    void Awake()
    {
        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = true;
        
        // Find the Player by tag and get its position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Set target position to the center of the player object
            targetPosition = player.transform.position;
            StartCoroutine(MoveTowardsPlayer());
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ranged hit " + damage);

            Health health = collider.GetComponent<Health>();
            if (health != null)
            {
                Debug.Log(collider.name + " damage: " + damage);
                GameObject sender = transform.parent != null ? transform.parent.gameObject : gameObject;
                health.GetHit(damage, sender);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("No Health component found on " + collider.name);
            }
        }
    }

    public void EnableCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        weaponCollider.enabled = false;
    }

    private IEnumerator MoveTowardsPlayer()
    {
        // Move towards the target (center of player) until it reaches it
        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
