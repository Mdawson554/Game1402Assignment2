using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseBehaviour : MonoBehaviour
{
    [SerializeField] private CinemachineOrbitalFollow gameCamera;
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
        _horizontalMinCameraRange = gameCamera.HorizontalAxis.Range.x;
        _horizonatalMaxCamerRange = gameCamera.HorizontalAxis.Range.y;
        
        _verticalMinCameraRange =  gameCamera.VerticalAxis.Range.x;
        _verticalMaxCameraRange = gameCamera.VerticalAxis.Range.y;
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
        gameCamera.HorizontalAxis.Value += _mouseInput.x *  cameraSensitivity; 
        gameCamera.VerticalAxis.Value += _mouseInput.y *  cameraSensitivity;
        
        gameCamera.HorizontalAxis.Value = Mathf.Clamp(gameCamera.HorizontalAxis.Value, _horizontalMinCameraRange, _horizonatalMaxCamerRange);
        gameCamera.VerticalAxis.Value = Mathf.Clamp(gameCamera.VerticalAxis.Value, _verticalMinCameraRange, _verticalMaxCameraRange);
        
        //Debug.Log(_mouseInput.x);
        //Debug.Log(_mouseInput.y);
    }
    
}
