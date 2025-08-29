using System;
using UnityEngine;

[Serializable]
public class PlayerCombo
{
    private PlayerInputs _playerInputs;
    private PlayerStats _playerStats;
    private PlayerRayCasts _playerRaycast;

    [SerializeField] public int _currentComboCount = 0;
    [SerializeField] private float _comboTimer = 2; //El tiempo espera para continuar el combo con otro ataque o para finalizar el combo, esto también evita que el user spameé y rompa el sistema
    [SerializeField] private float _comboCooldownAttackTimer = 0f; //El tiempo de espera pos terminar combo para iniciar uno nuevo
    [SerializeField] private bool _isActive = false;
    [SerializeField] private bool _isDoingCombo = false;

    [Header("Test Variables")][Space(5)]
    public float testCooldownAttackTimer = 1;
    public int testMaxComboCount = 4;
    public float testWaitForCombo = 1.6f;
    
    public IDamageable currentTarget;
    
    public event Action OnComboStart = () =>
    {
        //Debug.Log("Combo Start");
    };
    public event Action OnComboEnd = () =>
    {
        //Debug.Log("Combo Start");
    };

    public PlayerCombo(PlayerInputs playerInputs, PlayerStats playerStats, PlayerRayCasts playerRaycast)
    {
        _playerInputs   = playerInputs;
        _playerStats    = playerStats;
        _playerRaycast  = playerRaycast;
    }

    public void ComboHandlerUpdate()
    {
        if (_comboCooldownAttackTimer >= 0f)
        {
            _comboCooldownAttackTimer -= Time.deltaTime;
            return;
        }
        
        HandleComboInput();
        HandleComboTimer();
    }

    private void HandleComboInput()
    {
        if (!_playerRaycast.canDestroy && currentTarget == null)
            return;
        
        
        if (_playerInputs.IsAttacking && !_isDoingCombo)
        {
            if (!_isActive)
            {
                StartCombo();
                _isDoingCombo = true;
            }
            else if (_currentComboCount < testMaxComboCount)
            {
                ContinueCombo();
                _isDoingCombo = true;
            }
        }
    }

    private void HandleComboTimer()
    {
        if (_isActive)
        {
            _comboTimer -= Time.deltaTime;

            if (_comboTimer <= 0f)
            {
                ResetCombo();
            }
        }
    }

    private void StartCombo()
    {
        _isActive = true;
        _currentComboCount = 1;
        _comboTimer = _playerStats.playerAttackSpeed;
        _comboTimer = testWaitForCombo;

        currentTarget = _playerRaycast.destroyableTarget;
        OnComboStart?.Invoke();
    }

    private void ContinueCombo()
    {
        _currentComboCount++;
        _comboTimer = _playerStats.playerAttackSpeed;

        if (_currentComboCount >= testMaxComboCount)
        {
            // Combo máximo alcanzado, no incrementa más
            _currentComboCount = testMaxComboCount;
        }
    }

    private void ResetCombo()
    {
        _isActive = false;
        _currentComboCount = 0;
        _comboTimer = 0f;
        _isDoingCombo = false;
        _comboCooldownAttackTimer = testCooldownAttackTimer;
        
        currentTarget = null;
        OnComboEnd?.Invoke();
    }

    public void SetAvailableToContinueCombo()
    {
        _isDoingCombo = false;
    }
}
