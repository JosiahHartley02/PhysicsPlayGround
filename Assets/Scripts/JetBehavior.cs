using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBehavior : MonoBehaviour
{
    //Bool used to active the player
    public bool activePlayer;

    //List of thrusters needed to simulate flight
    [SerializeField]
    private ProjectileLauncher[] thruster;

    [Tooltip("NPC: Toggle thrusters on or off")]
    [SerializeField]
    public bool npc = false;

    //The main component of the aeroplane
    private Rigidbody bodyreference;
    //Top wing of the aeroplane
    [SerializeField]
    private Rigidbody topWing;
    //Bottom wing of the aeroplane
    [SerializeField]
    private Rigidbody bottomWing;
    
    //Where the thrusters are aiming their propulsion
    [SerializeField]
    private GameObject _target;

    //Cheat way to keep the aeroplane cheat
    private Quaternion desiredRotation;

    //List of previous locations for movement prediction
    [SerializeField]
    private Vector3[] previousLocations = new Vector3[0];
    //How often to record the current location for movement prediction
    [SerializeField]
    private float locationTimer = 0;
    private float bufferPeriod = 1;


    private void Start()
    {
        //On start we need to get the body component
        bodyreference = GetComponent<Rigidbody>();
        //We also want to get the cheat rotation to keep us straight
        desiredRotation = bodyreference.rotation;
    }
    private void Update()
    {
        //Update the current timer
        locationTimer += Time.deltaTime;

        //After every buffer period
        if (locationTimer > bufferPeriod)
        {
            //Mark the current location
            MarkLocation();
            //reset the timer
            locationTimer = 0;
        }

        //If the player jumps and its active or if its an npc we want the boosters boosting
        if ((Input.GetButton("Jump") && activePlayer) || npc)
            for(int i = 0; i < thruster.Length; i++)
                thruster[i].LaunchProjectile();

        //This is as much updating as we need up to this point if the player is not active or npc
        if (!activePlayer && !npc)
            return;

        //Cheat way to keep the aeroplane straight and steady
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
        //We make a temp array 1 unit less than the lenght of locations we have
        Vector3[] differences = new Vector3[previousLocations.Length - 1];
        //We make a temp sum;
        Vector3 differenceTotal = new Vector3();

        //keep an iterator of the location before
        int j = 0;
        for(int i = 1; i < previousLocations.Length; i++)
        {
            //set the differences at each iteration of the list
            differences[j] =  previousLocations[i] - previousLocations[j];
            j++;
        }

        //Tally the total
        for (int i = 0; i < differences.Length; i++)
            differenceTotal += differences[i];

        //Divide the total by how many there are
        differenceTotal = new Vector3(differenceTotal.x / previousLocations.Length, differenceTotal.y / previousLocations.Length, differenceTotal.z / previousLocations.Length);

        //Return what that average is
        return differenceTotal;
    }
}
