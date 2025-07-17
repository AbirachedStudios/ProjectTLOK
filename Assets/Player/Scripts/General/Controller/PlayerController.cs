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
    [SerializeField] float gravity;
    [SerializeField] float turnSpeed;

    private void Awake()
    {
        //Primero las referencias
        cam = Camera.main;
        cc = GetComponent<CharacterController>();
        pTransform = transform;

        //Luego los constructores
        pStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, walkSpeed, sprintSpeed, jumpHeight, gravity);
        pInputs = new PlayerInputs();
        pMovement = new PlayerMovement(pInputs, pStats, cc, cam, speedInterpolation, turnSpeed, pTransform);
        pCollisions = new PlayerCollisions();

    }
    private void Update()
    {
        pInputs.InputsUpdate();
        pMovement.MovementUpdate();
    }
}