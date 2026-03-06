using ExpObj;
using UnityEngine;

public class ArrowTarget : MonoBehaviour
{
   public void DestroyTarget()
   {
      GetComponent<ExplosiveObject>().Explode();
      GetComponent<BrokenObject>().RandomVelocities();
   }
}
