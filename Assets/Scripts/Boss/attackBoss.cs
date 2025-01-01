using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class attackBoss : MonoBehaviour
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
    public GameObject player;
    private Rigidbody2D m_rb;
    private bool isDashing = false;
    public UnityEvent OnBegin, OnDone;
    private EnemyAIBoss ai;
    public float dodgeChance = 0.75f;
    public Vector3 prevPos;
    private GameObject parent;
    public void Start(){
        m_rb = GetComponentInParent<Rigidbody2D>();
        ai = GetComponentInParent<EnemyAIBoss>();
        parent = transform.parent.gameObject;
    }
    public void ResetIsAttacking()
    {
        IsAttacking = false;
        attackingPLayer = false;
        isDashing = false;
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
    public void DashAttack(){
        if(isDashing){
            return;
        }
        animator.SetTrigger("dash");
        IsAttacking = true;
        isDashing = true;
        OnBegin?.Invoke();
        Vector3 direction = (transform.position - player.transform.position);
        
        float weight = 3;
        if(direction.x < 0){
            weight = weight * -1;
        }
        Vector3 pos = new Vector3(parent.transform.position.x + weight, parent.transform.position.y, parent.transform.position.z);
        prevPos = parent.transform.position;
        // m_rb.MovePosition(pos);
        m_rb.position = Vector3.Lerp(prevPos, pos, 4f);
        
        StartCoroutine(Reset());
        StartCoroutine(DelayAttack());
    }

    public void dashAttackHelper(){
        animator.SetTrigger("dashAttack");
        // m_rb.MovePosition(prevPos);
        m_rb.position = Vector3.Lerp(parent.transform.position, prevPos, 5f);
    }
    public void Attack()
    {
        
        if (attackBlocked)
            return;  
        if(m_rb.linearVelocity.x > 2){
            animator.SetTrigger("runAttack");
        }  
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack(first)")){
            animator.SetTrigger("attack_2");
        }
        else{
            animator.SetTrigger("attack_1");
        }
        
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }
    private IEnumerator Reset(){
        yield return new WaitForSeconds(2f);
        m_rb.linearVelocity = Vector2.zero;
        OnDone?.Invoke();
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
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack(first)")){
                    health.GetHit(25, transform.parent.gameObject);
                }
                else if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack(Second)")){
                    health.GetHit(35, transform.parent.gameObject);
                }
                else{
                    health.GetHit(50, transform.parent.gameObject);
                }
            }
        }
    }

}