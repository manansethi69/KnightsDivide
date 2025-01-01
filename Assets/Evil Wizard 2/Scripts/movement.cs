using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 9f;
    [SerializeField] private float targetOffsetY = 0.0f;
    public float walkSpeed = 2f; 
    public float walkDistance = 10f; 
    public float meleeRange = 4f; 
    public float detectionRange = 6f; // Detection

    private float walkedDistance = 0f; 
    private bool movingRight = true; // Direction of movement
    private Transform player; // Reference to the player's position
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isAttacking = false; // Currently attacking

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform; 

        if (player == null)
        {
            Debug.LogError("No GameObject with tag 'Player' found in the scene!");
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            // If currently attacking, do nothing
            return;
        }

        if (player == null)
        {
            Walk(); // If no player is found, continue walking 
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= meleeRange)
        {
            // Make sure the boss faces the player before melee attack
            FacePlayer();
            AttackMelee();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            FacePlayer(); 
            AttackRanged();
        }
        else
        {
            Walk();
        }
    }

    void Walk()
    {
        // Move the boss
        float movement = walkSpeed * Time.deltaTime;
        transform.Translate(movingRight ? Vector2.right * movement : Vector2.left * movement);
        walkedDistance += movement;

        // Check if the boss should turn around
        if (walkedDistance >= walkDistance)
        {
            TurnAround();
        }

        animator.SetTrigger("Run");
    }

    void TurnAround()
    {
        walkedDistance = 0f;
        movingRight = !movingRight;
        spriteRenderer.flipX = !movingRight;
    }

    void AttackMelee()
    {
        isAttacking = true;
        animator.SetTrigger("Attack2");
        Debug.Log("Entered melee range");

        StartCoroutine(EndAttackAfterDelay(1.5f)); // Set attack duration
    }

    void AttackRanged()
    {
        isAttacking = true; 
        animator.SetTrigger("Attack1");

        Debug.Log("Entered firing range");

        StartCoroutine(EndAttackAfterDelay(1.0f)); // Set attack duration
    }

    
    void FacePlayer()
    {
        if (player != null)
        {
            // Flip sprite based on player position relative to the boss
            if (player.position.x > transform.position.x && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false; // Face right
            }
            else if (player.position.x < transform.position.x && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true; // Face left
            }
        }
    }

    void Idle()
    {
        animator.SetTrigger("Idle");
    }

    IEnumerator EndAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the attack animation to finish
        isAttacking = false; 
        Idle(); 
    }

    public void die() {
        animator.SetTrigger("Death");
    }
}
