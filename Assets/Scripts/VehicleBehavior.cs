using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    //Bool to determine whether or not to update this script
    [SerializeField]
    public bool activePlayer = false;

    [SerializeField]
    private ProjectileLauncher launcher;

    //Reference to the Front Axel Acting as a single wheel
    [SerializeField]
    private SingleWheelVehicleBehavior frontAxel;
    //Placeholder Reference to a higher level joint to simulate vehicle turning
    private ConfigurableJoint frontJoint = new ConfigurableJoint();
    //Reference to the Back Axel Acting as a single Wheel
    [SerializeField]
    private SingleWheelVehicleBehavior rearAxel;

    [SerializeField]
    private Quaternion _restingPosition = new Quaternion();

    private void Awake()
    {
        //Get reference to the higher level joint to simulate vehicle turning
        frontJoint = frontAxel.GetComponent<ConfigurableJoint>();
    }
    private void Update()
    {
        //Test to see if this vehicle is the active player
        if (!activePlayer)
            //If not then dont bother updating the script
            return;

        //If it is Active, then we want to get the players input at the start of the frame
        float InputForward = Input.GetAxis("Vertical");
        float InputRight = Input.GetAxis("Horizontal");
        bool Fire = Input.GetButtonDown("Jump");

        if (Fire)
            launcher.LaunchProjectile();

        //Now we can use the single wheel behaviors update desired velocity function
        frontAxel.UpdateDesiredVelocity(InputForward * 10);
        rearAxel.UpdateDesiredVelocity(InputForward * 10);

        //Here we manually rotate the front axel on a free rotational axis from the higher level joint

        ActivePlayerSwitch();
    }

    private void ActivePlayerSwitch()
    {
        //We check to see if the user clicked
        if (Input.GetKeyDown(KeyCode.F))
        {
            //IF so then we cast a ray to see what the user clicked on
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //We hold a reference to the collider that the mouse interacted with
            RaycastHit hitInfo;

            //We check the ray to see what collider the user clicked on and check to see if its the player
            if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.gameObject.tag == "Player")
            {
                //If it is then this is no longer the active player
                activePlayer = false;
                //Now we get a reference to the switch script embedded in each collider possesing game object of the player
                ActivePlayerSwitch referenceToPlayer = hitInfo.transform.gameObject.GetComponent<ActivePlayerSwitch>();
                //The script holds a reference to the player game object we need to control
                GameObject player = referenceToPlayer.referenceToPlayer;
                //now we get the player controller script from the game object
                PlayerController controller = player.GetComponent<PlayerController>();
                //Now we can set the player to be the active player
                controller.activePlayer = true;

                //We can also set the player position to be at the world location of the gameobject selected to avoid teleportation
                player.transform.position = hitInfo.transform.gameObject.transform.position;
            }
        }
    }
}
