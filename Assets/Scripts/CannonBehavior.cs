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
    //We need a bool to determine whether or not this has been launched
    private bool _launched = false;

    public void ToggleLaunch()
    {
        //On toggle we check to see if it was launched
        if (!_launched)
        {
            //If not then we want to launch
            _launch = true;
        }

        //If we want to launch and it has not been launched
        if (_launch && !_launched)
        {
            //We can launch
            launcher.LaunchProjectile();
            //We can say this has been launched
            _launched = true;
        }
    }
}
