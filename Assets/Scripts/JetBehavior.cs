using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBehavior : MonoBehaviour
{
    public bool activePlayer;

    [SerializeField]
    private ProjectileLauncher[] thruster;

    [SerializeField]
    public bool npc = false;
    private Rigidbody bodyreference;
    [SerializeField]
    private Rigidbody topWing;
    [SerializeField]
    private Rigidbody bottomWing;

    [SerializeField]
    private GameObject _target;

    private Quaternion desiredRotation;

    [SerializeField]
    private Vector3[] previousLocations = new Vector3[0];
    [SerializeField]
    private float locationTimer = 0;
    private float bufferPeriod = 1;


    private void Start()
    {
        bodyreference = GetComponent<Rigidbody>();
        desiredRotation = bodyreference.rotation;
    }
    private void Update()
    {
        locationTimer += Time.deltaTime;

        if (locationTimer > bufferPeriod)
        {
            MarkLocation();
            locationTimer = 0;
        }


        if ((Input.GetButton("Jump") && activePlayer) || npc)
            for(int i = 0; i < thruster.Length; i++)
                thruster[i].LaunchProjectile();

        if (!activePlayer && !npc)
            return;

        float InputRight = Input.GetAxis("Horizontal") * 0.10f;
        float InputUp = Input.GetAxis("Vertical") * 0.10f;

        bodyreference.MoveRotation(desiredRotation);
        topWing.MoveRotation(desiredRotation);
        bottomWing.MoveRotation(desiredRotation);
    }

    private void MarkLocation()
    {
        if(previousLocations.Length == 3)
            RidOldestLocation();
        //Create a temp rigidBody value
        Vector3 temp = (transform.position);
        //Create a new array that is one larger than the current
        Vector3[] tempArray = new Vector3[previousLocations.Length + 1];
        //Copy the old array onto the temp array
        for (int i = 0; i < previousLocations.Length; i++)
        {
            tempArray[i] = previousLocations[i];
        }
        //set the new collider at the end of the temp array
        tempArray[tempArray.Length - 1] = temp;
        //set the old array to be the temp array
        previousLocations = tempArray;
    }

    private void RidOldestLocation()
    {
        //Create a temp rigidBody value
        Vector3 temp = new Vector3();
        //Create a new array that is one smaller than the current
        Vector3[] tempArray = new Vector3[previousLocations.Length - 1];
        //Copy the old array onto the temp array
        int j = 0;
        for (int i = 1; i < previousLocations.Length; i++)
        {
            tempArray[j] = previousLocations[i];
            j++;
        }
        //set the old array to be the temp array
        previousLocations = tempArray;

        _target.transform.position = transform.position + AverageLocationVelocity() * 3;
    }

    private Vector3 AverageLocationVelocity()
    {
        Vector3[] differences = new Vector3[9];
        Vector3 differenceTotal = new Vector3();

        int j = 0;
        for(int i = 1; i < previousLocations.Length; i++)
        {
            differences[j] =  previousLocations[i] - previousLocations[j];
            j++;
        }

        for (int i = 0; i < differences.Length; i++)
            differenceTotal += differences[i];

        differenceTotal = new Vector3(differenceTotal.x / 9, differenceTotal.y / 9, differenceTotal.z / 9);

        return differenceTotal;
    }
}
