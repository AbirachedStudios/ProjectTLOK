using UnityEngine;

public class LockMovementSMBehaviour : StateMachineBehaviour
{
    //Esta clase es utilizada por todos los estados de animación donde el jugador no puede moverse (ataques) para no moverse durante el combo
    //Es la manera más elegante que encontré para prohibir el movimiento del jugador durante las animaciones del combate.
    //Este script está sujeto a cambios.
    
    private PlayerMovement playerMovement;

    // método para inyectar la referencia desde PlayerController
    public void Initialize(PlayerMovement controller)
    {
        playerMovement = controller;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerMovement != null)
            playerMovement.canMove = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerMovement != null)
            playerMovement.canMove = true;
    }
}
