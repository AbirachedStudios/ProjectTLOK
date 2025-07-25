using System;
using PlayerData;
using UnityEngine;

[Serializable]
public class PlayerCombo
{
    private PlayerInputs _pInputs;
    private PlayerStats _pStats;

    [SerializeField] private int _currentComboCount = 0;
    [SerializeField] private float _comboTimer = 2; //El tiempo espera para continuar el combo con otro ataque o para finalizar el combo, esto también evita que el user spameé y rompa el sistema
    [SerializeField] private float _comboCooldownAttackTimer = 0f; //El tiempo de espera pos terminar combo para iniciar uno nuevo
    [SerializeField] private bool _isComboActive = false;

    [Header("Test Variables")][Space(5)]
    public float testCooldownAttackTimer = 1;
    public int testMaxComboCount = 4;
    public float testWaitForCombo = 0.6f;
    
    
    public event Action OnComboStart = () => {Debug.Log("Combo Start");};
    public event Action OnComboEnd = () => {Debug.Log("Combo Start");};

    public PlayerCombo(PlayerInputs pInputs, PlayerStats pStats)
    {
        _pInputs = pInputs;
        _pStats = pStats;
    }

    public void ComboHandlerUpdate()
    {
        if (_comboCooldownAttackTimer > 0f)
        {
            _comboCooldownAttackTimer -= Time.deltaTime;
            return;
        }
        
        HandleComboInput();
        HandleComboTimer();
    }

    private void HandleComboInput()
    {
        if (_pInputs.IsAttacking)
        {
            if (!_isComboActive)
            {
                StartCombo();
            }
            else if (_currentComboCount < testMaxComboCount)
            {
                ContinueCombo();
            }
        }
    }

    private void HandleComboTimer()
    {
        if (_isComboActive)
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
        _isComboActive = true;
        _currentComboCount = 1;
        _comboTimer = _pStats.p_attackSpeed;
        _comboTimer = testWaitForCombo;

        OnComboStart?.Invoke();
    }

    private void ContinueCombo()
    {
        _currentComboCount++;
        _comboTimer = _pStats.p_attackSpeed;

        if (_currentComboCount >= testMaxComboCount)
        {
            // Combo máximo alcanzado, no incrementa más
            _currentComboCount = testMaxComboCount;
        }
    }

    private void ResetCombo()
    {
        _isComboActive = false;
        _currentComboCount = 0;
        _comboTimer = 0f;
        _comboCooldownAttackTimer = testCooldownAttackTimer;
        
        OnComboEnd?.Invoke();
    }
}
