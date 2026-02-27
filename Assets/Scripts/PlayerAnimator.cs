using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator anim;
    
    Vector3 _playerVelocity;
    
    void Update()
    {
        anim.SetBool("IsGrounded", playerController.IsGrounded());
        _playerVelocity += playerController.GetPlayerVelocity();
        _playerVelocity.y = 0;
        anim.SetFloat("Velocity", playerController.GetPlayerVelocity().sqrMagnitude);
    }

    void OnEnable()
    {
        playerController.OnJumpEvent += OnJump;
    }

    void OnDisable()
    {
        playerController.OnJumpEvent -= OnJump;
    }

    private void OnJump()
    {
        anim.SetTrigger("Jump");
    }
}