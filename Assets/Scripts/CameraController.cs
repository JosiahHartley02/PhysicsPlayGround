using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] target;
    public GameObject[] targetChecks;
    public float desiredDistance = 5.0f;
    public float sensitivity = 2.0f;
    public float relaxSpeed = 5.0f;
    public bool invertY = false;
    public bool invertX = false;

    private float currentDistance = 0.0f;

    private PlayerController _boxChanController;
    private VehicleBehavior _vehicleController;
    private JetBehavior _jetBehavior;
    private int _currentCameraTarget = 0;

    private void Start()
    {
        _boxChanController = targetChecks[0].GetComponent<PlayerController>();
        _vehicleController = targetChecks[1].GetComponent<VehicleBehavior>();
        /*_jetBehavior = targetChecks[2].GetComponent<JetBehavior>();*/

        currentDistance = desiredDistance;
    }
    private void Update()
    {
        if (_boxChanController.activePlayer)
            _currentCameraTarget = 0;
        else if (_vehicleController.activePlayer)
            _currentCameraTarget = 1;/*
        else if (_jetBehavior.activePlayer)
            _currentCameraTarget = 2;*/

        desiredDistance -= Input.GetAxis("Mouse ScrollWheel");
        //Rotate the camera
        if(Input.GetMouseButton(1))
        {
            //Store current angles
            Vector3 angles = transform.eulerAngles;
            //Get input
            Vector2 rotation;
            rotation.x = Input.GetAxis("Mouse Y") * (invertY ? 1.0f : -1.0f);
            rotation.y = Input.GetAxis("Mouse X") * (invertX ? 1.0f : -1.0f);
            //Look up and down by rotating around the X-axis
            angles.x = Mathf.Clamp(angles.x + rotation.x * sensitivity, 0,80.0f);
            //Look left and right by rotating around the Y-axis
            angles.y += rotation.y * sensitivity;
            //Set the updated angles
            transform.eulerAngles = angles;
        }
        //Move the camera
        RaycastHit hitInfo;
        if (Physics.Raycast(target[_currentCameraTarget].position, -transform.forward, out hitInfo, desiredDistance))
        {
            currentDistance = Mathf.MoveTowards(currentDistance, hitInfo.distance, Time.deltaTime * relaxSpeed * (desiredDistance / currentDistance));
        }
        else
            currentDistance = Mathf.MoveTowards(currentDistance, desiredDistance, Time.deltaTime * relaxSpeed * (desiredDistance/ currentDistance));
        
        transform.position = target[_currentCameraTarget].position + (currentDistance * -transform.forward);
    }
}
