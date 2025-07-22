
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerRayCasts
{
    PlayerController _pController;
    float _distance;

    Ray ray;
    Vector3 playerPosition;


    public PlayerRayCasts(PlayerController playerController, float distance)
    {
        _distance = distance;
        _pController = playerController;
    }

    private void EyesRay()
    {
        playerPosition = _pController.transform.position;

        ray = new Ray(playerPosition, _pController.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            if (hit.collider.TryGetComponent<RainbowMaterialList>(out RainbowMaterialList rml)) //DESPUES LO ARREGLO PORQUE NO CAMBIA EL MOUSE
            {
                _pController.mouseSettings.ChangeCursor(CursorType.Attack);
                Debug.Log("Attack");

            }
        }
        else
        {
            _pController.mouseSettings.ChangeCursor(CursorType.Basic);
            Debug.Log("Basic");
        }
    }
    private static T GetRandomEnum<T>()
    {
        System.Array values = System.Enum.GetValues(typeof(T)); //Un generic base que retorna un parametro T de un tipo T.
        return (T)values.GetValue(Random.Range(0, values.Length)); //Ej: De un array retorna un elemento de dicho array
    }

    public void PlayerRayCastsUpdate()
    {
        EyesRay();
    }

    private int Pepe(int x) => x > 9 ? 0 : x + 1;
}
