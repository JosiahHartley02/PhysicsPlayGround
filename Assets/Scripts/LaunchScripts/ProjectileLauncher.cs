using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Vector3 ForceToApply;

    private LineRenderer drawLine;
    private void Awake()
    {
        drawLine = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        drawLine.SetPosition(1, ForceToApply.normalized * 3);
    }
}
