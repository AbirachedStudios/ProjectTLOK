
using UnityEngine;
using PlayerData;

public class PlayerMovement
{/*
    [Header("References")]
    private CharacterController controller;
    private PlayerInputs playerInputs;
    [SerializeField] Transform cam;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float sprintTransitionSprint = 5f;
    [SerializeField] float turnSpeed = 2f;


    [Header("Jump Settings")]
    private float verticalVelocity;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpHeight = 2f;
    public bool isMoving=false;

    private float storeSpeed;
    //private SoundControl sc;
    public float stepTimer = 0f;
    [SerializeField] float intervaloPisadas = 0.5f;
    //*****************************************/

    //*****-References-****//

    PlayerInputs _pInputs;
    PlayerStats _pStats;

    CharacterController _pController;

    //*****-Variables-****//

    //**Movement**//
    Vector3 _move;

    //**Jump**//
    float _downForce;

    public PlayerMovement(PlayerInputs pInputs, PlayerStats pStats, CharacterController controller)
    {
        _pInputs = pInputs;
        _pStats = pStats;
        _pController = controller;
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

    void GroundMovement()
    {
        _move = new Vector3(_pInputs.MoveInput.x, 0f, _pInputs.MoveInput.y);
        _move = cam.transform.TransformDirection(_move);
        _move.Normalize();

        if(_pInputs.IsRunning)
        {
            storeSpeed = Mathf.Lerp(storeSpeed, sprintSpeed, sprintTransitionSprint * Time.deltaTime);
        }
        else
        {
            storeSpeed = Mathf.Lerp(storeSpeed, walkSpeed, sprintTransitionSprint * Time.deltaTime);
        }

        _move *= storeSpeed;

        _move.y = Gravity();

        if (controller.isGrounded && (_move.x != 0 || _move.z != 0))
        {
            if (!isMoving)
            {
                isMoving = !isMoving;
            }
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
              //  sc.Pisadas();
                
                stepTimer = intervaloPisadas;
            }
        }
        else
        {
            if (isMoving)
            {
                if (!playerInputs.IsJumping)
                {
                    isMoving = !isMoving;
                }
            }
           
            stepTimer = 0f;
        }

        controller.Move(_move * Time.deltaTime);
    }

    void Turn()
    {
        if (Mathf.Abs(_pInputs.MoveInput.x) != 0 || Mathf.Abs(_pInputs.MoveInput.y) != 0)
        {
            Vector3 currentLookDirection = _pController.velocity.normalized;
            currentLookDirection.y = 0f;

            currentLookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
           // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
    }
    private float Gravity()
    {
        if(_pController.isGrounded)
        {
            _downForce = -1f;

            if(_pInputs.IsJumping)
            {
                _downForce = Mathf.Sqrt(_downForce * _pStats.p_gravity * 2); //Formula general para calcular la fuerza de salto en base a la gravedad
            }
        }
        else
        {
            _downForce -= _pStats.p_gravity * Time.deltaTime;
        }

        return _downForce;
    }
}