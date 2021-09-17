using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehavior : MonoBehaviour
{
    //We need a reference to the launch behavior
    [SerializeField]
    private ProjectileLauncher launcher;

    //We need a bool to determine whether or not to launch
    [SerializeField]
    private bool _launch = false;
    private bool _launched = false;

    public void ToggleLaunch()
    {
        if (!_launched)
        {
            _launch = true;
        }


        if (_launch && !_launched)
        {
            launcher.LaunchProjectile();
            _launched = true;
        }
    }
}
