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
    [SerializeField] private Transform aimCamera;
    [SerializeField] private CinemachineOrbitalFollow orbitalCamera;
    [SerializeField]private MouseBehaviour mouseBehaviour;
    
    private PlayerController _playerController;
    private bool canShoot = true;
    private GameObject _arrow;
    private bool _isAiming = false;
    void OnEnable()
    {
        if(_playerController == null)
            _playerController = GetComponent<PlayerController>();
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
        _isAiming = !_isAiming;
        _playerController.IsAiming = _isAiming;
       aimCamera.gameObject.SetActive(_isAiming);
       orbitalCamera.gameObject.SetActive(!_isAiming);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        canShoot =  true;
        yield return  null;
    }
}
