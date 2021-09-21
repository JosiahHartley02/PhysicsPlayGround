using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterBehavior : MonoBehaviour
{
    [SerializeField]
    private BoxCollider boosterCollider;
    [SerializeField]
    float boosterPower = 5;


    private Rigidbody[] _bodiesToBoost = new Rigidbody[0];
    private void OnTriggerEnter(Collider other)
    {
        //Create a temp rigidBody value
        Rigidbody temp;
        //Check to see if the other collider is attached to a rigid body and is not null
        if (other.TryGetComponent<Rigidbody>(out temp) && other != null)
            if (temp != null)
            {
                //Create a new array that is one larger than the current
                Rigidbody[] tempArray = new Rigidbody[_bodiesToBoost.Length + 1];
                //Copy the old array onto the temp array
                for (int i = 0; i < _bodiesToBoost.Length; i++)
                {
                    tempArray[i] = _bodiesToBoost[i];
                }
                //set the new collider at the end of the temp array
                tempArray[tempArray.Length - 1] = temp;
                //set the old array to be the temp array
                _bodiesToBoost = tempArray;
            }
    }
    private void Update()
    {
        for(int i = 0; i < _bodiesToBoost.Length; i++)
            _bodiesToBoost[i].AddForceAtPosition(transform.forward.normalized * boosterPower, _bodiesToBoost[i].position);
    }

    private void OnTriggerExit(Collider other)
    {
        //Create a temp rigidBody value
        Rigidbody temp;
        //Check to see if the other collider is attached to a rigid body since rigidbodys are all we care about
        if (other.TryGetComponent<Rigidbody>(out temp) && other != null)
        {
            //Create a rigid body array that is one unit shorter than the affected bodies list
            Rigidbody[] tempArray = new Rigidbody[_bodiesToBoost.Length - 1];
            //Create an int pointer
            int j = 0;
            //For each item in the affected bodies list
            for (int i = 0; i < _bodiesToBoost.Length; i++)
            {
                //check to see if the other collider is whats being pointed at
                if (_bodiesToBoost[i] != other.attachedRigidbody)
                {
                    //if its not, then copy the data
                    tempArray[j] = _bodiesToBoost[i];
                    //increment the pointer to acknowledge we copied data
                    j++;
                }
                //pointer would otherwise not increment
            }
            //Replace the original rigid body array with the temp one
            _bodiesToBoost = tempArray;
        }
    }
}
