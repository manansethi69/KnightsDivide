using System;
using UnityEngine;

public class LancelotMovementSound : MonoBehaviour
{
    public AudioSource walkingSound;  // AudioSource for walking
    public AudioSource runningSound; // AudioSource for running
    private Rigidbody2D rb2d;
    private lancelotController lancelotController;
    private bool isWalking = false;
    private bool isRunning = false;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        lancelotController = GetComponent<lancelotController>();
    }

    void Update()
    {

        // Handle walking sound
        if (Math.Abs(rb2d.linearVelocity.x) > 1.9f && !lancelotController.isRunning && lancelotController.isGrounded)
        {
            if (!isWalking)
            {
                // Stop running sound if playing
                if (isRunning)
                {
                    runningSound.Stop();
                    isRunning = false;
                }

                // Play walking sound
                walkingSound.Play();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                walkingSound.Stop();
                isWalking = false;
            }
        }

        // Handle running sound
        if (Math.Abs(rb2d.linearVelocity.x) > 1.9f && lancelotController.isRunning && lancelotController.isGrounded)
        {
            if (!isRunning)
            {
                // Stop walking sound if playing
                if (isWalking)
                {
                    walkingSound.Stop();
                    isWalking = false;
                }

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

