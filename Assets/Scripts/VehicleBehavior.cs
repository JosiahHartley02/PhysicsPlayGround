using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    [SerializeField]
    public bool activePlayer = false;

    public HingeJoint frontRightWheel;
    public HingeJoint frontLeftWheel;
    public HingeJoint backRightWheel;
    public HingeJoint backLeftWheel;

    private JointMotor frontRightMotor;
    private JointMotor frontLeftMotor;
    private JointMotor backRightMotor;
    private JointMotor backLeftMotor;


    public float desiredTireRotation = 20;

    private void Update()
    {
        if (!activePlayer)
            return;
        //Getting a reference to a modifyable value for the motors
        frontRightMotor = frontRightWheel.motor;
        frontLeftMotor = frontLeftWheel.motor;
        backRightMotor = backRightWheel.motor;
        backLeftMotor = backLeftWheel.motor;

        //Getting a reference to the front two wheels
        GameObject frontRightTire = frontRightWheel.connectedBody.gameObject;
        GameObject frontLeftTire = frontLeftWheel.connectedBody.gameObject;
        
        float InputForward = Input.GetAxis("Vertical");
        float InputRight = Input.GetAxis("Horizontal");

        //Uniform 4 wheel drive acceleration and decceleration
        frontRightMotor.targetVelocity -= InputForward;
        backRightMotor.targetVelocity -= InputForward;
        frontLeftMotor.targetVelocity += InputForward;
        backLeftMotor.targetVelocity += InputForward;

        //If the player turns left
        //INTRODUCE CLAMPING TO EACH FUNCTION TO PREVENT OVER ROTATING
        if (InputRight < 0)
        {
            frontRightTire.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(frontRightTire.transform.rotation.z, desiredTireRotation, 0.3f)));
            frontLeftTire.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(frontLeftTire.transform.rotation.z, desiredTireRotation, 0.3f)));
        }
        else if (InputRight > 0)
        {
            frontRightTire.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(frontRightTire.transform.rotation.z, -desiredTireRotation, 0.3f)));
            frontLeftTire.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(frontLeftTire.transform.rotation.z, -desiredTireRotation, 0.3f)));
        }
        else
        {
            frontRightTire.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(frontRightTire.transform.rotation.z, 0, 0.15f)));
            frontLeftTire.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(frontLeftTire.transform.rotation.z, 0, 0.15f)));
        }


        //Applying modified values to motors on the hinges
        frontRightWheel.motor = frontRightMotor;
        frontLeftWheel.motor = frontLeftMotor;
        backRightWheel.motor = backRightMotor;
        backLeftWheel.motor = backLeftMotor;
    }
}
