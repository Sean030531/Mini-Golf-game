using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GolfGameController : MonoBehaviour
{
    public TextMeshProUGUI parDisplayText; // UI component for displaying par score

    public Rigidbody clubRigidbody; // Reference to the club's Rigidbody
    public Rigidbody ballRigidbody; // Reference to the ball's Rigidbody
    public Transform clubAnchorPoint; // Anchor point of the club
    public Transform golfTee; // Transform of the golf tee

    private bool isBallHit = false; // Tracks whether the ball has been hit
    private int parCount = -4; // Counter for the number of swings

    private bool isSwingTimerActive = false; // Controls whether the swing timer is active
    private float swingTimerStartTime = 0f; // Start time for the swing timer
    private float swingDuration = 3f; // Duration of the swing timer, set to 3 seconds

    private Vector3 initialClubPositionRelativeToBall; // Initial relative position of the club anchor point to the ball
    private Vector3 initialBallPosition; // Initial position of the ball

    // Start is called before the first frame update
    void Start()
    {
        parDisplayText.text = "Par: -4";
    }

    // Update is called once per frame
    void Update()
    {
        if (isBallHit && ballRigidbody.velocity.magnitude < 0.03f)
        {
            ResetBallPosition();
        }

        if (ballRigidbody.transform.position.y < -5)
        {
            ballRigidbody.transform.position = initialBallPosition; // Reset ball position if it falls below a certain height
            ResetBallPosition();
        }

        HandleSwingControl();
    }

    void HandleSwingControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSwingTimerActive)
        {
            // Activate the swing timer
            isSwingTimerActive = true;
            swingTimerStartTime = Time.time; // Record current time

            // Calculate and store initial relative positions
            initialBallPosition = ballRigidbody.transform.position;
            initialClubPositionRelativeToBall = clubAnchorPoint.position - ballRigidbody.transform.position;
        }

        if (isSwingTimerActive)
        {
            // Check if three seconds have passed
            if (Time.time - swingTimerStartTime >= swingDuration)
            {
                isBallHit = true; // Mark the ball as hit
                parCount++;
                parDisplayText.text = "Par:" + parCount;
                isSwingTimerActive = false; // Stop the timer
            }
        }
    }

    void ResetBallPosition()
    {
        // Manually set ball velocity to zero
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        
        // Update ball position
        ballRigidbody.transform.position += new Vector3(0, 0.05002838f, 0);
        // Update golf tee position
        golfTee.position = ballRigidbody.transform.position + new Vector3(0, -0.06f, 0);
        
        ResetClubState();
        isBallHit = false; // Reset the hit flag
    }

    void ResetClubState()
    {
        ResetClubPhysicsSettings();

        // Set current Euler angles of the club
        Vector3 currentEulerAngles = clubRigidbody.transform.eulerAngles;
        clubRigidbody.transform.eulerAngles = new Vector3(0, currentEulerAngles.y, 0); // Reset X-axis rotation, retain Y and Z axis

        // Reset anchor point to a new position relative to the club as needed
        clubAnchorPoint.position = ballRigidbody.transform.position + initialClubPositionRelativeToBall;

        // Set club position relative to the golf ball
        clubRigidbody.transform.position = clubAnchorPoint.position + new Vector3(0, -0.2f, 0);

        // Once position and kinematics are reset, set isKinematic to false
        clubRigidbody.isKinematic = false;
    }

    void ResetClubPhysicsSettings()
    {
        // Retrieve and reset HingeJoint settings
        HingeJoint hinge = clubRigidbody.GetComponent<HingeJoint>();
        if (hinge != null)
        {
            JointMotor motor = hinge.motor;
            motor.force = 0;
            motor.targetVelocity = 0;
            motor.freeSpin = false;
            hinge.motor = motor;
            hinge.useMotor = true;
        }

        // Reset physics state of the Rigidbody
        if (clubRigidbody != null)
        {
            clubRigidbody.velocity = Vector3.zero;
            clubRigidbody.angularVelocity = Vector3.zero;
            clubRigidbody.isKinematic = true;
        }
    }
}
