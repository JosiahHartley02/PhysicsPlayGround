using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    [SerializeField]
    public bool activePlayer = false;

    public HingeJoint frontRight;
    public HingeJoint frontLeft;
    public HingeJoint backRight;
    public HingeJoint backLeft;
    private void Update()
    {
        if (!activePlayer)
            return;
        //Forward and Backwards
        if(Input.GetKey(KeyCode.UpArrow))
        {
            JointMotor rightmotor = frontRight.motor;
            rightmotor.targetVelocity -= 1;
            frontRight.motor = rightmotor;
            backRight.motor = rightmotor;
            JointMotor leftmotor = frontLeft.motor;
            leftmotor.targetVelocity += 1;
            frontLeft.motor = leftmotor;
            backLeft.motor = leftmotor;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            JointMotor rightmotor = frontRight.motor;
            rightmotor.targetVelocity += 1;
            frontRight.motor = rightmotor;
            backRight.motor = rightmotor;
            JointMotor leftmotor = frontLeft.motor;
            leftmotor.targetVelocity -= 1;
            frontLeft.motor = leftmotor;
            backLeft.motor = leftmotor;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            JointMotor rightmotor = frontRight.motor;
            rightmotor.targetVelocity -= 1;
            frontRight.motor = rightmotor;
            backRight.motor = rightmotor;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            JointMotor leftmotor = frontLeft.motor;
            leftmotor.targetVelocity += 1;
            frontLeft.motor = leftmotor;
            backLeft.motor = leftmotor;
        }
    }
}
