using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    //Declare a target for the projectile spawner to lob projectiles towards
    public Transform target;
    //Declare the prefab gameobject with a rigid body to lob
    public Rigidbody projectile;

    //Decide how long we want the projectile to be in the air for
    public float airTime = 2.0f;

    //Information of the direct displacement
    private Vector3 _displacement = new Vector3();
    //Information on the current acceleration of the object
    private Vector3 _acceleration = new Vector3();
    //float on the time this projectile is taking
    private float _time = 0.0f;
    //The force needed right at the start of the impulse
    private Vector3 _initialVelocity = new Vector3();
    //The force at the end of the impulse, right before impact
    private Vector3 _finalVelocity = new Vector3();

    public void LaunchProjectile()
    {
        //The displacement is easy enough to locate, it is the final position minus the current position
        _displacement = target.position - transform.position;
        //We say the acceleration is equal to the gravity
        _acceleration = Physics.gravity;
        //We set the time we want it to take to be the air to be the modifyable value
        _time = airTime;
        //We set the initial velocity to be the displacement divided by time and acceleration
        _initialVelocity = FindInitialVelocity(_displacement, _acceleration, _time);
        //We set the final velocity to be the velocity at the end of the parabola
        _finalVelocity = FindFinalVelocity(_initialVelocity, _acceleration, _time);

        //We instantiate a new projectile at the location of this instance
        Rigidbody projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
        //We apply the force needed to get the gameObject to the final destination
        projectileInstance.AddForce(_initialVelocity, ForceMode.VelocityChange);
    }

    public Vector3 FindFinalVelocity(Vector3 initialVelocity, Vector3 acceleration, float time)
    {
        //v = v0 + at
        Vector3 finalVelocity = initialVelocity + acceleration * time;

        return finalVelocity;
    }

    private Vector3 FindDisplacement(Vector3 initialVelocity, Vector3 acceleration, float time)
    {
        //▲X = v-*t + (1/2)*a*t^2
        Vector3 displacement = initialVelocity * time + (1 / 2) * acceleration * time * time;
        return displacement;
    }

    private Vector3 FindInitialVelocity(Vector3 displacement, Vector3 acceleration, float time)
    {
        //▲X = v0*t + (1/2)*a*t^2
        //▲X - (1/2)*a*t^2 = v0*t
        //▲X/t - (1/2)*a*t = v0
        //v0 = ▲X/t -(1/2)*a*t

        Vector3 initialVelocity = displacement / time - 0.5f * acceleration * time;

        return initialVelocity;
    }
}
