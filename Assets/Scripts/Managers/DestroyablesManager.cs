using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyablesManager : MonoBehaviour
{
    //Aca van las listas de cosas destruibles
    [SerializeField] List<GameObject> destroyableWallsList;

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

    //Aca van los metodos de destruccion de cosas
}