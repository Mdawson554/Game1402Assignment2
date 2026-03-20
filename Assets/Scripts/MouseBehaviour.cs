using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseBehaviour : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity;
    public CinemachineOrbitalFollow GameCamera;
    private float _horizontalMinCameraRange;
    private float _horizonatalMaxCamerRange;
    private float _verticalMinCameraRange;
    private float _verticalMaxCameraRange;
    private Vector2 _mouseInput;
    
    private PlayerController _playerController;
    private PlayerState _currentState;

    void OnEnable()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.OnStateUpdated += StateUpdate;
    }

    void OnDisable()
    {
        _playerController.OnStateUpdated -= StateUpdate;
    }
    void StateUpdate(PlayerState state)
    {
        _currentState = state;
    }
    
    void Start()
    {
        _horizontalMinCameraRange = GameCamera.HorizontalAxis.Range.x;
        _horizonatalMaxCamerRange = GameCamera.HorizontalAxis.Range.y;
        
        _verticalMinCameraRange =  GameCamera.VerticalAxis.Range.x;
        _verticalMaxCameraRange = GameCamera.VerticalAxis.Range.y;
    }

    void Update()
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
    
    public void CalculateCamera()
    {
        GameCamera.HorizontalAxis.Value += _mouseInput.x *  cameraSensitivity * Time.deltaTime; 
        GameCamera.VerticalAxis.Value += _mouseInput.y *  cameraSensitivity  * Time.deltaTime;
        
        GameCamera.HorizontalAxis.Value = Mathf.Clamp(GameCamera.HorizontalAxis.Value, _horizontalMinCameraRange, _horizonatalMaxCamerRange);
        GameCamera.VerticalAxis.Value = Mathf.Clamp(GameCamera.VerticalAxis.Value, _verticalMinCameraRange, _verticalMaxCameraRange);
    }
    
}
