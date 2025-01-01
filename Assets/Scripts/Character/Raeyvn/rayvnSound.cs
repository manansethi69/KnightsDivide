using System;
using UnityEngine;

public class rayvnSound : MonoBehaviour
{
    // AudioSource for walking
    public AudioSource runningSound; // AudioSource for running
    private Rigidbody2D rb2d;
    private PlayerControl playerControl;
    private bool isRunning = false;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        playerControl = GetComponent<PlayerControl>();
    }

    void Update()
    {

        // Handle running sound
        if (Math.Abs(rb2d.linearVelocity.x) > 1.9f && playerControl.isGrounded)
        {
            if (!isRunning)
            {

                // Play running sound
                runningSound.Play();
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                runningSound.Stop();
                isRunning = false;
            }
        }
    }
}
