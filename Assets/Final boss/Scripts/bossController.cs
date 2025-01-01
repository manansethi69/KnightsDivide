using UnityEngine;
using System.Collections;

public class bossController : MonoBehaviour
{
    [SerializeField] private GameObject bullet; // For the lightning attack
    [SerializeField] private Transform bulletSpawnPoint; // Spawn point for the bullet
    [SerializeField] private Sprite alternateSprite; // Second sprite for transformation
    private Sprite originalSprite; // Store the original sprite
    private int attackCounter;
    public float walkSpeed = 6f;
    public float walkDistance = 10f;
    public float meleeRange = 3f;
    public float detectionRange = 10f;

    private float walkedDistance = 0f;
    private bool movingRight = true;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Transform attackChild;
    private Transform lightningSpawn;
    private float lastAttackTime = -Mathf.Infinity;
    public float cooldownDuration = 2f;
    private bool isAttacking = false;
    private bool hasTransformed = false; // Flag for transformation
    private GameObject activeLightning;

    void Start()
    {
        attackCounter = 1;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite; // Save the original sprite
        animator = GetComponentInChildren<Animator>();
         // Find the player by tag
        attackChild = transform.Find("Melee"); // Find the "Attack" child
        lightningSpawn = transform.Find("LightningSpawn"); // Find the lightning spawn point
    }

    void Update()
    {
        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!hasTransformed)
            {
                TransformBoss(); // Trigger transformation animation
                hasTransformed = true; // Set the flag to prevent re-triggering
                return; // Skip the rest of the update for this frame
            }

            if (isAttacking)
            {
                return; // Skip movement and actions during an attack
            }

            if (player == null)
            {
                Walk();
                return;
            }

            FacePlayer();

            if (distanceToPlayer > meleeRange)
            {
                MoveTowardsPlayer();
            }
            else if (Time.time - lastAttackTime >= cooldownDuration)
            {
                AttackMelee();
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
        float movement = walkSpeed * Time.deltaTime;
        transform.Translate(movingRight ? Vector2.right * movement : Vector2.left * movement);

        walkedDistance += movement;

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
        FlipAttackChild();
    }

    void AttackMelee()
    {
        if (attackCounter == 1)
        {
            animator.SetTrigger("Attack1");
            attackCounter++;
            meleeRange = 6f;
            walkSpeed = 4f;
        }
        else if (attackCounter == 2)
        {
            animator.SetTrigger("Shockwave");
            attackCounter++;
            walkSpeed = 2f;
        }
        else if (attackCounter == 3)
        {
            ShootLightning();
            attackCounter = 1;
            meleeRange = 3f;
            walkSpeed = 6f;
        }

        Debug.Log("Attack counter: " + attackCounter);

        lastAttackTime = Time.time; // Update attack time
        StartCoroutine(EndAttackAfterDelay(1.5f)); // Set attack duration
    }

    public void ShootLightning()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is null! Cannot shoot.");
            return;
        }

        isAttacking = true; // Stop the boss from moving
        animator.SetTrigger("ShootLightning");
        activeLightning = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity); // Lightning object reference

        StartCoroutine(EndAttackAfterDelay(1.5f));
    }

    void FacePlayer()
    {
        if (player != null)
        {
            bool shouldFaceRight = player.position.x > transform.position.x;

            if (shouldFaceRight && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
                FlipAttackChild();
                FlipLightningSpawn();
            }
            else if (!shouldFaceRight && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
                FlipAttackChild();
                FlipLightningSpawn();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        float direction = player.position.x > transform.position.x ? 1f : -1f;
        transform.Translate(Vector2.right * direction * walkSpeed * Time.deltaTime);
        animator.SetTrigger("Run");
    }

    void Idle()
    {
        animator.SetTrigger("Idle");
    }

    IEnumerator EndAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        Idle();
    }

    void FlipAttackChild()
    {
        if (attackChild != null)
        {
            Vector3 localPosition = attackChild.localPosition;
            localPosition.x = -localPosition.x;
            attackChild.localPosition = localPosition;
        }
        else
        {
            Debug.LogError("No attackChild found!");
        }
    }

    void FlipLightningSpawn()
    {
        if (lightningSpawn != null)
        {
            Vector3 localPosition = lightningSpawn.localPosition;
            localPosition.x = -localPosition.x;
            lightningSpawn.localPosition = localPosition;
        }
    }
    

        void TransformBoss()
    {
        animator.SetTrigger("Transform"); // Play the transformation animation
        spriteRenderer.sprite = alternateSprite; 
        StartCoroutine(ScaleSpriteAfterDelay()); 

    }

    IEnumerator ScaleSpriteAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // Wait for 1 second
        Transform spriteChild = transform.Find("Sprite"); 
        if (spriteChild != null)
        {
            spriteChild.localScale = new Vector3(3f, 3f, 1f); 
    
        }
       
    }

    public void Die()
    {
        animator.SetTrigger("Death");
    }
}
