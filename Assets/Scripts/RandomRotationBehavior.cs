using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationBehavior : MonoBehaviour
{
   [Tooltip("The minimum possible rotational values this object could recieve")]
    [SerializeField]
    private Vector3 randomRotationMins;

    [Tooltip("The maximum possible rotational values this object could recieve")]
    [SerializeField]
    private Vector3 randomRotationMaxs;

    [Tooltip("The minimum possible distance this object can move")]
    [SerializeField]
    private Vector3 randomMoveMins;

    [Tooltip("The maximum possible distance this object can move")]
    [SerializeField]
    private Vector3 randomMoveMaxs;

    //This is the only necessary function
    private void Start()
    {
        //Apply the desired rotational values
        transform.Rotate(
            Random.Range(randomRotationMins.x, randomRotationMaxs.x),
            Random.Range(randomRotationMins.y, randomRotationMaxs.y),
            Random.Range(randomRotationMins.z, randomRotationMaxs.z));
        //Apply the desired positional values
        transform.position += new Vector3(
            Random.Range(randomMoveMins.x, randomMoveMaxs.x),
            Random.Range(randomMoveMins.y, randomMoveMaxs.y),
            Random.Range(randomMoveMins.z, randomMoveMaxs.z));
    }
}
