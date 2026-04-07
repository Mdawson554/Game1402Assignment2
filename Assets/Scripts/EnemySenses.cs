using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private float giveUpDistance;
    [SerializeField] private float chaseCheckAngle;
    [SerializeField] private Transform playerTarget;

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTarget.position) <= detectionRange;
    }

    public bool IsPlayerEvaded()
    {
        return Vector3.Distance(transform.position, playerTarget.position) >= giveUpDistance;
    }

    public bool IsInFOV()
    {
        Vector3 directionToPlayer = (playerTarget.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, directionToPlayer) <= chaseCheckAngle;
    }
}
