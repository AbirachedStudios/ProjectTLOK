
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public void Activate()
    {
        //Aca va la funcion de la interaccion especifica cuando el player la interactua
    }

    void OnDestroy()
    {
        //Animaciones
        //Sonidos
        //Particulas
        InteractablesManager.instance.CleanList(gameObject);
    }
}
