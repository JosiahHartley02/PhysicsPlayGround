using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_spawnPosition;
    [SerializeField]
    private Quaternion m_spawnRotation;

    private void Start()
    {
        m_spawnPosition = this.transform.position;
        m_spawnRotation = this.transform.rotation;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            transform.position = m_spawnPosition;
            transform.rotation = m_spawnRotation;
        }
    }
}
