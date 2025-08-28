using UnityEngine;

public class PlayerController : Entity
{
    #region Dependencies

    [Header("Dependencies")]
    private Camera _mainCamera;
    private CharacterController _characterController;
    private Animator _playerAnimator;

    #endregion

    #region References

    [Header("References")]
    public PlayerMovement playerMovement;
    public PlayerInputs playerInputs;
    public PlayerStats playerStats;
    public PlayerRayCasts playerRayCasts;
    public PlayerCollisions playerCollisions;
    public PlayerAnimation playerAnimation;
    public PlayerCombo playerCombo;
    public MouseSettings mouseSettings;

    #endregion

    #region Player Configs
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
    #endregion

    private void Awake()
    {
        //Primero las referencias
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<Animator>();
        coyoteReset = coyoteTimer;

        //Luego los constructores en orden de dependencia
        playerInputs = new PlayerInputs();
        playerRayCasts = new PlayerRayCasts(this,_mainCamera, playerInputs, distance, rayOffset);
        
        playerCollisions = new PlayerCollisions();
        
        playerStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, walkSpeed, sprintSpeed, jumpHeight, gravity);
        
        playerMovement = new PlayerMovement(playerInputs, playerStats, _characterController, _mainCamera, speedInterpolation, turnSpeed, transform, coyoteTimer, coyoteReset);
        
        playerCombo = new PlayerCombo(playerInputs, playerStats, playerRayCasts);
        playerAnimation = new PlayerAnimation(_playerAnimator, playerMovement, playerInputs, _characterController, playerCombo);
        
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
        _mainCamera = Camera.main;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * distance);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_mainCamera.transform.position + (_mainCamera.transform.up * rayOffset), _mainCamera.transform.forward * distance);
    }
}