
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    private PlayerInputs playerInputs;
    [SerializeField] Transform cam;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float sprintTransitionSprint = 5f;
    [SerializeField] float turnSpeed = 2f;
    private Vector3 move; //Necesito


    [Header("Jump Settings")]
    private float verticalVelocity;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpHeight = 2f;
    public bool isMoving=false;

    private float storeSpeed;
    //private SoundControl sc;
    public float stepTimer = 0f;
    [SerializeField] float intervaloPisadas = 0.5f;
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
       // sc = GetComponent<SoundControl>();
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    void Update()
    {
        Movement();
        Gravity();
    }

    void Movement()
    {
        GroundMovement();
        Turn();
    }

    void GroundMovement()
    {
        move = new Vector3(playerInputs.MoveInput.x, 0f, playerInputs.MoveInput.y);
        move = cam.transform.TransformDirection(move);
        move.Normalize();

        if(playerInputs.IsRunning)
        {
            storeSpeed = Mathf.Lerp(storeSpeed, sprintSpeed, sprintTransitionSprint * Time.deltaTime);
        }
        else
        {
            storeSpeed = Mathf.Lerp(storeSpeed, walkSpeed, sprintTransitionSprint * Time.deltaTime);
        }

        move *= storeSpeed;

        move.y = Gravity();

        /*if (controller.isGrounded) 
    { 
        if (move.x != 0 || move.y != 0)
        {
            sc.Pisadas();
        }
    }*/
        if (controller.isGrounded && (move.x != 0 || move.z != 0))
        {
            if (!isMoving)
            {
                isMoving = !isMoving;
                Debug.Log(isMoving);
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
           
            stepTimer = 0f; // reiniciar si no se mueve o est� en el aire
        }

        controller.Move(move * Time.deltaTime);
    }

    void Turn()
    {
        //if (Mathf.Abs(playerInputs.MoveInput.x) > 0 || Mathf.Abs(playerInputs.MoveInput.x) < 0 || Mathf.Abs(playerInputs.MoveInput.y) < 0 || Mathf.Abs(playerInputs.MoveInput.y) > 0)
        if (Mathf.Abs(playerInputs.MoveInput.x) != 0 || Mathf.Abs(playerInputs.MoveInput.y) != 0)
        {
            Vector3 currentLookDirection = controller.velocity.normalized;
            currentLookDirection.y = 0f;

            currentLookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
    }
    private float Gravity()
    {
        if(controller.isGrounded)
        {
            verticalVelocity = -1f;

            if(playerInputs.IsJumping)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2); //Formula general para calcular la fuerza de salto en base a la gravedad
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        return verticalVelocity;
    }
}