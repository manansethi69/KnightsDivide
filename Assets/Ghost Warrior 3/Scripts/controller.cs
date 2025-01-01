using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class controller : MonoBehaviour
{
    private int attackCounter;
    public float walkSpeed = 2f; 
    public float walkDistance = 10f; 
    public float meleeRange = 4f; 
    public float detectionRange = 6f; 

    private float walkedDistance = 0f; 
    private bool movingRight = true; 
    private Transform player; 
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Transform attackChild;
    private float lastAttackTime = -Mathf.Infinity; 
    public float cooldownDuration = 2f; 
    private bool isAttacking = false;
    private Light2D light;

    void Start()
    {
        attackCounter = 1;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Find the player object
        attackChild = transform.Find("Attack"); // Find the "Attack" child
        light = GetComponentInChildren<Light2D>();
        if (player == null)
        {
            Debug.LogError("No GameObject with tag 'Player' found in the scene!");
        }
        if (attackChild == null)
        {
            Debug.LogError("No child GameObject named 'Attack' found!");
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            
            return;
        }

        if (player == null)
        {
            Walk(); 
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            FacePlayer(); // Ensure the boss faces the player
            if (distanceToPlayer > meleeRange)
            {
                MoveTowardsPlayer(); // Move toward the player
            }
            else if (Time.time - lastAttackTime >= cooldownDuration) 
            {
                AttackMelee(); // Attack if in range
            }
            else
            {
                Idle(); 
            }
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
    
        // Track distance walked
        walkedDistance += movement;
    
        // Ensure the sprite faces the movement direction
        if (movingRight && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
            FlipAttackChild();
        }
        else if (!movingRight && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
            FlipAttackChild();
        }
    
        // Check if the boss should turn around
        if (walkedDistance >= walkDistance)
        {
            TurnAround();
        }
    
        // Set the animator to "Run"
        animator.SetTrigger("Run");
    }



    void TurnAround()
    {
        // Reset distance tracking
        walkedDistance = 0f;

        // Flip direction
        movingRight = !movingRight;

        // Flip sprite
        spriteRenderer.flipX = !movingRight;

        // Flip the "Attack" child
        FlipAttackChild();
    }

    void AttackMelee()
    {
        isAttacking = true; // Start attack sequence
        light.intensity = 3;
        light.pointLightOuterRadius = 10;
        if (attackCounter == 1)
        {
            animator.SetTrigger("Attack1");
            
        }
        else if (attackCounter == 2)
        {
            animator.SetTrigger("Attack2");
        }
        else
        {
            animator.SetTrigger("Attack3");
        }

        if (attackCounter == 3)
        {
            attackCounter = 1;
        }
        else
        {
            attackCounter++;
        }

        Debug.Log("Attack counter: " + attackCounter);

        lastAttackTime = Time.time; // Update attack time
        StartCoroutine(EndAttackAfterDelay(1.5f)); // Set attack duration
    }

    void FacePlayer()
    {
        if (player != null)
        {
            bool shouldFaceRight = player.position.x > transform.position.x;

            // Flip sprite and child if necessary
            if (shouldFaceRight && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
                FlipAttackChild();
            }
            else if (!shouldFaceRight && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
                FlipAttackChild();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Determine direction to move
        float direction = player.position.x > transform.position.x ? 1f : -1f;

        // Move the boss
        transform.Translate(Vector2.right * direction * walkSpeed * Time.deltaTime);

        // Set the animator to "Run"
        animator.SetTrigger("Run");
    }

    void Idle()
    {
        animator.SetTrigger("Idle");
    }

    IEnumerator EndAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the attack animation to finish
        isAttacking = false; // End the attack sequence
        light.intensity = 2;
        light.pointLightOuterRadius = 5;
        Idle(); // Transition to idle state
    }

    void FlipAttackChild()
    {
        if (attackChild != null)
        {
            Vector3 localScale = attackChild.localScale;
            localScale.x = Mathf.Abs(localScale.x) * (spriteRenderer.flipX ? -1 : 1);
            attackChild.localScale = localScale;
        }
    }

    public void die()
    {
        animator.SetTrigger("Death");
    }
}
