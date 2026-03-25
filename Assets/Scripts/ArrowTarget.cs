using ExpObj;
using UnityEngine;

public class ArrowTarget : MonoBehaviour
{
    [SerializeField] private ButterfliesInteractable butterflyPrefab;
    [SerializeField] private Transform butterflySpawnPoint;
   
   public void DestroyTarget()
   {
       SpawnButterflies();
       GetComponent<ExplosiveObject>().Explode();
       GetComponent<BrokenObject>().RandomVelocities();
   }
   
   private void SpawnButterflies()
   {
       Instantiate(butterflyPrefab, butterflySpawnPoint.position, Quaternion.identity);
   }
}
