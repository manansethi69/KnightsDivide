using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIBoss : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack, OnDodge;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float chaseDistanceThreshold =3, attackDistanceThreshold = 0.8f;
    [SerializeField]
    private float attackDelay = 2;
    private float dodge;
    public bool attacked = false;
    private float passedTime = 1;
    private AgentBossAnimations bossAnimations;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        bossAnimations = GetComponent<AgentBossAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            return;
        }

        float distance = Vector2.Distance(player.transform.position, transform.position);
        if(distance < chaseDistanceThreshold){
            OnPointerInput?.Invoke(player.transform.position);
            if(distance <= attackDistanceThreshold){
                OnMovementInput?.Invoke(Vector2.zero);
                dodge = UnityEngine.Random.Range(0f, 1f);

                //attack
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack(first)")){
                    passedTime = 0;
                    OnAttack?.Invoke();
                }

                if(passedTime >= attackDelay && !animator.GetCurrentAnimatorStateInfo(0).IsName("dashAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack(Second)")){
                    passedTime = 0;
                    OnAttack?.Invoke();
                }
                
            }
            else{
                //chasing the player
                Vector2 direction = player.transform.position - transform.position;
                OnMovementInput?.Invoke(direction.normalized);
            }
        }
        if(passedTime < attackDelay){
            passedTime += Time.deltaTime;
        }
    }
}
