using System.Collections;
using UnityEngine;


public class Block : MonoBehaviour
{
    public SpriteRenderer characterRenderer;
    public Vector2 PointerPosition { get; set; }

    public Animator animator;
    public float delay = 0.2f;
    public bool perfectBlock;

    public bool IsBlocking {get; private set;}
    public Transform circleOrigin;
    public float radius;
    private lancelotController controller;
    private Health health;
    // private AgentMover agentMover;
    // private EnemyAI enemyAI;

    void Start(){
        controller = GetComponentInParent<lancelotController>();
        health = GetComponentInParent<Health>();
    }

    
    private void Update()
    {
        if (IsBlocking)
            return;
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

    }
    public void resetIsBlocking(){
        IsBlocking = false;
    }
    public void performBlock()
    {
        if(perfectBlock){
            return;
        }
        // agentMover.enabled = false;
        // enemyAI.enabled = false;
        IsBlocking = true;
        perfectBlock = true;
        health.enabled = false;
        StartCoroutine(DelayBlock());
    }

    public void perfectBlockAttack(){
        animator.SetTrigger("special");
        StartCoroutine(Dash());
        // BlockAttack?.Invoke();
    }

    private IEnumerator Dash(){
        yield return new WaitForSeconds(0.5f);
        controller.isDashing = true;
    }
    private IEnumerator DelayBlock()
    {
        yield return new WaitForSeconds(delay);
        perfectBlock = false;
        health.enabled = true;
    }

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
            performBlock();
        }
    }
}