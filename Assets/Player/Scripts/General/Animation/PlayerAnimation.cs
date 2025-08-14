using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int FreeFall = Animator.StringToHash("FreeFall");
    private static readonly int CurrentCombo = Animator.StringToHash("CurrentCombo");

    private Animator _playerAnimator;
    private PlayerMovement _playerMovement;
    private PlayerInputs _playerInputs;
    private CharacterController _characterController;
    private PlayerCombo _playerCombo;
    
    //Suavizado de la velocidad para la animación que no sea tan brusca
    private float _smoothSpeed;
    private float _smoothTime = 0.1f;
    
    public PlayerAnimation(Animator playerAnimator, PlayerMovement playerMovement, PlayerInputs playerInputs, CharacterController characterController, PlayerCombo playerCombo)
    {
        _playerAnimator = playerAnimator;
        _playerMovement = playerMovement;
        _playerInputs = playerInputs;
        _characterController = characterController;
        _playerCombo = playerCombo;
    }
    
    public void AnimationUpdate()
    {
        if (_playerAnimator)
        {
            float targetSpeed = new Vector2(_playerMovement.move.x, _playerMovement.move.z).magnitude;
            _smoothSpeed = Mathf.Lerp(_smoothSpeed, targetSpeed, Time.deltaTime / _smoothTime);
            
            _playerAnimator.SetFloat(Speed, _smoothSpeed);
            _playerAnimator.SetBool(Jump, _playerInputs.IsJumping);
            _playerAnimator.SetBool(Grounded, _characterController.isGrounded);
            _playerAnimator.SetBool(FreeFall, _playerMovement.freeFall);
            _playerAnimator.SetInteger(CurrentCombo, _playerCombo._currentComboCount);
        }

    }
}