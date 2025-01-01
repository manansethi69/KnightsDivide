using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedAttackNew : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;
    public Animator animator;
    public float speed = 20f;
    private Rigidbody2D rb2d;
    void Start(){
        rb2d = GetComponentInParent<Rigidbody2D>();
    }
	public void Shoot ()
	{
        EnemyAI enemyAI = GetComponentInParent<EnemyAI>();
        GameObject player = enemyAI.player;
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        float direction = player.transform.position.x - transform.position.x;
        if(direction < 0){
           bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(speed * -1, 0);
        }
        else{
            bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(speed, 0);
        }
        bullet.GetComponent<Bullet>().speed = speed * -1;
        
	}

    public void rangedattck(){
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("rangedAttack");
    }
}