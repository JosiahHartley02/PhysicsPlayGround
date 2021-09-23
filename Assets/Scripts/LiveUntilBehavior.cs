using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveUntilBehavior : MonoBehaviour
{
    //Time in seconds that the gameObject attached should live
    [SerializeField]
    public float m_timeTillDestroy = 10;

    //The current time in seconds that the gameObject has existed
    [SerializeField]
    private float m_timeAlive = 0;

    //Bool to note whether or not to despawn on explosion
    [SerializeField]
    private bool m_despawnOnExplode = true;

    //Bool to note whether or not to have the count-Up active
    [SerializeField]
    private bool m_active = false;

    private void Update()
    {
        //Check to see if this gameObject is even active to start the count-up
        if (!m_active)
            return;

        //Update the count-Up timer
        m_timeAlive += Time.deltaTime;

        //Check to see if the count up Timer has exceeded the desired time alive
        if(m_timeAlive > m_timeTillDestroy)
        {
            //If it has then this gameObject should despawn
            Destroy(this.gameObject);
        }
    }

    public void Toggle()
    {
        //Toggle the activity
        m_active = !m_active;
    }
    public void ToggleOn()
    {
        //Set the activity on
        m_active = true;
    }

    //Toggles activity on and set the length of time
    public void Activate(int min, int max)
    {

        m_active = true;
        //Set the timer to be 100x stronger to divide later
        m_timeTillDestroy = Random.Range(min*100, max*100);
        //Dividing here gives us a random float rather than a random int
        m_timeTillDestroy /= 100;
    }

    public void ToggleDespawn()
    {
        //Despawn on explosion
        m_despawnOnExplode = !m_despawnOnExplode;
    }

    public bool ShouldExplode()
    {
        //Should this despawn on explosion
        return m_despawnOnExplode;
    }
}
