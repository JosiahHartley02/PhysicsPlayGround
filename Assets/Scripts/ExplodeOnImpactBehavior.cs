using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpactBehavior : MonoBehaviour
{
    //The Collider to hold the explosion radius
    [SerializeField]
    private SphereCollider _explosionMatrix;

    //The force to apply when explosion happens
    [SerializeField]
    private float _explosionForce = 300;
    //Radius affected by explosion
    public float _explosionRadius;

    //Array of rigid bodies in the radius of the explosion
    private Rigidbody[] _affectedBodies = new Rigidbody[0];

    private void Start()
    {
        //At the start, update the explosion radius to be what the user has set
        _explosionMatrix.radius = _explosionRadius;
    }
    private void OnCollisionEnter(Collision collision)
    {
        ActivePlayerSwitch temp;
        UnityChanNPCBehavior reference;
        //when the bomb hits something apply a force to everything within radius
        for (int i = 0; i < _affectedBodies.Length; i++)
        {
            _affectedBodies[i].AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 20);
        }

        //Destroy the bomb
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Create a temp rigidBody value
        Rigidbody temp;
        //Check to see if the other collider is attached to a rigid body and is not null
        if (other.TryGetComponent<Rigidbody>(out temp) && other != null)
            if (temp != null)
            {
                //Create a new array that is one larger than the current
                Rigidbody[] tempArray = new Rigidbody[_affectedBodies.Length + 1];
                //Copy the old array onto the temp array
                for(int i = 0; i < _affectedBodies.Length; i++)
                {
                    tempArray[i] = _affectedBodies[i];
                }
                //set the new collider at the end of the temp array
                tempArray[tempArray.Length - 1] = temp;
                //set the old array to be the temp array
                _affectedBodies = tempArray;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        //Create a temp rigidBody value
        Rigidbody temp;
        //Check to see if the other collider is attached to a rigid body since rigidbodys are all we care about
        if (other.TryGetComponent<Rigidbody>(out temp) && other != null)
        {
            //Create a rigid body array that is one unit shorter than the affected bodies list
            Rigidbody[] tempArray = new Rigidbody[_affectedBodies.Length - 1];
            //Create an int pointer
            int j = 0;
            //For each item in the affected bodies list
            for (int i = 0; i < _affectedBodies.Length; i++)
            {
                //check to see if the other collider is whats being pointed at
                if (_affectedBodies[i] != other.attachedRigidbody)
                {
                    //if its not, then copy the data
                    tempArray[j] = _affectedBodies[i];
                    //increment the pointer to acknowledge we copied data
                    j++;
                }
                //pointer would otherwise not increment
            }
            //Replace the original rigid body array with the temp one
            _affectedBodies = tempArray;
        }
    }
}
