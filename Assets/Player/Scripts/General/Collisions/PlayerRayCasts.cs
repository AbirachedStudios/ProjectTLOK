
using System;
using UnityEngine;

[Serializable]
public class PlayerRayCasts
{
    private PlayerController _pController;
    private PlayerInputs _pInputs;
    private float _distance;
    private float _rayOffset;
    public bool canDestroy = false;
    public bool canInteract = false;
    private Ray ray;
    private Vector3 playerPosition;
    private Camera _mainCamera;

    public Transform destroyableTransform;
    public Transform interactableTransform;
    
    public PlayerRayCasts(PlayerController playerController, Camera mainCamera, PlayerInputs pInputs, float distance, float rayOffset)
    {
        _distance = distance;
        _mainCamera = mainCamera;
        _rayOffset = rayOffset; 
        _pController = playerController;
        _pInputs = pInputs;
    }

    [SerializeField] private float interactionRadius = 0.5f;

    private void EyesRay()
    {
        playerPosition = _pController.transform.position;
        ray = new Ray(playerPosition, _pController.transform.forward);

        if (Physics.SphereCast(ray, interactionRadius, out RaycastHit hit, _distance))
        {
            var destroyable = hit.collider.GetComponent<IDestroyable>();
            if (destroyable != null)
            {
                canDestroy = true;
                destroyableTransform = hit.transform;
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
            
            destroyableTransform = null;
            interactableTransform = null;
        }
    }
    public void CameraRay()
    {
        Vector3 rayPosition = _mainCamera.transform.position + (_mainCamera.transform.up * _rayOffset);
        ray = new Ray(rayPosition, _mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            var destroyable = hit.collider.GetComponent<IDestroyable>();

            if (destroyable != null)
            {
                if (canDestroy)
                {
                    _pController.mouseSettings.ChangeCursor(CursorType.Attack);
                    if (_pInputs.IsAttacking)
                    {
                        destroyable.DestroyByInterface();
                    }
                }
                else
                {
                    _pController.mouseSettings.ChangeCursor(CursorType.Basic);
                }
            }
            
            var interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (canInteract)
                {
                    if (_pInputs.IsInteracting)
                    {
                        Debug.Log("Interaction complete");
                    }
                }
            }
        }
        _pController.mouseSettings.ChangeCursor(CursorType.Basic);
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