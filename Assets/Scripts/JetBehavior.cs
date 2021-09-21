using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBehavior : MonoBehaviour
{
    public bool activePlayer;

    [SerializeField]
    private ProjectileLauncher[] thruster;

    [SerializeField]
    public bool npc = false;
    private Rigidbody bodyreference;
    [SerializeField]
    private Rigidbody topWing;
    [SerializeField]
    private Rigidbody bottomWing;

    [SerializeField]
    private GameObject _propulsionDirection;

    private Quaternion desiredRotation;
    private void Start()
    {
        bodyreference = GetComponent<Rigidbody>();
        desiredRotation = bodyreference.rotation;
    }
    private void Update()
    {
        if ((Input.GetButton("Jump") && activePlayer) || npc)
            for(int i = 0; i < thruster.Length; i++)
                thruster[i].LaunchProjectile();

        if (!activePlayer && !npc)
            return;

        float InputRight = Input.GetAxis("Horizontal") * 0.10f;
        float InputUp = Input.GetAxis("Vertical") * 0.10f;

        bodyreference.MoveRotation(desiredRotation);
        topWing.MoveRotation(desiredRotation);
        bottomWing.MoveRotation(desiredRotation);
    }
}
