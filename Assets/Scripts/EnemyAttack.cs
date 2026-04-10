using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int enemyDamage;
    [SerializeField] private float coolDownTime;

    private EnemyBehaviour enemyBehaviour;
    private bool _canDamage = true;
    
    private void Awake()
    {
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        enemyBehaviour.SetState(EnemyState.ATTACK); 
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        enemyBehaviour.SetState(EnemyState.CHASE);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!_canDamage) return;

        StartCoroutine(DamageCooldown());
        GameManager.Instance.LooseHealth(enemyDamage);
    }
 
    private IEnumerator DamageCooldown()
    {
        _canDamage = false;
        yield return new WaitForSeconds(coolDownTime); 
        _canDamage = true;
    }
}
