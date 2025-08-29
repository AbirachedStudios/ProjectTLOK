using System.Collections;
using System.Collections.Generic;
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
    public SoundControl pSoundControl;

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
    
    [Header("Access")]
    public bool isMoving;
    public bool isJumping;

    public static PlayerController instance;

    private void Awake()
    {
        //Primero las referencias
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<Animator>();
        coyoteReset = coyoteTimer;

        if(instance != null)
        {
            Debug.Log("Ya hay uno"); Destroy(this);
        } 
        else { instance = this; }

        
        //Luego los constructores en orden de dependencia
        playerInputs = new PlayerInputs();
        playerRayCasts = new PlayerRayCasts(this,_mainCamera, playerInputs, distance, rayOffset);
        
        playerCollisions = new PlayerCollisions();
        
        playerStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, walkSpeed, sprintSpeed, jumpHeight, gravity);
        
        playerMovement = new PlayerMovement(playerInputs, playerStats, _characterController, _mainCamera, speedInterpolation, turnSpeed, transform, coyoteTimer, coyoteReset);
        
        playerCombo = new PlayerCombo(playerInputs, playerStats, playerRayCasts);
        playerAnimation = new PlayerAnimation(_playerAnimator, playerMovement, playerInputs, _characterController, playerCombo);
        
        pSoundControl = new SoundControl(transform, GetComponent<CharacterAudio>(), this, _characterController);
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
        
        pSoundControl.SoundControllerUpdate();
        isMoving = playerInputs.MoveInput != Vector3.zero;
        isJumping = playerInputs.IsJumping;
    }

    public float offset;
    private void OnDrawGizmos()
    {
        _mainCamera = Camera.main;
        
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position - transform.forward * offset;
        Gizmos.DrawRay(origin, transform.forward * distance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_mainCamera.transform.position + (_mainCamera.transform.up * rayOffset), _mainCamera.transform.forward * distance);
    }
    
    public void ChangeStats(int i, float buff/*, float timer*/)
    {
        /*switch (i)
        {
            case 0:
                StartCoroutine(pStats.StatFlatChronometer(pStats.p_damage, buff, timer));
                break;

            case 1:
                StartCoroutine(pStats.StatFlatChronometer(pStats.p_attackSpeed, buff, timer));
                break;

            case 2:
                StartCoroutine(pStats.StatFlatChronometer(pStats.p_armor, buff, timer));
                break;

            case 3:
                StartCoroutine(pStats.StatFlatChronometer(pStats.p_walkSpeed, buff, timer));
                StartCoroutine(pStats.StatFlatChronometer(pStats.p_sprintSpeed, buff, timer));
                break;
        }*/
        StartCoroutine(playerStats.BoostStat(i, buff));
    }
}