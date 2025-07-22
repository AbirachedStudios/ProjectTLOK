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
    PlayerCollisions pCollisions;
    PlayerRayCasts pRayCasts;

    [Header("Dependencies")]
    CharacterController cc;
    Camera cam;
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
    [SerializeField] float turnSpeed;

    [Header("Player Attack")]
    [SerializeField] float distance;

    private float coyoteReset;

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
        pRayCasts = new PlayerRayCasts(this, distance);

    }
    private void Update()
    {
        pInputs.InputsUpdate();
        pMovement.MovementUpdate();
        pRayCasts.PlayerRayCastsUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * distance);
    }
}