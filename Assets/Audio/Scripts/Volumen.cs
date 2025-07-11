using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volumen : MonoBehaviour
{
    public float musica;
    public float SFX;
    public float ambiente;

    public void VolumenDeMusica()
    {
        AudioManager.instance.VolumenMusica(musica);
    }

    public void VolumenDeAmbiente()
    {
        AudioManager.instance.CambiarAmbiente("Intensidad", ambiente);
    }
}
