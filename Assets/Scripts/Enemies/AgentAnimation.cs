using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgentAnimations : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D m_rb;

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
        animator.SetTrigger("hit");
    }
    public void Die(){
        m_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("death");
    }
}