
using Unity.VisualScripting;
using UnityEngine;

public class DestroyableWalls : MonoBehaviour, IDestroyable
{
    //Este script es para cargar la interfaz a las paredes destruibles en la lista
    //Hay que hacer uno de estos por cada lista creada de elementos destruibles
    public void DestroyByInterface()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        //Animaciones
        //Sonidos
        //Particulas
    }
}