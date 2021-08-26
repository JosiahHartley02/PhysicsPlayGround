using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WakeUpMaterialBehavior : MonoBehaviour
{
    public Material awakeMaterial = null;
    public Material asleepMaterial = null;

    private Rigidbody _rigidbody = null;
    private MeshRenderer _renderer = null;

    private bool _materialIsAwake = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        //Set material to asleep if rigidbody is asleep
        //check to see if the material is set to awake, if the rigidbody is sleeping, and the asleep material exists
        if(_materialIsAwake && _rigidbody.IsSleeping() && asleepMaterial)
        {
            //set the material to not be awake
            _materialIsAwake = false;
            //actually set the material to be the asleepMaterial
            _renderer.material = asleepMaterial;
        }
        //set material to awake if rigidbody is awake
        else if (!_materialIsAwake && !_rigidbody.IsSleeping() && awakeMaterial)
        {
            _materialIsAwake = true;
            _renderer.material = awakeMaterial;
        }
    }
}
