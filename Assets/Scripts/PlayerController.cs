using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpstrength = 4.0f;
    public float gravityModifier = 1.0f;
    private CharacterController _controller;
    private Vector3 _desiredVelocity;
    private Vector3 _airVelocity;
    private bool _isJumpDesired;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Get WASD Input
        _desiredVelocity.x = Input.GetAxis("Horizontal");
        _desiredVelocity.y = 0.0f;
        _desiredVelocity.z = Input.GetAxis("Vertical");

        //Get jump input
        _isJumpDesired = Input.GetButton("Jump");

        //Normalize direction to move
        _desiredVelocity.Normalize();
        _desiredVelocity *= speed;

        //Apply jump strength
        if (_isJumpDesired)
        {
            _airVelocity.y = jumpstrength;
            _isJumpDesired = false;
        }

        //Apply gravity
        _airVelocity += Physics.gravity * gravityModifier * Time.deltaTime;

        //Apply the magnitude to the player at deltaTime
        _controller.Move((_desiredVelocity + _airVelocity) * Time.deltaTime);
    }
    
}