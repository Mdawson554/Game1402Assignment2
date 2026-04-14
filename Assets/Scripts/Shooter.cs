using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private InputAction shootInput;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject shootObject;
    [SerializeField] private float shootForce;
    [SerializeField] private float reloadTime;
    [SerializeField] private AudioClip bowRelease;
    [SerializeField] private AudioClip bowLoading;
    [SerializeField] private Transform aimtrack;
    
    [SerializeField] private Camera playerCamera;
    private PlayerController _playerController;
    private AudioManager audioManager;
    private bool _canShoot = true;
    private GameObject _arrow;
    private PlayerState _currentState;
    
    private void OnEnable()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.OnStateUpdated += StateUpdate;
        shootInput.Enable();
        shootInput.performed += Shoot;
    }

    private void StateUpdate(PlayerState state)
    {
        _currentState = state;
    }
    
    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    private void OnDisable()
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
        else if (_canShoot && InventoryManager.Instance.currentArrows > 0)
        {
            Ray ray =playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Vector3 targetPoint;

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(100f);
            }
            //calculate the direction
            Vector3 shootDirection = (targetPoint - shootPoint.position).normalized;
            
            //create a new arrow
            _arrow = Instantiate(shootObject, shootPoint.position, Quaternion.LookRotation(shootDirection));

            // apply a force
            audioManager.PlaySound(bowRelease);
            _arrow.GetComponent<Rigidbody>().AddForce(shootForce * shootDirection, ForceMode.Impulse);
            GameManager.Instance.ShootArrow();
            _canShoot = false;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        _canShoot =  true;
    }
}
