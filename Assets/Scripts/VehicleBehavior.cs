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

    private void Update()
    {
        if (!activePlayer)
            return;
        //Getting a reference to a modifyable value for the motors
        frontRightMotor = frontRightWheel.motor;
        frontLeftMotor = frontLeftWheel.motor;
        backRightMotor = backRightWheel.motor;
        backLeftMotor = backLeftWheel.motor;
        
        float InputRight = Input.GetAxis("Horizontal");
        float InputForward = Input.GetAxis("Vertical");


        //Uniform 4 wheel drive acceleration and decceleration
        frontRightMotor.targetVelocity -= InputForward;
        backRightMotor.targetVelocity -= InputForward;
        frontLeftMotor.targetVelocity += InputForward;
        backLeftMotor.targetVelocity += InputForward;

        //Applying modified values to motors on the hinges
        frontRightWheel.motor = frontRightMotor;
        frontLeftWheel.motor = frontLeftMotor;
        backRightWheel.motor = backRightMotor;
        backLeftWheel.motor = backLeftMotor;
    }
}
