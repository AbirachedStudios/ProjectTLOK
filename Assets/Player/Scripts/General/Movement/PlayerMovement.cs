using System;
using UnityEngine;
using PlayerData;

[Serializable]
public class PlayerMovement
{
    //*****-References-****//
    private PlayerInputs _pInputs;
    private PlayerStats _pStats;
    private CharacterController characterController;


    //**Movement**//
    public Vector3 move;
    public bool freeFall;
    private Transform _pTransform;
    private Camera _cam;
    private float _storeSpeed;
    private float _speedInterpolation;
    private float _turnSpeed;

    private float _coyoteTimer;
    private float _coyoteTimeReset;

    //**Jump**//
    private float _downForce;

    public PlayerMovement(PlayerInputs pInputs, PlayerStats pStats, CharacterController controller, Camera cam, float speedInterpolate, float turnSpeed, Transform pTransform, float coyote, float coyoteReset)
    {
        _pInputs = pInputs;
        _pStats = pStats;
        characterController = controller;
        _cam = cam;
        _speedInterpolation = speedInterpolate;
        _pTransform = pTransform;
        _turnSpeed = turnSpeed;
        _coyoteTimer = coyote;
        _coyoteTimeReset = coyoteReset;
    }

    public void MovementUpdate()
    {
        Movement();
        Gravity();
    }
    private void Movement()
    {
        GroundMovement();
        Turn();
    }

    private void GroundMovement()
    {
        move = new Vector3(_pInputs.MoveInput.x, 0f, _pInputs.MoveInput.y);
        move = _cam.transform.TransformDirection(move);
        move.Normalize();

        if(_pInputs.IsRunning)
        {
            _storeSpeed = Mathf.Lerp(_storeSpeed, _pStats.playerSprintSpeed, _speedInterpolation * Time.deltaTime);
        }
        else
        {
            _storeSpeed = Mathf.Lerp(_storeSpeed, _pStats.playerWalkSpeed, _speedInterpolation * Time.deltaTime);
        }

        move *= _storeSpeed;

        move.y = Gravity();

        characterController.Move(move * Time.deltaTime);
    }

    private void Turn()
    {
        if (Mathf.Abs(_pInputs.MoveInput.x) != 0 || Mathf.Abs(_pInputs.MoveInput.y) != 0)
        {
            Vector3 currentLookDirection = characterController.velocity.normalized;
            currentLookDirection.y = 0f;

            currentLookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
            _pTransform.rotation = Quaternion.Slerp(_pTransform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
        }
    }

    private float Gravity()
    {
        if (characterController.isGrounded)
        {
            _coyoteTimer = _coyoteTimeReset;
            freeFall = false;

            if (_pInputs.IsJumping)
            {
                _downForce = Mathf.Sqrt(_pStats.playerJumpHeight * _pStats.playerGravity * 2); //Formula general para calcular la fuerza de salto en base a la gravedad
            }
        }
        else
        {
           _coyoteTimer -= Time.deltaTime;

            if(_pInputs.IsJumping && _coyoteTimer > 0f)
            {
                _downForce = Mathf.Sqrt(_pStats.playerJumpHeight * _pStats.playerGravity * 2f);
                freeFall = true;
                _coyoteTimer = 0f;
            }
            _downForce -= _pStats.playerGravity * Time.deltaTime;
        }

        return _downForce;
    }
}