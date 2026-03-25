using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseBehaviour : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private CinemachineOrbitalFollow gameCamera;
    private float _horizontalMinCameraRange;
    private float _horizontalMaxCameraRange;
    private float _verticalMinCameraRange;
    private float _verticalMaxCameraRange;
    private Vector2 _mouseInput;
    
    private PlayerController _playerController;
    private PlayerState _currentState;

    private void OnEnable()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.OnStateUpdated += StateUpdate;
    }

    private void OnDisable()
    {
        _playerController.OnStateUpdated -= StateUpdate;
    }
    private void StateUpdate(PlayerState state)
    {
        _currentState = state;
        if (state == PlayerState.EXPLORE)
        {
            _mouseInput = Vector2.zero;
        }
    }
    
    private void Start()
    {
        _horizontalMinCameraRange = gameCamera.HorizontalAxis.Range.x;
        _horizontalMaxCameraRange = gameCamera.HorizontalAxis.Range.y;
        
        _verticalMinCameraRange =  gameCamera.VerticalAxis.Range.x;
        _verticalMaxCameraRange = gameCamera.VerticalAxis.Range.y;
    }

    private void Update()
    {
        if (_currentState == PlayerState.EXPLORE)
        {
            CalculateCamera();
        }
    } 
    
    public void OnLook(InputValue value)
    {
        _mouseInput = value.Get<Vector2>();
    }
    
    private void CalculateCamera()
    {
        gameCamera.HorizontalAxis.Value += _mouseInput.x *  cameraSensitivity * Time.deltaTime; 
        gameCamera.VerticalAxis.Value += _mouseInput.y *  cameraSensitivity  * Time.deltaTime;
        
        gameCamera.HorizontalAxis.Value = Mathf.Clamp(gameCamera.HorizontalAxis.Value, _horizontalMinCameraRange, _horizontalMaxCameraRange);
        gameCamera.VerticalAxis.Value = Mathf.Clamp(gameCamera.VerticalAxis.Value, _verticalMinCameraRange, _verticalMaxCameraRange);
    }
    
}
