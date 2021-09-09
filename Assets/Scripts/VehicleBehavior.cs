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


    public float desiredTireRotation = -23;

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
            //Rotate The Tires Toward the desired Rotation
            frontRightTire.transform.Rotate(0, 0, Mathf.Lerp(frontRightTire.transform.rotation.eulerAngles.z, desiredTireRotation, 0.0
                3f));

            frontLeftTire.transform.rotation =
                new Quaternion(frontLeftTire.transform.rotation.eulerAngles.x,
                Mathf.Lerp(frontLeftTire.transform.rotation.eulerAngles.y, desiredTireRotation, 0.3f),
                frontLeftTire.transform.rotation.eulerAngles.z,
                frontLeftTire.transform.rotation.w);

/*            //Move the joints anchor
            frontRightWheel.anchor =
                new Vector3(Mathf.Lerp(frontRightWheel.anchor.x,0.8f,0.3f),
                Mathf.Lerp(frontRightWheel.anchor.y, 0.03f, 0.3f),
                Mathf.Lerp(frontRightWheel.anchor.z, 0.305f, 0.3f));
            frontLeftWheel.anchor =
                new Vector3(Mathf.Lerp(frontLeftWheel.anchor.x, 0.8f, 0.3f),
                Mathf.Lerp(frontLeftWheel.anchor.y, 0.03f, 0.3f),
                Mathf.Lerp(frontLeftWheel.anchor.z, 0.305f, 0.3f));
            //Rotate the joints Axis
            frontRightWheel.axis =
                new Vector3(Mathf.Lerp(frontRightWheel.axis.x, 1, 0.3f),
                Mathf.Lerp(frontRightWheel.axis.y, 0, 0.3f),
                Mathf.Lerp(frontRightWheel.axis.z, 0.44f, 0.3f));
            frontLeftWheel.axis =
                new Vector3(Mathf.Lerp(frontLeftWheel.axis.x, -1, 0.3f),
                Mathf.Lerp(frontLeftWheel.axis.y, 0, 0.3f),
                Mathf.Lerp(frontLeftWheel.axis.z, -0.44f, 0.3f));*/
        }
        else if (InputRight > 0)
        {
            //Rotate the Tires Toward the desired Rotation
            frontRightTire.transform.Rotate(new Vector3(0, Mathf.Lerp(frontRightTire.transform.rotation.z, desiredTireRotation, 0.5f), 0));
            frontLeftTire.transform.Rotate(new Vector3(0, Mathf.Lerp(frontLeftTire.transform.rotation.z, desiredTireRotation, 0.5f), 0));
            //Move the joints anchor
            //Rotate the joints Axis
        }
        else
        {
            //Rotate the Tires toward the resting rotation
            frontRightTire.transform.Rotate(new Vector3(0, Mathf.Lerp(frontRightTire.transform.rotation.z, 0, 0.15f), 0));
            frontLeftTire.transform.Rotate(new Vector3(0, Mathf.Lerp(frontLeftTire.transform.rotation.z, 0, 0.15f), 0));
            //Move the joints anchor
            //Rotate the joints Axis
        }


        //Applying modified values to motors on the hinges
        frontRightWheel.motor = frontRightMotor;
        frontLeftWheel.motor = frontLeftMotor;
        backRightWheel.motor = backRightMotor;
        backLeftWheel.motor = backLeftMotor;
    }

    //Left Turn
    //Front Right Tire Y Rotation = -23
    //FrontRightWheel Anchor = (0.8,0.03,0.305)
    //FrontRightWheel Axis = (1,0,0.44)

    //Front Left Tire Y Rotation = -23
    //FrontLeftWheel Anchor = (-0.76,0.03,0.44)
    //FrontLeftWheel Axis = (-1,0,-0.44)
}
