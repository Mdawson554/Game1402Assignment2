using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    // Input
    [SerializeField] private InputAction interactionInput;

    private IInteractable _interactable;
    private IInteractable _tempInteractable;
    
    public GameManager gameManager;

    void OnEnable()
    {
        interactionInput.Enable();
        interactionInput.performed += Interact;
    }

    void OnDisable()
    {
        interactionInput.performed -= Interact;
    }

    private void OnTriggerEnter(Collider other)
    {
        _tempInteractable = other.GetComponent<IInteractable>();

        if (_tempInteractable != null)
        {
            _interactable = _tempInteractable;
            _interactable?.OnHoverIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _interactable?.OnHoverOff();
        _interactable = null;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        _interactable?.OnInteract();
    }
    
    // to do: add binding for OnPause in the InputActions!!!
    void OnPause(InputAction.CallbackContext context) 
    {
        Debug.Log("Game Pause");
    }
}