using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneBehavior : MonoBehaviour
{
    [SerializeField]
    private CannonBehavior[] cannonsToTrigger;
    [SerializeField]
    private UnityChanNPCBehavior[] unityChanNpcsToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        //Create a temp rigidBody value
        Rigidbody temp;
        //Check to see if the other collider is attached to a character controller and the controller belongs to the player
        if (other.TryGetComponent<Rigidbody>(out temp) && other.tag == "Player")
            if (temp != null)
            {
                for (int i = 0; i < cannonsToTrigger.Length; i++)
                    cannonsToTrigger[i].ToggleLaunch();
                for (int i = 0; i < unityChanNpcsToTrigger.Length; i++)
                    unityChanNpcsToTrigger[i].shouldTravel = true;
            }
    }
}
