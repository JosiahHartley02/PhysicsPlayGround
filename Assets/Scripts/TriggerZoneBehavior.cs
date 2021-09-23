using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneBehavior : MonoBehaviour
{
    [Tooltip("Each cannon in this list will toggle thier lauch once")]
    [SerializeField]
    private CannonBehavior[] cannonsToTrigger;

    [Tooltip("Each Npc in this list will begin to travel")]
    [SerializeField]
    private UnityChanNPCBehavior[] unityChanNpcsToTrigger;

    [Tooltip("Each jet in this list will begin to travel")]
    [SerializeField]
    private JetBehavior[] jetsToTrigger;

    [Tooltip("Each lifeTimer in this list will begin")]
    [SerializeField]
    private LiveUntilBehavior[] lifeTimersToStart;

    private void OnTriggerEnter(Collider other)
    {
        //Create a temp rigidBody value to hold the info of the gameObject that collided
        Rigidbody temp;


        //Check to see if the other collider is attached to a character controller and that the controller belongs to the player
        if (other.TryGetComponent<Rigidbody>(out temp) && (other.tag == "Player" || other.tag == "Vehicle"))
            if (temp != null)
            {
                for (int i = 0; i < cannonsToTrigger.Length; i++)
                    cannonsToTrigger[i].ToggleLaunch();

                for (int i = 0; i < unityChanNpcsToTrigger.Length; i++)
                    unityChanNpcsToTrigger[i].shouldTravel = true;

                for (int i = 0; i < jetsToTrigger.Length; i++)
                    jetsToTrigger[i].npc = true;

                for (int i = 0; i < lifeTimersToStart.Length; i++)
                    lifeTimersToStart[i].ToggleOn();
            }
    }
}
