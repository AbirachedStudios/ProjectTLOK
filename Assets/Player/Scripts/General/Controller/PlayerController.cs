using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerData;

public class PlayerController : Entity
{
    [Header("References")]
    public PlayerMovement playerMovement;
    private PlayerInputs playerInputs;
    private PlayerStats playerStats;
    private PlayerRayCasts playerRayCasts;
    private PlayerCollisions playerCollisions;
    private PlayerCombo playerCombo;
    public PlayerAnimation playerAnimation;
    public MouseSettings mouseSettings;

    [Header("Dependencies")]
    CharacterController characterController;
    public Camera mainCamera;
    Transform pTransform;

    [Header("Player Stats")]
    [SerializeField] float damage;
    [SerializeField] float attackSpeed;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float armor;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float speedInterpolation;
    [SerializeField] float jumpHeight;
    [SerializeField] float coyoteTimer;
    [SerializeField] float gravity;
    private float coyoteReset;

    [Header("Player Attack")]
    [SerializeField] float distance;
    [SerializeField] float rayOffset; 

    [Header("Player Mouse")]
    [SerializeField] float turnSpeed;
    [SerializeField] Texture2D[] mouseTexture;
    
    [Header("Player Animation")]
    [SerializeField] Animator playerAnimator;

    private void Awake()
    {
        //Primero las referencias
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        pTransform = transform;
        coyoteReset = coyoteTimer;

        //Luego los constructores
        playerStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, walkSpeed, sprintSpeed, jumpHeight, gravity);
        playerInputs = new PlayerInputs();
        playerMovement = new PlayerMovement(playerInputs, playerStats, characterController, mainCamera, speedInterpolation, turnSpeed, pTransform, coyoteTimer, coyoteReset);
        playerCollisions = new PlayerCollisions();
        playerAnimation = new PlayerAnimation(playerAnimator, playerMovement, playerInputs, characterController);
        playerRayCasts = new PlayerRayCasts(this, playerInputs, distance, rayOffset);
        mouseSettings = new MouseSettings(mouseTexture);
        playerCombo = new PlayerCombo(playerInputs, playerStats);

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