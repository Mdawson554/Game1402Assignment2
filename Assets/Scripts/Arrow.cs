using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   [SerializeField] private float arrowDestroyTime;

   private Rigidbody _rb;

   void start()
   {
      _rb = GetComponent<Rigidbody>();
      //Invoke(nameof(DestroyAfter), 5f);
      

   }

   void FixedUpdate()
   {
      //_rb.rotation = quaternion.LookRotation(_rb.linearVelocity);
   }
   
   private void OnCollisionEnter(Collision collision)
   {
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
      yield return  null;
   }
   
   /*void DestroyAfter()
   {
      Destroy(gameObject)
   }
   */
}
