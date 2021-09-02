using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        Application.targetFrameRate = default;
    }

    private void Update()
    {
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
        _animator.SetBool("Jump", !_isGrounded);


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

        //Move
        _controller.Move((_desiredVelocity) * Time.deltaTime);
    }   
    
}