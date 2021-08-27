using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    public HingeJoint frontRight;
    public HingeJoint frontLeft;
    public HingeJoint backRight;
    public HingeJoint backLeft;
    public Vector3 Rotation;
    private void FixedUpdate()
    {
        //Forward and Backwards
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            JointMotor rightmotor = frontRight.motor;
            rightmotor.targetVelocity -= 10;
            frontRight.motor = rightmotor;
            backRight.motor = rightmotor;
            JointMotor leftmotor = frontLeft.motor;
            leftmotor.targetVelocity += 10;
            frontLeft.motor = leftmotor;
            backLeft.motor = leftmotor;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            JointMotor rightmotor = frontRight.motor;
            rightmotor.targetVelocity += 10;
            frontRight.motor = rightmotor;
            JointMotor leftmotor = frontLeft.motor;
            leftmotor.targetVelocity -= 10;
            frontLeft.motor = leftmotor;
        }

        //Steering

    }
}
