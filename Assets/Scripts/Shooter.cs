using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private InputAction shootInput;
    [SerializeField] private InputAction aimInput;
    
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject shootObject;
    [SerializeField] private float shootForce;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private GameObject shooterCamera;
    [SerializeField] private GameObject orbitalCamera;
    private GameObject _arrow;
    
    private bool canShoot = true;
    
    void OnEnable()
    {
        shootInput.Enable();
        shootInput.canceled += Shoot;
        
        aimInput.Enable();
        aimInput.performed += AimWeapon;
    }

    void OnDisable()
    {
        shootInput.canceled -= Shoot;
        aimInput.performed -= AimWeapon;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (canShoot)
        {
            //create a new arrow
            _arrow = Instantiate(shootObject, shootPoint.position, shootPoint.rotation);

            // apply a force
            _arrow.GetComponent<Rigidbody>().AddForce(shootForce * shootPoint.forward);
        }
        canShoot = false;
    }

    private void AimWeapon(InputAction.CallbackContext context)
    {
       shooterCamera.SetActive(true);
       orbitalCamera.SetActive(false);
    }

    private IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(reloadSpeed);
        canShoot =  true;
        yield return  null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DestroyArrow());
    }
}
