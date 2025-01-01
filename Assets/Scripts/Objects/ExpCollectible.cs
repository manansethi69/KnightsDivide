using System;
using System.Threading;
using UnityEngine;

public class ExpCollectible : MonoBehaviour
{
    private GameObject player;
    private float timer;
    [SerializeField] private float timeBeforeCollection = 1f;
    [SerializeField] private float speed;
    private bool canCollect;
    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if(canCollect == false) {
            if(timer < timeBeforeCollection) {
                timer += Time.deltaTime;
            } else{
                canCollect = true;
                rb.gravityScale = 0;
            }
        }

        if(canCollect == true) {
            Vector3 xpMovement = player.transform.position - transform.position;
            Debug.Log(xpMovement);
            rb.linearVelocity = xpMovement * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            other.GetComponent<PlayerStats>().addExp(1);
            Destroy(gameObject);
        }
    }
}
