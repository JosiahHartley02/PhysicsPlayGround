using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distanceFromTarget = 5.0f;
    private void Update()
    {
        //Rotate the camera
        if(Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            Vector2 mouse;
            mouse.x = Input.GetAxis("Mouse X");
            mouse.y = Input.GetAxis("Mouse Y");
            //Look up and down by rotating around X-axis
        }
        //Move the camera
        transform.position = target.position + (distanceFromTarget * -transform.forward);
    }
}
