using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public List<GameObject> prefabToSpawn;
    [SerializeField] int maxChance;
    [SerializeField] int dropRate;

    void Start()
    {
        InitializeListOfPrefabs();
    }

    private void InitializeListOfPrefabs()
    {
        foreach (var prefabs in prefabToSpawn) prefabToSpawn.Add(prefabs);
    }

    public void SetPrefabDroped() //Este metodo llamarlo cuando se destruye / elimina un enemigo para que elija que dropear
    {
        int rng = Random.Range(0, maxChance);
        int listRng = Random.Range(0, prefabToSpawn.Count);

        if (rng <= dropRate)
        {
            GameObject prefabInstance = Instantiate(prefabToSpawn[listRng], transform.position, Quaternion.identity);
            print(prefabInstance.transform.position);
        }
    }
}
