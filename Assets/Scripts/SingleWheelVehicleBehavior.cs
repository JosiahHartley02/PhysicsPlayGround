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

    public void UpdateDesiredVelocity(float tothisMuch, float InputRight)
    {
        tothisMuch *= 800;

        float InputLeft = 0;
        if (InputRight < 0)
            InputLeft = -InputRight;

        //set the reference of the motors equal to the motors
        _rightMotor = rightWheel.motor;
        _leftMotor = leftWheel.motor;
        //modify the reference motors                                               Apply pos or neg value to turn
        _rightMotor.targetVelocity = Mathf.Lerp(_rightMotor.targetVelocity,-tothisMuch - (InputLeft * 300),0.8f);
        _leftMotor.targetVelocity = Mathf.Lerp(_rightMotor.targetVelocity, tothisMuch + (InputRight * 300), 0.8f);

        //Clamp the motor so the vehicle doesn't start flipping
        _rightMotor.targetVelocity = Mathf.Clamp(_rightMotor.targetVelocity, -800, 800);
        _leftMotor.targetVelocity = Mathf.Clamp(_leftMotor.targetVelocity, -800, 800);

        //set the motors equal to the reference motors
        rightWheel.motor = _rightMotor;
        leftWheel.motor = _leftMotor;
    }
}
