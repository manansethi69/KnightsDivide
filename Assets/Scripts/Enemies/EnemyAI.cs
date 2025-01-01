using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack, OnBlock, OnRangedAttack;

    [SerializeField]
    public GameObject player;
    
    [SerializeField]
    private float chaseDistanceThreshold =3, attackDistanceThreshold = 0.8f, rangedAttackThreshold = 15;
    [SerializeField]
    private float attackDelay = 0;

    public float rangedAttackDelay = 3;
    // private float blockDelay = 1f;
    // private bool attacked = false;
    private float passedTime = 1;
    private Animator animator;
    [SerializeField] private Transform groundCheck;
	[SerializeField] private float groundCheckRadius;
	[SerializeField] private LayerMask whatIsGround;

    private bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = player.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            return;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if(distance < chaseDistanceThreshold){
            OnPointerInput?.Invoke(player.transform.position);
            if(distance <= attackDistanceThreshold){
                OnMovementInput?.Invoke(Vector2.zero);
                // //block
                // if(passedTime >= blockDelay && attacked){
                    
                //     if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2")){
                //         passedTime = 0;
                //         attacked = false;
                //         OnBlock?.Invoke();
                //     }
                // }
                //attack
                
                if(passedTime >= attackDelay){
                    passedTime = 0;
                    // attacked = true;
                    
                    OnAttack?.Invoke();
                }
                
            }
            else{
                //chasing the player
                if(isGrounded){
                    Vector2 direction = player.transform.position - transform.position;
                    OnMovementInput?.Invoke(direction.normalized);
                }
                else{
                    OnMovementInput?.Invoke(Vector2.zero);
                }
            }   
        }
        else if(distance < rangedAttackThreshold){
            OnPointerInput?.Invoke(player.transform.position);
            if(passedTime >= rangedAttackDelay){
                passedTime = 0;
                OnRangedAttack?.Invoke();
            }
        }
        if(passedTime < attackDelay || passedTime < rangedAttackDelay){
            passedTime += Time.deltaTime;
        }
    }
}
