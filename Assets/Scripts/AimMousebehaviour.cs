using UnityEngine;
using UnityEngine.InputSystem;

public class AimMousebehaviour : MonoBehaviour
{
    
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private GameObject aimCamera;
    
    [SerializeField] private float _horizonatalMaxCamerRange = 50f;
    [SerializeField] private float _horizontalMinCameraRange = 50f;
    
    [SerializeField] private float _verticalMinCameraRange = 50f;
    [SerializeField] private float _verticalMaxCameraRange = 50f;

    private float xRot;
    private float yRot;
    
    private Vector2 _mouseInput;
    
    void Update()
    {
        CalculateCamera();
    }
    
    void Start()
    {
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
        xRot += _mouseInput.y * cameraSensitivity * Time.deltaTime;
        yRot += _mouseInput.x * cameraSensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, -_horizontalMinCameraRange, _horizonatalMaxCamerRange);
        xRot = Mathf.Clamp(xRot, -_verticalMinCameraRange, _verticalMaxCameraRange);
        var vector = new Vector3(xRot, yRot, aimCamera.transform.rotation.z);
        // aimCamera.transform.eulerAngles = vector;
        aimCamera.transform.eulerAngles = vector;
    }
}
