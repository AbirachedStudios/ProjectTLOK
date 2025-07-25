
using System.Runtime.CompilerServices;
using PlayerData;
using UnityEngine;

public class PlayerRayCasts
{
    PlayerController _pController;
    PlayerInputs _pInputs;
    float _distance;
    float _rayOffset;
    Ray ray;
    Vector3 playerPosition;


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
                Debug.Log("Destroyable object detected");
            }
        }
    }
    public void CameraRay()
    {
        Vector3 rayPosition = _pController.cam.transform.position + (_pController.cam.transform.up * _rayOffset);
        ray = new Ray(rayPosition, _pController.cam.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            if (hit.collider.TryGetComponent<IDestroyable>(out IDestroyable destroyable))
            {
                _pController.mouseSettings.ChangeCursor(CursorType.Attack);
                if (_pInputs.IsAttacking)
                {
                    destroyable.DestroyByInterface();
                }
                
                else
                {
                    _pController.mouseSettings.ChangeCursor(CursorType.Basic);
                }
            }
        }
    }
    public void PlayerRayCastsUpdate()
    {
        EyesRay();
    }
    /*
    private static T GetRandomEnum<T>()
    {
        System.Array values = System.Enum.GetValues(typeof(T)); //Un generic base que retorna un parametro T de un tipo T.
        return (T)values.GetValue(Random.Range(0, values.Length)); //Ej: De un array retorna un elemento de dicho array
    }
    */
}