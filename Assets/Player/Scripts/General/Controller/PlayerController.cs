using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerData;
using UnityEngine.Serialization;

public class PlayerController : Entity
{
    [Header("References")]
    [SerializeField] PlayerMovement pMovement;
    PlayerInputs pInputs;
    PlayerStats pStats;

    [Header("Dependencies")]
    CharacterController cc;
    Camera cam;
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
    [SerializeField] float gravity;
    [SerializeField] float turnSpeed;

    private void Awake()
    {
        cam = Camera.main;
        cc = GetComponent<CharacterController>();
        pTransform = transform;

        pStats = new PlayerStats(this, damage, attackSpeed, maxHealth, health, armor, walkSpeed, sprintSpeed, jumpHeight, gravity);
        pInputs = new PlayerInputs();
        pMovement = new PlayerMovement(pInputs, pStats, cc, cam, speedInterpolation, turnSpeed, pTransform);
        pCombo = new PlayerCombo(pInputs, pStats);

    }
    private void Update()
    {
        pInputs.InputsUpdate();
        pMovement.MovementUpdate();
        pCombo.ComboHandlerUpdate();
    }
}