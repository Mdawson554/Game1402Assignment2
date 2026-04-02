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

        if (Physics.SphereCast( new Ray(sightPosition.position, Vector3.forward) ,sightRadius, out RaycastHit hit, lineOfSight))
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
        Gizmos.DrawWireSphere(new Vector3(sightPosition.position.x, sightPosition.position.y,sightPosition.position.z + lineOfSight) , sightRadius);
    }
}
