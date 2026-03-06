using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseBehaviour : MonoBehaviour
{
    public CinemachineOrbitalFollow GameCamera;
    [SerializeField] private float cameraSensitivity;

    private float _horizontalMinCameraRange;
    private float _horizonatalMaxCamerRange;
    
    private float _verticalMinCameraRange;
    private float _verticalMaxCameraRange;
    
    
    private Vector2 _mouseInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowMouse(false);
        _horizontalMinCameraRange = GameCamera.HorizontalAxis.Range.x;
        _horizonatalMaxCamerRange = GameCamera.HorizontalAxis.Range.y;
        
        _verticalMinCameraRange =  GameCamera.VerticalAxis.Range.x;
        _verticalMaxCameraRange = GameCamera.VerticalAxis.Range.y;
    }

    void Update()
    {
        CalculateCamera();
    } 

    public void ShowMouse(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
    
    public void OnLook(InputValue value)
    {
        _mouseInput = value.Get<Vector2>();
    }
    
    public void CalculateCamera()
    {
        GameCamera.HorizontalAxis.Value += _mouseInput.x *  cameraSensitivity; 
        GameCamera.VerticalAxis.Value += _mouseInput.y *  cameraSensitivity;
        
        GameCamera.HorizontalAxis.Value = Mathf.Clamp(GameCamera.HorizontalAxis.Value, _horizontalMinCameraRange, _horizonatalMaxCamerRange);
        GameCamera.VerticalAxis.Value = Mathf.Clamp(GameCamera.VerticalAxis.Value, _verticalMinCameraRange, _verticalMaxCameraRange);
    }
    
}
