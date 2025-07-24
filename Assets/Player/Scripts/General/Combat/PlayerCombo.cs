using System;
using PlayerData;
using UnityEngine;

[Serializable]
public class PlayerCombo
{
    private PlayerInputs _pInputs;
    private PlayerStats _pStats;

    [SerializeField] private int _currentComboStep = 0;
    [SerializeField] private float _comboTimer = 2;
    [SerializeField] private float _comboCooldownTimer = 0f;
    [SerializeField] private bool _isComboActive = false;

    public float testWaitForCombo = 1;
    public float testWaitForStartCombo = 1;
    public int testMaxComboSteps;
    
    public event Action OnComboStart = () => {Debug.Log("Combo Start");};
    public event Action OnComboEnd = () => {Debug.Log("Combo Start");};

    public PlayerCombo(PlayerInputs pInputs, PlayerStats pStats)
    {
        _pInputs = pInputs;
        _pStats = pStats;
    }

    public void ComboHandlerUpdate()
    {
        if (_comboCooldownTimer > 0f)
        {
            _comboCooldownTimer -= Time.deltaTime;
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
            else
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
        _currentComboStep = 1;
        //_comboTimer = _pStats.p_attackSpeed;
        _comboTimer = testWaitForCombo;

        OnComboStart?.Invoke();
    }

    private void ContinueCombo()
    {
        if (_currentComboStep < testMaxComboSteps)
        {
            _currentComboStep++;
            //_comboTimer = _pStats.p_attackSpeed;
            _comboTimer = testWaitForCombo;
        }
        else
        {
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        _isComboActive = false;
        _currentComboStep = 0;
        _comboTimer = 0f;
        _comboCooldownTimer = testWaitForStartCombo;
        
        OnComboEnd?.Invoke();
    }
}
