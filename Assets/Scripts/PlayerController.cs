using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool activePlayer = true;

    public float speed = 5;
    public float jumpstrength = 7.0f;
    public float airControl = 1.0f;
    public float gravityModifier = 1.0f;
    public bool faceWithCamera = true;

    public Camera playerCamera;

    private CharacterController _controller;

    [SerializeField]
    private Animator _animator;

    private Vector3 _desiredVelocity;
    private Vector3 _airVelocity;
    private bool _isJumpDesired;
    private bool _isGrounded = false;

    private TetherToBehavior _tetherBehavior;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _tetherBehavior = GetComponent<TetherToBehavior>();
    }

    private void Update()
    {
        //Set Animator enabled equal to whether or not the player is active
        _animator.enabled = activePlayer;
        //if the animator is not enabled then dont update
        if (!_animator.enabled)
            return;

        //if the player is left clicking but not moving the camera
        ActivePlayerSwitch();
        //Get WASD Input
        float InputRight = Input.GetAxis("Horizontal");
        float InputForward = Input.GetAxis("Vertical");

        //Get camera forward
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        //Get Camera Right
        Vector3 cameraRight = playerCamera.transform.right;

        //Find the desired Velocity
        _desiredVelocity = ((InputRight * cameraRight) + (InputForward * cameraForward));

        //Get jump input
        _isJumpDesired = Input.GetButton("Jump");

        //Set movement magnitude
        _desiredVelocity.Normalize();
        _desiredVelocity *= speed;

        //Check for ground
        _isGrounded = _controller.isGrounded;

        //Update Animations
        faceWithCamera = Input.GetMouseButton(0);
        if (faceWithCamera)
        {
            transform.forward = cameraForward;
            _animator.SetFloat("Speed", InputForward);
            _animator.SetFloat("Direction", InputRight);
        }
        else
        {
            if (_desiredVelocity != Vector3.zero)
            {
                transform.forward = _desiredVelocity.normalized;
            }
            _animator.SetFloat("Speed", _desiredVelocity.magnitude / speed);
        }


        //Apply jump strength
        if (_isJumpDesired && _isGrounded)
        {
            _airVelocity.y = jumpstrength;
            _isJumpDesired = false;
        }

        //Stop on ground
        if (_isGrounded && _airVelocity.y <= 0.0f)
        {
            _airVelocity.y = -1.0f;
        }

        //Apply gravity
        _airVelocity += Physics.gravity * gravityModifier * Time.deltaTime;

        //Apply air velocity
        _desiredVelocity += _airVelocity;

        _animator.SetBool("Jump", !_isGrounded);
        _animator.SetFloat("VerticalSpeed", _airVelocity.y / jumpstrength);

        //Move
        _controller.Move((_desiredVelocity) * Time.deltaTime);
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
            if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.gameObject.tag == "Vehicle")
            {
                //If it is then this is no longer the active player
                activePlayer = false;
                //Now we get a reference to the switch script embedded in each collider possesing game object of the player
                ActivePlayerSwitch referenceToPlayer = hitInfo.transform.gameObject.GetComponent<ActivePlayerSwitch>();
                //The script holds a reference to the player game object we need to control
                GameObject player = referenceToPlayer.referenceToPlayer;
                //now we get the player controller script from the game object
                VehicleBehavior controller = player.GetComponent<VehicleBehavior>();
                //Now we can set the player to be the active player
                controller.activePlayer = true;
            }
        }
    }
}