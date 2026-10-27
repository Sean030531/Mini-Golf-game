using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundGolfBall : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Rotation speed, adjustable via the Unity Editor
    public Rigidbody golfBall; // Reference to the golf ball's Rigidbody

    private bool isSwingTimerActive = false; // Controls whether the timer is active
    private float swingTimer = 0f; // Timer to track duration
    private float swingDuration = 3f; // Duration of the swing set to 3 seconds

    private bool canRotate = true; // Controls whether rotation is allowed

    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        // Stop rotation when the space key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            canRotate = false; // Stop rotation
        }

        // Allow rotation if enabled, using the Horizontal input to rotate
        if (canRotate)
        {
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.RotateAround(golfBall.transform.position, Vector3.up, -rotation);
        }

        // Start the timer when the space key is pressed and the timer is not already active
        if (Input.GetKeyDown(KeyCode.Space) && !isSwingTimerActive)
        {
            isSwingTimerActive = true; // Activate the timer
            swingTimer = Time.time; // Record the current time
        }

        // Check if the swing duration has been reached and the golf ball has stopped moving
        if (isSwingTimerActive && Time.time - swingTimer >= swingDuration && golfBall.velocity.magnitude == 0)
        {
            canRotate = true; // Allow rotation once the timer expires and the ball is stationary
            isSwingTimerActive = false; // Stop the timer
        }
    }
}





