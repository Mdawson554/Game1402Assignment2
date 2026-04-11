using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowDestroyTime;
    private bool _isDestroying;
    
    private void OnCollisionEnter(Collision collision) //bottle
    {
        HandleHit(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other) //enemy
    {
        HandleHit(other.gameObject);
    }
    
    private void HandleHit(GameObject hit)
    {
        if (_isDestroying) return;
        _isDestroying = true;

        Debug.Log("Hit: " + hit.name);

        var target = hit.GetComponentInParent<ArrowTarget>();
        if (target != null)
        {
            target.DestroyTarget();
        }
        else if (hit.GetComponentInParent<EnemyBehaviour>() is EnemyBehaviour enemy)
        {
            enemy.EnemyDeath();
        }

        StartCoroutine(DestroyArrow());
    }

    private IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(arrowDestroyTime);
        Destroy(gameObject);
    }
}