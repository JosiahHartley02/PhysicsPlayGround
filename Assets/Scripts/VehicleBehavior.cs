using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    [SerializeField]
    public bool activePlayer = false;

    [SerializeField]
    private HingeJoint turningAxel;
    [SerializeField]
    private SingleWheelVehicleBehavior frontAxel;
    [SerializeField]
    private SingleWheelVehicleBehavior rearAxel;

    private void Update()
    {
        float InputForward = Input.GetAxis("Vertical");
        float InputRight = Input.GetAxis("Horizontal");
        frontAxel.UpdateDesiredVelocity(InputForward);
        rearAxel.UpdateDesiredVelocity(InputForward);

        JointMotor _turning = turningAxel.motor;
        _turning.targetVelocity += InputRight;
        _turning.targetVelocity = Mathf.Clamp(_turning.targetVelocity, -50, 50);
        turningAxel.motor = _turning;
    }
}
