using UnityEngine;

public class projectileBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private LayerMask colliderObjs; 
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
        rb.linearVelocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if((colliderObjs.value & (1 << other.gameObject.layer)) > 0) {
            //on collision behavior
        }
    }
}
