using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   [SerializeField] private float arrowDestroyTime;
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
}
