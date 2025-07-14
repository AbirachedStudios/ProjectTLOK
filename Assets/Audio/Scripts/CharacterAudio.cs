using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CharacterAudio : MonoBehaviour
{
    public static CharacterAudio instance { get; private set; }

    [field: Header("Golpe")]
    [field: SerializeField] public EventReference golpe { get; private set; }

    [field: Header("Especial")]
    [field: SerializeField] public EventReference especial { get; private set; }

    [field: Header("Pasos")]
    [field: SerializeField] public EventReference pasos { get; private set; }
    
    [field: Header("Salto")]
    [field: SerializeField] public EventReference salto { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ya hay otro coso");
            Destroy(this);
        }
        instance = this;

        //FMODEvents.instance.SetAudio(golpe, especial, pasos, salto);
    }

    public void PlaySteps(Transform t)
    {
        AudioManager.instance.Steps(pasos, transform.position);
    }
    public void PlayJump(Transform t)
    {
        AudioManager.instance.PlaySound(salto, t.position);
    }
}
