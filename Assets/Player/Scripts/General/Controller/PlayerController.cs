using UnityEngine;
using PlayerData;

public class PlayerController : Entity
{
    [Header("References")]
    private PlayerMovement playerMovement;
    private PlayerInputs playerInputs;
    private PlayerStats playerStats;
    [SerializeField] private PlayerRayCasts playerRayCasts;
    [SerializeField] private PlayerCollisions playerCollisions;
    [SerializeField] private PlayerCombo playerCombo;
    [SerializeField] private PlayerAnimation playerAnimation;
    public MouseSettings mouseSettings;

    [Header("Dependencies")]
    public Camera mainCamera;
    private CharacterController _characterController;
    private Animator _playerAnimator;

    [Header("Player Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float armor;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float speedInterpolation;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float coyoteTimer;
    [SerializeField] private float gravity;
    [SerializeField] private float coyoteReset;

    [Space(5)][Header("Player Attack")]
    [SerializeField] private float distance;
    [SerializeField] private float rayOffset; 

    [Header("Player Mouse")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private Texture2D[] mouseTexture;
    

    private void Awake()
    {
        //Primero las referencias
        mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<Animator>();
        coyoteReset = coyoteTimer;

        //Luego los constructores
        playerInputs = new PlayerInputs();
        playerCollisions = new PlayerCollisions();
        
        playerStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, walkSpeed, sprintSpeed, jumpHeight, gravity);
        playerMovement = new PlayerMovement(playerInputs, playerStats, _characterController, mainCamera, speedInterpolation, turnSpeed, transform, coyoteTimer, coyoteReset);
        playerAnimation = new PlayerAnimation(_playerAnimator, playerMovement, playerInputs, _characterController);
        playerRayCasts = new PlayerRayCasts(this, playerInputs, distance, rayOffset);
        playerCombo = new PlayerCombo(playerInputs, playerStats);
        mouseSettings = new MouseSettings(mouseTexture);

    }
    private void Update()
    {
        playerInputs.InputsUpdate();
        playerMovement.MovementUpdate();
        playerRayCasts.PlayerRayCastsUpdate();
        playerCombo.ComboHandlerUpdate();
        playerCollisions.PlayerCollisionsUpdate();
        playerAnimation.AnimationUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * distance);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(mainCamera.transform.position + (mainCamera.transform.up * rayOffset), mainCamera.transform.forward * distance);
    }
}