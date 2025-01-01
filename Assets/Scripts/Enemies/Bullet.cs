using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage = 40;
    public Animator animator;
    private Rigidbody2D rb2d;
    public float speed;
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();

    }
	

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
        if(hitInfo.tag != "enemy"){
            rb2d.linearVelocity = Vector2.zero;
            rb2d.gravityScale = 0;
            Destroy(gameObject);
        }
        
		if(hitInfo.tag == "Player"){
            Health health;
            Block block;
            if(health = hitInfo.GetComponent<Health>())
            {
                if(block = hitInfo.GetComponentInChildren<Block>()){
                    if(block.IsBlocking){
                        Destroy(gameObject);
                        return;
                    }
                }
                health.GetHit(10, gameObject);
            }
            
        }
        
	}
	
}