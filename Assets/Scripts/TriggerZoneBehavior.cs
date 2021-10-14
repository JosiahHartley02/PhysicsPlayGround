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

    [Tooltip("Each Text in this list will show itself")]
    [SerializeField]
    private ToggleTextBehavior[] textsToToggle;

    [Tooltip("Each Text in this list will toggle itself at a specific time, MUST USE textsToToggleTimers")]
    [SerializeField]
    private ToggleTextBehavior[] textsToToggleSmart;

    [Tooltip("Initializes a toggle time in all of the textsToToggleSmart array, MUST USE textsToToggleSmart")]
    [SerializeField]
    private float[] textsToToggleTimers;

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

                for (int i = 0; i < lifeTimersToStart.Length; i++)                {
                    lifeTimersToStart[i].ToggleOn(); 
                }
                for (int i = 0; i < textsToToggle.Length; i++)
                    textsToToggle[i].ToggleText();

                for (int i = 0; i < textsToToggleSmart.Length; i++)
                    textsToToggleSmart[i].ToggleAfter(textsToToggleTimers[i]);

                textsToToggle = new ToggleTextBehavior[0];
                textsToToggleSmart = new ToggleTextBehavior[0];
                textsToToggleTimers = new float[0];
                lifeTimersToStart = new LiveUntilBehavior[0];
            }
    }
}
