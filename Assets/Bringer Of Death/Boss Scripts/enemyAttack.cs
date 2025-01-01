using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class enemyAttack : MonoBehaviour
{
    public float attackRange = 2f; // Distance within which the enemy will attack
    private Transform player;
    private Animator animator;
    [HideInInspector] public bool isAttacking = false; // Public flag for the patrol script to read
    private float attackCooldown = 0.5f; // Time between attacks
    private float lastAttackTime = -2f; // Tracks the last time the enemy attacked

    private bossWeapon weapon;
    public UnityEvent performAttack;

    void Start()
    {
        // Find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        weapon = GetComponentInChildren<bossWeapon>();
        animator = GetComponentInChildren<Animator>(); // Get the Animator component
    }

    void Update()
    {
        // Check if the player exists and is within attack range
        if (player != null && !isAttacking)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check if the player is within range and cooldown has expired
            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true; // Lock movement and actions
        lastAttackTime = Time.time; 
        performAttack?.Invoke();
        yield return new WaitForSeconds(1.5f);
        isAttacking = false; // Unlock movement and actions
        animator.SetTrigger("Idle");
        
    }

    public void Die() {
        
        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        Destroy(gameObject, 1.5f); 
    }


}