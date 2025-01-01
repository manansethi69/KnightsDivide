using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of the bat
    public float moveDistance = 5f; // Total distance the bat moves on the X-axis
    private Vector3 startPosition;
    private bool movingRight = false; // To track the direction

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Calculate the target positions for left and right movement
        float maxX = startPosition.x + moveDistance / 2f;
        float minX = startPosition.x - moveDistance / 2f;

        if (movingRight)
        {
            // Move right
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= maxX)
            {
                movingRight = false; // Switch direction
                Flip(); // Flip the sprite
            }
        }
        else
        {
            // Move left
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= minX)
            {
                movingRight = true; // Switch direction
                Flip(); // Flip the sprite
            }
        }
    }

    // Flip the sprite to face the opposite direction
    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}

