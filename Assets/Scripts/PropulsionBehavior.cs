using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsionBehavior : MonoBehaviour
{
    [SerializeField]
    public float TimeTillDestroy = 5;

    [SerializeField]
    private float TimeAlive = 0;

    private void Update()
    {
        TimeAlive += Time.deltaTime;
        if(TimeAlive > TimeTillDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
