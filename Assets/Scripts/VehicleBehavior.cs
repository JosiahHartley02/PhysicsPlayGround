using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    [SerializeField]
    public bool activePlayer = false;

    [SerializeField]
    private SingleWheelVehicleBehavior frontAxel;
    private ConfigurableJoint frontJoint;
    [SerializeField]
    private SingleWheelVehicleBehavior rearAxel;

    private void Awake()
    {
        frontJoint = frontAxel.GetComponent<ConfigurableJoint>();
    }
    private void Update()
    {
        if (!activePlayer)
            return;
        float InputForward = Input.GetAxis("Vertical");
        float InputRight = Input.GetAxis("Horizontal");
        frontAxel.UpdateDesiredVelocity(InputForward);
        rearAxel.UpdateDesiredVelocity(InputForward);

        frontAxel.transform.Rotate(0, InputRight * 3, 0);

        if (Input.GetMouseButton(0))
        {
            //cast a ray to see what the player clicked on
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.gameObject.tag == "Player")
            {
                activePlayer = false;
                PlayerController vehicle = hitInfo.transform.gameObject.GetComponent<PlayerController>();
                vehicle.activePlayer = true;
            }
        }
    }
}
