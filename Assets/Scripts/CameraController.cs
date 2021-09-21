using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("KEEP IN ORDER PVJ: Camera Points to focus depending on activePlayer")]
    public Transform[] cameraFocusPoint;

    [Tooltip("KEEP IN ORDER PVJ: References to the gameObjects holding the player controller scripts")]
    public GameObject[] activePlayerChecks;

    public float desiredDistance = 5.0f;
    public float sensitivity = 2.0f;
    public float relaxSpeed = 5.0f;

    public bool invertY = false;
    public bool invertX = false;

    private int _currentCameraTarget = 0;
    private float currentDistance = 0.0f;

    //Reference to the player
    private PlayerController _boxChanController;
    //Reference to the player car
    private VehicleBehavior _vehicleController;
    //Reference to the player plane
    private JetBehavior _jetBehavior;

    private void Start()
    {
        //Initialize the player check
        _boxChanController = activePlayerChecks[0].GetComponent<PlayerController>();
        //Initialize the car check
        _vehicleController = activePlayerChecks[1].GetComponent<VehicleBehavior>();
        //Initialize the jet check
        _jetBehavior = activePlayerChecks[2].GetComponent<JetBehavior>();

        //Initialize the current distance variable
        currentDistance = desiredDistance;
    }
    private void Update()
    {
        //Check to see what camera we should be focusing on staying in order
        //we check the player to see if the player is active, if so focus on the player
        if (_boxChanController.activePlayer)
            _currentCameraTarget = 0;
        //we check to see if the vehicle is active, if so focus on the vehicle
        else if (_vehicleController.activePlayer)
            _currentCameraTarget = 1;
        //we check to see if the jet is active, if so focus on the jet
        else if (_jetBehavior.activePlayer)
            _currentCameraTarget = 2;

        //We allow the player to manually adjust the camera distance
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
        if (Physics.Raycast(cameraFocusPoint[_currentCameraTarget].position, -transform.forward, out hitInfo, desiredDistance))
        {
            currentDistance = Mathf.MoveTowards(currentDistance, hitInfo.distance, Time.deltaTime * relaxSpeed * (desiredDistance / currentDistance));
        }
        else
            currentDistance = Mathf.MoveTowards(currentDistance, desiredDistance, Time.deltaTime * relaxSpeed * (desiredDistance/ currentDistance));
        
        transform.position = cameraFocusPoint[_currentCameraTarget].position + (currentDistance * -transform.forward);
    }
}
