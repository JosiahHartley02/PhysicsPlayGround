using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWheelVehicleBehavior : MonoBehaviour
{
    //Get a permanent reference to the two tires on this axel
    [SerializeField]
    public HingeJoint leftWheel;
    [SerializeField]
    public HingeJoint rightWheel;

    //Create an instance of what will hold the motors
    private JointMotor _rightMotor = new JointMotor();
    private JointMotor _leftMotor = new JointMotor();

    public void UpdateDesiredVelocity(float tothisMuch)
    {
        //set the reference of the motors equal to the motors
        _rightMotor = rightWheel.motor;
        _leftMotor = leftWheel.motor;
        //modify the reference motors
        _rightMotor.targetVelocity = Mathf.Lerp(_rightMotor.targetVelocity,-tothisMuch,0.8f);
        _leftMotor.targetVelocity = Mathf.Lerp(_rightMotor.targetVelocity, tothisMuch, 0.8f); ;

        //Clamp the motor so the vehicle doesn't start flipping
        _rightMotor.targetVelocity = Mathf.Clamp(_rightMotor.targetVelocity, -1000, 1000);
        _leftMotor.targetVelocity = Mathf.Clamp(_leftMotor.targetVelocity, -1000, 1000);

        //set the motors equal to the reference motors
        rightWheel.motor = _rightMotor;
        leftWheel.motor = _leftMotor;
    }
}
