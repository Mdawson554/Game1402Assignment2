using System.Collections;
using Unity.Cinemachine;
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
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private CinemachineOrbitalFollow orbitalCamera;
    [SerializeField]private MouseBehaviour mouseBehaviour;
    
    private bool canShoot = true;
    private GameObject _arrow;
    void OnEnable()
    {
        shootInput.Enable();
        shootInput.performed += Shoot;
        
        aimInput.Enable();
        aimInput.performed += AimWeapon;
    }

    void OnDisable()
    {
        shootInput.performed -= Shoot;
        aimInput.performed -= AimWeapon;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (canShoot && GameManager.Instance.CurrentArrows > 0)
        {
            //create a new arrow
            _arrow = Instantiate(shootObject, shootPoint.position, shootPoint.rotation);

            // apply a force
            _arrow.GetComponent<Rigidbody>().AddForce(shootForce * shootPoint.forward);
            
            GameManager.Instance.ShootArrow();
        }
        canShoot = false;
        StartCoroutine(Reload());
    }

    private void AimWeapon(InputAction.CallbackContext context)
    {
      // aimCamera.gameObject.SetActive(true);
       //orbitalCamera.gameObject.SetActive(false);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        canShoot =  true;
        yield return  null;
    }
}
