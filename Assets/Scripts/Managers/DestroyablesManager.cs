using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyablesManager : MonoBehaviour
{
    public static DestroyablesManager instance;
    //Aca van las listas de cosas destruibles
    public List<GameObject> destroyableWallsList;

    private void Awake() { instance = this; }

    void Start()
    {
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach (var walls in destroyableWallsList)
        {
            walls.AddComponent<DestroyableWalls>();
        }
    }

    public void CleanList(GameObject go)
    {
        destroyableWallsList.Remove(go);
    }

    //Aca van los metodos de destruccion de cosas
}