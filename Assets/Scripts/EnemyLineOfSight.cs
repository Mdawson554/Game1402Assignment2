using System;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    [SerializeField] private float lineOfSight;
    [SerializeField] private float sightRadius;
    [SerializeField] private Transform sightPosition;
    [SerializeField] private LayerMask detectionMask;
   
    public bool IsDetected;
    
    private void FixedUpdate()
    {
        Ray ray = new Ray(sightPosition.position, transform.forward);

        if (Physics.SphereCast(ray, sightRadius, out RaycastHit hit, lineOfSight, detectionMask))
        {
            IsDetected = hit.transform.CompareTag("Player");
        }
        else
        {
            IsDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(sightPosition.position, transform.forward * lineOfSight, Color.red);
        Gizmos.DrawWireSphere(sightPosition.position + transform.forward * lineOfSight, sightRadius);
    }
}
