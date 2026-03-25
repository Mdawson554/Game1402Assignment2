using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   [SerializeField] private float arrowDestroyTime;
   private bool _isDestroying;

   private void OnCollisionEnter(Collision collision)
   {
      if (_isDestroying) return;
      _isDestroying = true;
      
      if (collision.gameObject.GetComponent<ArrowTarget>())
      {
         Debug.Log("ArrowTarget destroyed");
         collision.gameObject.GetComponent<ArrowTarget>().DestroyTarget();
      }
      StartCoroutine(DestroyArrow());
   }
   
   private IEnumerator DestroyArrow()
   {
      yield return new WaitForSeconds(arrowDestroyTime);
      Destroy(gameObject);
   }
}
