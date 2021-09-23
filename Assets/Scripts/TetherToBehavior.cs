using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherToBehavior : MonoBehaviour
{
    public GameObject objectToTetherFrom;
    public GameObject objectToTetherTo;

    [SerializeField]
    private GameObject _ropePrefab;
    [SerializeField]
    private bool _tetherOnStart = false;

    private GameObject[] _objectsTethered;

    private GameObject[] _ropeLinks = new GameObject[0];

    private void Start()
    {
        if (_tetherOnStart)
            Tether(objectToTetherTo);
    }
    public void Tether(GameObject itemToTetherTo)
    {
        //We get the height of the rope to see how many we will need
        float heightOfRope = _ropePrefab.transform.localScale.z;
        //We need to check to see how far away the item tethering is in each direction
        Vector3 displacement = itemToTetherTo.transform.position - objectToTetherFrom.transform.position;
        //We need to see what direction that item is
        Vector3 rotation = displacement.normalized;

        //We get the length of the desired rope to see how many we will need
        float distance = displacement.magnitude;
        //We check to see how many ropes we will need by dividing the distance by the length of each rope
        int numberOfRopes = Mathf.RoundToInt(distance / heightOfRope);

        //We hold a spawn position for each rope we want to instantiate
        Vector3 spawnHere = objectToTetherFrom.transform.position;

        //Create a temp array to replace the current array
        GameObject[] temparray = new GameObject[numberOfRopes];

        //For the whole number of ropes needed
        for (int i = 0; i < numberOfRopes; i++)
        {
            //We instantiate a piece of rope
            temparray[i] = Instantiate(_ropePrefab, spawnHere, new Quaternion());
            temparray[i].transform.Rotate(rotation);
            //We set the spawn here postion to be down the line on the displacement line
            spawnHere += rotation * heightOfRope;
            //WE check to see if there is more than 1 item in the list
            if (i > 0)
            {
                //Temp Joint Reference
                ConfigurableJoint joint;
                //getting a reference to the joint component
                joint = temparray[i].gameObject.GetComponent<ConfigurableJoint>();
                //tethering the two
                joint.connectedBody = temparray[i - 1].GetComponent<Rigidbody>();
            }
            else
            {
                ConfigurableJoint joint;
                //getting a reference to the joint component
                joint = temparray[i].gameObject.GetComponent<ConfigurableJoint>();
                //tethering the two
                joint.connectedBody = objectToTetherFrom.GetComponent<Rigidbody>();
            }
        }
        //Get a reference to the object we are tethering to
        ConfigurableJoint tempJoint = itemToTetherTo.GetComponent<ConfigurableJoint>();
        //Link it to the last rope we instantiated
        tempJoint.connectedBody = temparray[temparray.Length - 1].GetComponent<Rigidbody>();

        //Replace the old array with this new one
        _ropeLinks = temparray;
    }
}
