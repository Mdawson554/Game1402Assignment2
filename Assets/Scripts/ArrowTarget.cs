using ExpObj;
using UnityEngine;

public class ArrowTarget : MonoBehaviour
{
    [SerializeField] private ButterfliesInteractable butterflyPrefab;
    [SerializeField] Transform butterflySpawnPoint;
   
   public void DestroyTarget()
   {  
       Spawnbutterflies();
      GetComponent<ExplosiveObject>().Explode();
      GetComponent<BrokenObject>().RandomVelocities();
   }
   
   public void Spawnbutterflies()
   {
     ButterfliesInteractable butterfly =  Instantiate(butterflyPrefab);
     butterfly.transform.position = transform.localPosition;
   }
}
