using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerData;

public class PlayerController : Entity
{
    [Header("References")]
    PlayerMovement pMovement;
    PlayerInputs pInputs;
    PlayerStats pStats;
    PlayerRayCasts pRayCasts;
    PlayerCollisions pCollisions;
    public MouseSettings mouseSettings;

    [Header("Dependencies")]
    CharacterController cc;
    public Camera cam;
    Transform pTransform;
    public PlayerCombo pCombo;

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

    [Header("Access")]
    public bool isMoving;
    public bool isJumping;

    public static PlayerController instance;

    private void Awake()
    {
        //Primero las referencias
        cam = Camera.main;
        cc = GetComponent<CharacterController>();
        pTransform = transform;
        coyoteReset = coyoteTimer;

        //Luego los constructores
        pStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, walkSpeed, sprintSpeed, jumpHeight, gravity);
        pInputs = new PlayerInputs();
        pMovement = new PlayerMovement(pInputs, pStats, cc, cam, speedInterpolation, turnSpeed, pTransform, coyoteTimer, coyoteReset);
        pCollisions = new PlayerCollisions();
        pRayCasts = new PlayerRayCasts(this, pInputs, distance, rayOffset);
        mouseSettings = new MouseSettings(mouseTexture);
        pCombo = new PlayerCombo(pInputs, pStats);

    }
    private void Update()
    {
        pInputs.InputsUpdate();
        pMovement.MovementUpdate();
        pRayCasts.PlayerRayCastsUpdate();
        pCombo.ComboHandlerUpdate();
        pCollisions.PlayerCollisionsUpdate();
        isMoving = pInputs.MoveInput != Vector3.zero;
        isJumping = pInputs.IsJumping;
    }

    public void ChangeStats(int i, float buff, float timer)
    {
        switch (i)
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
        }
    }
}