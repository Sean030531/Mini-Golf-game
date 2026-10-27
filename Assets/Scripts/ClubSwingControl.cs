using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ClubSwingControl : MonoBehaviour
{
    public Rigidbody clubRigidbody; // Rigidbody component of the club for physics interaction
    public HingeJoint clubHinge; // Hinge joint component for the club to swing
    public Slider powerSlider; // UI slider to display and control the swing power
    public float maxForce = 1000f; // Maximum force that can be applied to the club swing
    public float chargeSpeed = 1f; // Rate at which the power bar charges

    private bool isPowerCharging = false; // Flag to check if the power is currently charging
    private bool isPowerDecreasing = false; // Flag to control if the slider value should increase or decrease
    private bool isSwingPrepared = false; // Flag to check if the swing is ready to be executed

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleCharging();
        HandleSwing();
    }

    void HandleCharging()
    {
        // Start charging when space key is pressed down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPowerCharging = true;
            isPowerDecreasing = false;
        }
        // Stop charging and prepare to swing when space key is released
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isPowerCharging = false;
            isSwingPrepared = true;
        }

        // Update the power slider based on current charging state
        if (isPowerCharging)
        {
            UpdatePowerSlider();
        }
    }

    void UpdatePowerSlider()
    {
        // Calculate the change in slider value
        float delta = chargeSpeed * Time.deltaTime * (isPowerDecreasing ? -1 : 1);
        powerSlider.value += delta;

        // Reverse the charging direction at limits
        if (powerSlider.value >= powerSlider.maxValue)
        {
            powerSlider.value = powerSlider.maxValue;
            isPowerDecreasing = true;
        }
        else if (powerSlider.value <= 0)
        {
            powerSlider.value = 0;
            isPowerDecreasing = false;
        }
    }

    void HandleSwing()
    {
        // Execute the swing if it's ready
        if (isSwingPrepared)
        {
            Swing(powerSlider.value / powerSlider.maxValue);
            isSwingPrepared = false;
            powerSlider.value = 0; // Reset the power slider
            isPowerDecreasing = false; // Reset the decreasing flag
        }
    }

    void Swing(float power)
    {
        // Enable physics interactions and setup the motor of the hinge joint based on the power applied
        clubRigidbody.isKinematic = false;
        clubHinge.useMotor = true;
        JointMotor motor = clubHinge.motor;
        motor.force = maxForce * power;
        motor.targetVelocity = -motor.force; // Set the target velocity to be negative to swing correctly
        clubHinge.motor = motor;
    }
    

    /*
        I've tried using AddTorque to swing the club, but the power of the swing is always too weak, 
        no matter how much I adjust the maxTorque value. Since AddTorque isn't effective, I've been using motor force, 
        although it falls a bit short of the natural swing I'm aiming for. 
        I don't fully understand where the problem lies, 
        so I'm going to leave the code as it is for now and look into adjustments when I have the chance.
    */

    /*
    public Transform golfBall; // Transform of the golf ball
    public float maxTorque = 1000f; // Maximum torque that can be applied

    void Start() 
    {
        clubRigidbody.centerOfMass = new Vector3(0, -0.2572f, 0.0121f); // Set the center of mass to the center of club head
    }

    void Swing(float power)
    {
        // Calculate the direction of the hit from the club's center of mass towards the golf ball
        Vector3 hitDirection = (golfBall.position - (clubRigidbody.position + clubRigidbody.centerOfMass)).normalized;

        // Normalize the position of the force arm to ensure it only affects the direction
        Vector3 forceArm = (clubRigidbody.centerOfMass).normalized;

        // Calculate the torque direction as the cross product of the force arm and hit direction
        Vector3 torqueDirection = Vector3.Cross(forceArm, hitDirection).normalized;

        // Apply the torque to the club's rigidbody
        clubRigidbody.AddTorque(torqueDirection * maxTorque * power, ForceMode.Impulse);
    }
    */
}



