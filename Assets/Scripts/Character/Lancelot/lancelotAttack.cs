using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class lancelotAttack : MonoBehaviour
{
    // private GameObject firstAttack = default;
    //  private GameObject secondAttack = default;
    public Animator animator;
    // public bool canAttack = true;

    // public float cooldownTime = 0f;   

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // // Update is called once per frame
    // void Update()
    // {
    //     if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1")){
    //         firstAttack.SetActive(true);
    //     }
    //     else{
    //         firstAttack.SetActive(false);
    //     }
    //     if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2")){
    //         secondAttack.SetActive(true);
    //     }
    //     else{
    //         secondAttack.SetActive(false);
    //     }
    // }
    public float delay = 0.3f;
    private bool attackBlocked;

    public bool IsAttacking { get; private set; }

    public Transform circleOrigin;
    public float radius;

    public int damage;
    public int atkBoostDamage = 0;
    public AudioSource swordSound;
    public void ResetIsAttacking()
    {
        IsAttacking = false;
        if (radius > 1){
            radius = 1;
        }
    }

    private void Update()
    {
        if (IsAttacking)
            return;

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1")){
            damage = 35 + atkBoostDamage;
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2")){
            damage = 50 + atkBoostDamage;
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("special")){
            damage = 100 + atkBoostDamage;
            radius = 3;
        }
    }

    public void Attack()
    {
        if (attackBlocked)
            return;
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
            
            Health health;
            Block block;
            attackBoss attackBoss;
            Attack();
            swordSound.Play();
            if(collider.tag == "enemy")
            {   
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1")){
                    damage = 35;
                }
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2")){
                    damage = 50;
                }
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("special")){
                    damage = 100;
                    radius = 3;
                }
                health = collider.GetComponent<Health>();
                if(block = collider.GetComponentInChildren<Block>()){
                    if(block.IsBlocking){
                        animator.SetTrigger("knockback");
                        return;
                    }
                }
                // Debug.Log(collider.name);
                if(attackBoss = collider.GetComponentInChildren<attackBoss>()){
                    
                    float dodgeChance = Random.Range(0, 1f);
                    if(dodgeChance >= attackBoss.dodgeChance){
                        attackBoss.DashAttack();
                        return;
                    }
                }
                
                health.GetHit(damage, transform.parent.gameObject);
            }
        }
    }

    public void adjustAtkBoost(int dmg) {
        atkBoostDamage = dmg;
    }
}
