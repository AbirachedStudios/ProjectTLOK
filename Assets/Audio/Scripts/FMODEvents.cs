using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    /* Ejemplo de campo de Evento de FMOD
    [field: Header()]
    [field: SerializeField] public EventReference { get; private set; }*/

    [field: Header("Ambiente")]
    [field: SerializeField] public EventReference ambiente { get; private set; }

    [field: Header("Música")]
    [field: SerializeField] public EventReference musica { get; private set; }

    [field: Header("Menú")]
    [field: SerializeField] public EventReference menu { get; private set; }

    [field: Header("Item SFX")]
    [field: SerializeField] public EventReference itemGet { get; private set; }

    /*[field: Header("Golpes")]
    [field: SerializeField] public EventReference Golpe { get; private set; }
    [field: Header("Ataque especial")]
    [field: SerializeField] public EventReference Special { get; private set; }
    [field: Header("Salto")]
    [field: SerializeField] public EventReference Salto { get; private set; }
    [field: Header("Pasos")]
    [field: SerializeField] public EventReference Pasos { get; private set; }*/

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ya hay otro coso");
            Destroy(this);
        }
        instance = this;
    }

    private void Start()
    {

        ambiente = LevelAudio.instance.ambiente;
        musica = LevelAudio.instance.musica;

        /*Golpe = CharacterAudio.instance.golpe;
        Special = CharacterAudio.instance.especial;
        Salto = CharacterAudio.instance.salto;
        Pasos = CharacterAudio.instance.pasos;*/
    }

    /*public void SetAudio(EventReference golpeRef, EventReference specialRef, EventReference saltoRef, EventReference pasosRef)
    {
        Golpe = golpeRef;
        Special = specialRef;
        Salto = saltoRef;
        Pasos = pasosRef;
    }*/
}
