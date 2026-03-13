using UnityEngine;
using UnityEngine.InputSystem;

public class AimMousebehaviour : MonoBehaviour
{
    
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private GameObject aimCamera;
    
    [SerializeField] private float _verticalMinCameraRange = 50f;
    [SerializeField] private float _verticalMaxCameraRange = 50f;

    private float xRot;
    private float yRot;
    
    private Vector2 _mouseInput;
    private PlayerController _player;
    void Update()
    {
        CalculateCamera();
    }
    
    void Start()
    {
        _player = GetComponent<PlayerController>();
        ShowMouse(false);
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
        if(!_player.IsAiming) return;
        
        Debug.Log($"Looking at {_mouseInput.x}, {_mouseInput.y}");
        yRot += _mouseInput.x * cameraSensitivity * Time.deltaTime;
        xRot += _mouseInput.y * cameraSensitivity * Time.deltaTime;
        
        xRot = Mathf.Clamp(xRot, -_verticalMinCameraRange, _verticalMaxCameraRange);
        var vector = new Vector3(-xRot, 0, 0);
        aimCamera.transform.eulerAngles = vector;
       var rotation = new Vector3(0,-yRot, 0);
       transform.eulerAngles = rotation;
    }
}
