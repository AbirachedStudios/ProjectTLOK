using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class LevelAudio : MonoBehaviour
{
    public static LevelAudio instance { get; private set; }

    [field: Header("Música")]
    [field: SerializeField] public EventReference musica { get; private set; }

    [field: Header("Ambiente")]
    [field: SerializeField] public EventReference ambiente { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ya hay otro coso");
            Destroy(this);
        }
        instance = this;
    }
}
