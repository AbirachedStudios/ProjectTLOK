using System.Collections;
using UnityEngine;

//Este script se encarga de manejar los eventos de animación del jugador, como moverlo hacia el objetivo durante un ataque y gestionar el combo de ataques.
//Mirar las animaciones de ataque para entender mejor los eventos
[RequireComponent(typeof(PlayerController))]
public class PlayerEventHandler : MonoBehaviour
{
    public PlayerController playerController;
    private PlayerCombo _playerCombo;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        _playerCombo = playerController.playerCombo;
    }

    public void MoveTowardsTargetAnimationEvent(float duration = 0.3f)
    {
        float deltaDistance = 0.8f;
        if(_playerCombo.currentTarget == null) return;
            MoveTowardsTarget(_playerCombo.currentTarget.position, deltaDistance,duration);
    }
    
    public void AttackAnimationEvent()
    {
        //Evento que es cuando el jugador hace daño
        //MakeDamage() por ejemplo
    }
    
    public void AttackEndAnimationEvent()
    {
        //Evento hecho para que mejorar la fluidez del combo que permite que el jugador no pueda romper el juego spameando botones
        playerController.playerCombo.SetAvailableToContinueCombo();
        
    }
     
    //Función que mueve y apunta al jugador hacía el objetivo
    /// <summary>
    /// Mueve al jugador hacia una posición target y lo hace rotar en esa dirección.
    /// </summary>
    /// <param name="target">Posición target hacia donde moverse.</param>
    /// <param name="deltaDistance">
    /// Distancia mínima que se debe mantener respecto al objetivo.  
    /// Si es 0, el jugador se moverá directamente pegado hasta la posición del target sin margen.  
    /// </param>
    /// <param name="duration">
    /// Duración del movimiento en segundos.  
    /// Controla qué tan suave/lento será el desplazamiento hacia el objetivo.  
    /// </param>
    private void MoveTowardsTarget(Vector3 target, float deltaDistance, float duration = 0.3f)
    {
        FaceThis(target);
        Vector3 finalPos = Vector3.MoveTowards(target, transform.position, deltaDistance);
        finalPos.y = transform.position.y;

        // Llama a una corrutina para manejar el movimiento suave
        StartCoroutine(MovePlayerCoroutine(finalPos, duration));
    }

    //Corrutina que suaviza ese movimiento
    private IEnumerator MovePlayerCoroutine(Vector3 targetPosition, float duration)
    {
        Vector3 initialPosition = transform.position;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    //Apunta y rota hacía el objeto que está enfocando
    private void FaceThis(Vector3 target)
    {
        Vector3 targetVector = new Vector3(target.x, target.y, target.z);
        Quaternion lookAtRotation = Quaternion.LookRotation(targetVector - transform.position);
        
        lookAtRotation.x = 0;
        lookAtRotation.z = 0;

        // Llama a una corrutina para manejar la rotación suave
        StartCoroutine(RotatePlayerCoroutine(lookAtRotation, 0.2f));
    }
    
    
    //Corrutina que se encarga de rotar suavemente
    private IEnumerator RotatePlayerCoroutine(Quaternion targetRotation, float duration)
    {
        Quaternion initialRotation = transform.localRotation;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
    }
}