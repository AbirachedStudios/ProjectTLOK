
using System;
using UnityEngine;

[Serializable]
public class PlayerRayCasts
{
    private PlayerController _pController;
    private PlayerInputs _pInputs;
    private float _distance;
    private float _rayOffset;
    private bool canDestroy = false;
    private bool canInteract = false;
    private Ray ray;
    private Vector3 playerPosition;


    public PlayerRayCasts(PlayerController playerController, PlayerInputs pInputs, float distance, float rayOffset)
    {
        _distance = distance;
        _rayOffset = rayOffset; 
        _pController = playerController;
        _pInputs = pInputs;
    }

    private void EyesRay()
    {
        playerPosition = _pController.transform.position;

        ray = new Ray(playerPosition, _pController.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            //Testeo de destruccion de paredes. Sujeto a cambios
            if (hit.collider.TryGetComponent<IDestroyable>(out IDestroyable destroyable))
            {
                canDestroy = true;
            }

            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                canInteract = true;
            }
        }
        else canDestroy = false; canInteract = false;
    }
    public void CameraRay()
    {
        Vector3 rayPosition = _pController.mainCamera.transform.position + (_pController.mainCamera.transform.up * _rayOffset);
        ray = new Ray(rayPosition, _pController.mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            if (hit.collider.TryGetComponent<IDestroyable>(out IDestroyable destroyable))
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
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
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