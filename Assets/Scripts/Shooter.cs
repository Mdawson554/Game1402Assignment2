using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private InputAction shootInput;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject shootObject;
    [SerializeField] private float shootForce;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private CinemachineOrbitalFollow orbitalCamera;
    [SerializeField]private MouseBehaviour mouseBehaviour;
    [SerializeField] private AudioClip bowRelease;
    [SerializeField] private AudioClip bowLoading;
    [SerializeField] private Transform aimtrack;
    private PlayerController _playerController;
    private AudioManager audioManager;
    private bool canShoot = true;
    private GameObject _arrow;
    private PlayerState _currentState;
    
    void OnEnable()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.OnStateUpdated += StateUpdate;
        shootInput.Enable();
        shootInput.performed += Shoot;
    }

    void StateUpdate(PlayerState state)
    {
        _currentState = state;
    }
    
    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    void OnDisable()
    {
        _playerController.OnStateUpdated -= StateUpdate;
        shootInput.performed -= Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (_currentState != PlayerState.AIM)
        {
            return;
        }
        else if (canShoot && GameManager.Instance.CurrentArrows > 0)
        {
            //calculate the direction
            Vector3 _shootDirection = aimtrack.position - shootPoint.position;
            
            //create a new arrow
            _arrow = Instantiate(shootObject, shootPoint.position, Quaternion.LookRotation(_shootDirection));

            // apply a force
            audioManager.PlaySound(bowRelease);
            _arrow.GetComponent<Rigidbody>().AddForce(shootForce * _shootDirection, ForceMode.Impulse);
            GameManager.Instance.ShootArrow();
        }
        canShoot = false;
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        canShoot =  true;
        yield return  null;
    }
}
