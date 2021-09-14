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
        //when the bomb hits something apply a force to everything within radius
        for (int i = 0; i < _affectedBodies.Length; i++)
            _affectedBodies[i].AddExplosionForce(_explosionForce, transform.position, _explosionRadius,20);
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
        Rigidbody[] tempArray;
        if (_affectedBodies.Length == 0)
            tempArray = new Rigidbody[0];
        else
            tempArray = new Rigidbody[_affectedBodies.Length - 1];
        int j = 0;
        for(int i = 0; i < _affectedBodies.Length; i++)
        {
            if(_affectedBodies[i] != other.attachedRigidbody)
            {
                tempArray[j] = _affectedBodies[i];
                j++;
            }
        }
        _affectedBodies = tempArray;
    }
}
