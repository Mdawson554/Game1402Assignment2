using System;
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
    [SerializeField] private AudioClip bowRelease;
    [SerializeField] private AudioClip bowLoading;
    
    private PlayerController _playerController;
    private AudioManager audioManager;
    private bool canShoot = true;
    private GameObject _arrow;
    private bool _isAiming = false;
    
    //private Vector3 _shootdirection;
    //private PlayerState _currentState;
    
    void OnEnable()
    {
        //_playerController.OnStateUpdated += (state) = _currentState = state;
        
        if(_playerController == null)
            _playerController = GetComponent<PlayerController>();
        shootInput.Enable();
        shootInput.performed += Shoot;
        aimInput.Enable();
        aimInput.performed += AimWeapon;
    }

    /*void StateUpdate(Playerstate state)
    {
        _currentState = state;
    }
    */

    private void Start()
    {
        audioManager = AudioManager.Instance;
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
            //calculate the direction
            //_shootdirection = aimtrack.position - shootpPoint.position;
            
            //create a new arrow
            _arrow = Instantiate(shootObject, shootPoint.position, /* quaternion.LookRoatation(_shootDirection*/shootPoint.rotation);

            // apply a force
            audioManager.PlaySound(bowRelease);
            _arrow.GetComponent<Rigidbody>().AddForce(shootForce * /*_shootDirection, ForceMode.Impulse*/ shootPoint.forward);
            GameManager.Instance.ShootArrow();
        }
        canShoot = false;
        StartCoroutine(Reload());
        
        //if(_currentState !=  PlayerState.AIM) return;
    }

    private void AimWeapon(InputAction.CallbackContext context)
    {
        audioManager.PlaySound(bowLoading);
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
