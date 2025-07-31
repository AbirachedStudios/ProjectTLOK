using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    public static InteractablesManager instance;
    //Aca van las listas de cosas interactuables
    public List<GameObject> interactablesList;

    private void Awake() { instance = this; }

    void Start()
    {
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach (var interactables in interactablesList)
        {
            interactables.AddComponent<Lever>();
        }
    }

    public void CleanList(GameObject go)
    {
        interactablesList.Remove(go);
    }

    //Aca van los metodos de interaccion con las cosas
}
