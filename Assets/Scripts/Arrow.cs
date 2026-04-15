using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowDestroyTime;
    private bool _isDestroying;

    private void Start()
    {
        Destroy(gameObject, 6f);
    }
    
    private void OnCollisionEnter(Collision collision) 
    {
        HandleHit(collision.gameObject);
    }
    
    private void HandleHit(GameObject hit)
    {
        if (_isDestroying) return;
        _isDestroying = true;

        Debug.Log("Hit: " + hit.name);

        var target = hit.GetComponent<ArrowTarget>();
        var enemy = hit.GetComponent<EnemyBehaviour>();
        if (target != null)
        {
            target.DestroyTarget();
        }
        if (enemy != null)
        {
            Debug.Log("Hitting enemy");
            enemy.EnemyDeath();
        }
        
        // else if (hit.GetComponent<EnemyBehaviour>() is EnemyBehaviour enemy)
        // {
        //     enemy.EnemyDeath();
        // }

        //StartCoroutine(DestroyArrow());
    }

    // private IEnumerator DestroyArrow()
    // {
    //     yield return new WaitForSeconds(arrowDestroyTime);
    //     Destroy(gameObject);
    // }
}