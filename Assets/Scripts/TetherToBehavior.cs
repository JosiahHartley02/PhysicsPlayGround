using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherToBehavior : MonoBehaviour
{
    //The start of the rope linkage
    public GameObject objectToTetherFrom;
    //The end of the rope linkage
    public GameObject objectToTetherTo;

    //What object we are using as a rope link
    [SerializeField]
    private GameObject _ropePrefab;

    //Bool to decide whether or not the rope should form on start
    [SerializeField]
    private bool _tetherOnStart = false;

    //GameObject array of all the rope links
    private GameObject[] _ropeLinks = new GameObject[0];

    private void Start()
    {
        //If we tether on start then tether on start
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
        float numberOfRopesTotal = (distance / heightOfRope);
        //Convert to a whole number
        int numberOfRopes = Mathf.RoundToInt(numberOfRopesTotal);
        //We hold a spawn position for each rope we want to instantiate
        Vector3 spawnHere = objectToTetherFrom.transform.position;

        //Create a temp array to replace the current array
        GameObject[] temparray = new GameObject[numberOfRopes];

        //For the whole number of ropes needed
        for (int i = 0; i < numberOfRopes; i++)
        {
            //We instantiate a piece of rope
            temparray[i] = Instantiate(_ropePrefab, spawnHere, new Quaternion(),this.transform);
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

        //Get a reference to the last rope and give it a hinge joint
        HingeJoint lastJoint = temparray[temparray.Length - 1].AddComponent<HingeJoint>();
        //get a reference to the rigid body of the last item after the rope
        Rigidbody rigid = itemToTetherTo.GetComponent<Rigidbody>();
        //Connect the last rope hinge to the last rope end
        lastJoint.connectedBody = rigid;
        //Set the position of the hinge
        lastJoint.connectedAnchor = rigid.position;
        //Replace the old array with this new one
        _ropeLinks = temparray;
    }
}
