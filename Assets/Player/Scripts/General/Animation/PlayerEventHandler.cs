using System;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    public PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    
    public void AttackAnimationEvent()
    {
        
    }
    
    public void AttackEndAnimationEvent()
    {
        playerController.playerCombo.SetAvailableToContinueCombo();
        print("UNABLE");
    }
}