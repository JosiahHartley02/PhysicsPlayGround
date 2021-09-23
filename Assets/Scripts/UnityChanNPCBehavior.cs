using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnityChanNPCBehavior : MonoBehaviour
{
    [Tooltip("Whether or not that animator is animating TIP: WHEN OFF -> RAGDOLL")]
    [SerializeField]
    public bool activeAnimator = true;

    [Tooltip("The location of where the npc wants to travel to")]
    [SerializeField]
    private Transform _destination;
    //Bool to determine whether or not the npc should be traveling
    public bool shouldTravel = false;

    [SerializeField]
    private float desiredTimeAlive;
    private float _timeAlive;

    //Speed of the NPC
    public float speed = 3;

    //Reference to the character controller being used to control this npc
    private CharacterController _controller;
    //Navmesh reference to the character desired path of travel
    private NavMeshAgent _navigation;

    //Reference to the animator that is making the NPC not ragdoll
    [SerializeField]
    private Animator _animator;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _navigation = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_timeAlive > desiredTimeAlive)
        {
            _navigation.isStopped = true;
            activeAnimator = false;
        }

        //Set Animator enabled equal to whether or not the player is active
        _animator.enabled = activeAnimator;

        //If the Npc should not travel
        if (shouldTravel)
        {
            //We say the NPC should navigate to this position
            _navigation.SetDestination(_destination.position);
            _timeAlive += Time.deltaTime;
        }

        if (_navigation.velocity.magnitude != 0)
            transform.forward = _navigation.velocity.normalized;

        _animator.SetFloat("Speed", _navigation.velocity.magnitude);

        //if the npcs velocity is not equal to 0
        if (_navigation.velocity != Vector3.zero)
        {
            //Then set the forward to be the velocitys forward
            transform.forward = _navigation.velocity.normalized;
        }
        else
            //set the animators speed float
        _animator.SetFloat("Speed", _navigation.velocity.magnitude / speed);
    }

    //Set the animator and navigator to be disabled
    public void Stop()
    {
        //Stop the navigator
        _navigation.isStopped = true;
        //Stop the animator
        activeAnimator = false;
    }
}
