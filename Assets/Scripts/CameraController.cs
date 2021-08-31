using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distanceFromTarget = 5.0f;
    public float sensitivity = 2.0f;
    public bool invertY = false;
    public bool invertX = false;
    private void Update()
    {
        //Rotate the camera
        if(Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            Vector2 rotation;
            rotation.x = Input.GetAxis("Mouse Y") * (invertY ? 1.0f : -1.0f);
            rotation.y = Input.GetAxis("Mouse X") * (invertX ? 1.0f : -1.0f);
            //Look up and down by rotating around the X-axis
            angles.x = Mathf.Clamp(angles.x + rotation.x * sensitivity, 0.0f,70.0f);
            //Look left and right by rotating around the Y-axis
            angles.y += rotation.y * sensitivity;
            //Set the angles
            transform.eulerAngles = angles;
        }
        //Move the camera
        transform.position = target.position + (distanceFromTarget * -transform.forward);
    }
}
