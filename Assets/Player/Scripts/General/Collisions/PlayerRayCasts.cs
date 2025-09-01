
using System;
using UnityEngine;

[Serializable]
public class PlayerRayCasts
{
    private PlayerController _playerController;
    private PlayerInputs _playerInputs;
    private float _distance;
    private float _rayOffset;
    public bool canDestroy = false;
    public bool canInteract = false;
    private Ray ray;
    private Vector3 playerPosition;
    private Camera _mainCamera;

    public IDamageable destroyableTarget;
    public Transform interactableTransform;
    public LayerMask rayCastLayerMask;
    
    public PlayerRayCasts(PlayerController playerController, Camera mainCamera, PlayerInputs playerInputs, float distance, float rayOffset)
    {
        _distance = distance;
        _mainCamera = mainCamera;
        _rayOffset = rayOffset; 
        _playerController = playerController;
        _playerInputs = playerInputs;
        rayCastLayerMask = LayerMask.GetMask($"Interactable");
    }

    [SerializeField] private float interactionRadius = 0.5f;

    private void EyesRay()
    {
        playerPosition = _playerController.transform.position;
        
        
        Vector3 origin = playerPosition - _playerController.transform.forward * 0.3f;

        ray = new Ray(origin, _playerController.transform.forward);
        

        if (Physics.SphereCast(ray, interactionRadius, out RaycastHit hit, _distance))
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                canDestroy = true;
                destroyableTarget = damageable;
                destroyableTarget.damageableTransform = hit.transform;
                return;
            }

            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                canInteract = true;
                interactableTransform = hit.transform;
                return;
            }
        }
        else
        {
            canDestroy = false;
            canInteract = false;
            
            destroyableTarget = null;
            interactableTransform = null;
        }
    }
    public void CameraRay()
    {
        Vector3 rayPosition = _mainCamera.transform.position + (_mainCamera.transform.up * _rayOffset);
        ray = new Ray(rayPosition, _mainCamera.transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, _distance, rayCastLayerMask))
        {
            var destroyable = hit.collider.GetComponent<IDestroyable>();

            if (destroyable != null)
            {
                if (canDestroy)
                {
                    _playerController.mouseSettings.ChangeCursor(CursorType.Attack);
                    if (_playerInputs.IsAttacking)
                    {
                        destroyable.DestroyByInterface();
                    }
                }
                else
                {
                    _playerController.mouseSettings.ChangeCursor(CursorType.Basic);
                }
            }
            
            var interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (canInteract)
                {
                    if (_playerInputs.IsInteracting)
                    {
                        Debug.Log("Interaction complete");
                    }
                }
            }
        }
        _playerController.mouseSettings.ChangeCursor(CursorType.Basic);
    }
    public void PlayerRayCastsUpdate()
    {
        EyesRay();
        CameraRay();
    }
    /*
    private static T GetRandomEnum<T>()
    {
        System.Array values = System.Enum.GetValues(typeof(T)); //Un generic base que retorna un parametro T de un tipo T.
        return (T)values.GetValue(Random.Range(0, values.Length)); //Ej: De un array retorna un elemento de dicho array
    }
    */
}