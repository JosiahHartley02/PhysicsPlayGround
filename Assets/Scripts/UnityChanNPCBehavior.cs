using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnityChanNPCBehavior : MonoBehaviour
{
    [SerializeField]
    public bool activeAnimator = true;
    [SerializeField]
    private Transform _destination;

    [SerializeField]
    private float desiredTimeAlive;
    public float speed = 3;

    private CharacterController _controller;
    private NavMeshAgent _navigation;

    [SerializeField]
    private Animator _animator;

    private float _timeAlive;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _navigation = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _timeAlive += Time.deltaTime;
        if (_timeAlive > desiredTimeAlive)
        {
            _navigation.isStopped = true;
            activeAnimator = false;
        }
        //Set Animator enabled equal to whether or not the player is active
        _animator.enabled = activeAnimator;

        _navigation.SetDestination(_destination.position);

        if(_navigation.velocity.magnitude != 0)
            transform.forward = _navigation.velocity.normalized;

        _animator.SetFloat("Speed", _navigation.velocity.magnitude);

        if (_navigation.velocity != Vector3.zero)
        {
            transform.forward = _navigation.velocity.normalized;
        }
        else
        _animator.SetFloat("Speed", _navigation.velocity.magnitude / speed);
    }

}
