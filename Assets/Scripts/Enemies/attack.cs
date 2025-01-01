using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class attack : MonoBehaviour
{
    public SpriteRenderer characterRenderer;
    public Vector2 PointerPosition { get; set; }

    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;

    public bool IsAttacking { get; private set; }
    public bool attackingPLayer;
    public Transform circleOrigin;
    public float radius;
    private Rigidbody2D rb2d;
    public int damage;

    void Start(){
        rb2d = GetComponentInParent<Rigidbody2D>();
    }
    public void ResetIsAttacking()
    {
        IsAttacking = false;
        attackingPLayer = false;
        rb2d.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }

    private void Update()
    {
        if (IsAttacking)
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

    public void Attack()
    {
        if (attackBlocked)
            return;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
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
            // Debug.Log(collider.name);
            Health health;
            Block block;
            if(health = collider.GetComponent<Health>())
            {
                attackingPLayer = true;
                if(block = collider.GetComponentInChildren<Block>()){
                    if(block.perfectBlock){
                        block.perfectBlockAttack();
                        return;
                    }
                    if(block.IsBlocking){
                        health.GetHit(10, transform.parent.gameObject);
                        return;
                    }
                }
                health.GetHit(damage, transform.parent.gameObject);
            }
        }
    }
}