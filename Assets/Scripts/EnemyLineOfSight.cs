using System;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    [SerializeField] private float lineOfSight;
    [SerializeField] private float sightRadius;
    [SerializeField] private Transform sightPosition;
    public bool IsDetected;
    
    private void Update()
    {
        IsDetected = false;
        if (Physics.SphereCast( new Ray(sightPosition.position, transform.forward) ,sightRadius, out RaycastHit hit, lineOfSight))
        {
            if (hit.transform.CompareTag("Player"))
            {
                IsDetected = true;
            }
            else 
            {
                IsDetected = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(sightPosition.position, transform.TransformDirection(Vector3.forward) * lineOfSight, Color.red);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sightPosition.position + transform.forward * lineOfSight, sightRadius);
    }
}
