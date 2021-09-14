using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehavior : MonoBehaviour
{
    [SerializeField]
    private ProjectileLauncher launcher;
    private void Start()
    {
        launcher.LaunchProjectile();   
    }
}
