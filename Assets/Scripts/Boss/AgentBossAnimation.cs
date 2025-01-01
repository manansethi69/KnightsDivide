using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgentBossAnimations : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D m_rb;
    private int hitNum = 0;
    public bool hitProtection;
    public void Start(){
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void RotateToPointer(Vector2 lookDirection)
    {
        Vector3 scale = transform.localScale;
        if (lookDirection.x > 0)
        {
            scale.x = 1;
        }
        else if (lookDirection.x < 0)
        {
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    public void PlayAnimation()
    {
        animator.SetFloat("move", Mathf.Abs(m_rb.linearVelocity.x));
        
    }

    public void Hit(){
        if(hitNum >= 3 && !hitProtection){
            hitNum = 0;
            hitProtection = true;
        }
        if(!hitProtection){
            animator.SetTrigger("hit");
            hitNum++;
        }
    }
    public void Die(){
        animator.SetTrigger("death");
    }

    public void ResetProtection(){
        hitProtection = false;
    }
}