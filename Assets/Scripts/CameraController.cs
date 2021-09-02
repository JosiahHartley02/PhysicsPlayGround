using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float desiredDistance = 5.0f;
    public float sensitivity = 2.0f;
    public float relaxSpeed = 5.0f;
    public bool invertY = false;
    public bool invertX = false;

    private float currentDistance = 0.0f;

    private void Start()
    {
        currentDistance = desiredDistance;
    }
    private void Update()
    {
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
            angles.x = Mathf.Clamp(angles.x + rotation.x * sensitivity, 0.0f,80.0f);
            //Look left and right by rotating around the Y-axis
            angles.y += rotation.y * sensitivity;
            //Set the updated angles
            transform.eulerAngles = angles;
        }
        //Move the camera
        RaycastHit hitInfo;
        if (Physics.Raycast(target.position, -transform.forward, out hitInfo, desiredDistance))
        {
            currentDistance = Mathf.MoveTowards(currentDistance, hitInfo.distance, Time.deltaTime * relaxSpeed * (desiredDistance / currentDistance));
        }
        else
            currentDistance = Mathf.MoveTowards(currentDistance, desiredDistance, Time.deltaTime * relaxSpeed * (desiredDistance/ currentDistance));
        
        transform.position = target.position + (currentDistance * -transform.forward);
    }
}
