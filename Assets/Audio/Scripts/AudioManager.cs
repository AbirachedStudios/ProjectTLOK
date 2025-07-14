using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Threading;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    //Booleano para controlar la m�sica
    public bool musicaOn;

    //Crea una lista de los eventos para la funci�n CleanUp
    private List<EventInstance> eventos;

    //Evento que maneja la m�sica
    private EventInstance musica;

    //Evento que maneja el ambiente
    private EventInstance ambientaci�n;

    //Evento que maneja el men�
    private EventInstance menu;

    private float timer = 0;

    public static AudioManager instance { get; private set; }

    /*private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ya hay otro coso");
        }
        instance = this;

        eventos = new List<EventInstance>();
    }*/

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ya hay otro coso");
            Destroy(this);
        }
        instance = this;

        eventos = new List<EventInstance>();

        IniciarAmbiente(FMODEvents.instance.ambiente);
        if (musicaOn)
        {
            IniciarMusica(FMODEvents.instance.musica);
        }
    }

    private void Update()
    {
        if (!musicaOn)
        {
            musica.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        if (timer > 0) { timer -= Time.deltaTime; }
    }

    //Crea un evento de sonido
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventos.Add(eventInstance);

        return eventInstance;
    }

    //Borra los eventos para que no queden cuando cambia a escena
    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventos)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    //Funci�n para los efectos de sonido
    public void PlaySound(EventReference s, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(s, worldPos);
    }

    //Funci�n para los pasos
    public void Steps(EventReference s, Vector3 worldPos)
    {
        /*if (timer <= 0)
        {*/
            var instance = RuntimeManager.CreateInstance(s);
            instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
            instance.start();
            /*timer = 0.56f;
            StartCoroutine(Delay(timer, instance));
        }*/
    }

    //Funci�n para atrasar funciones
    IEnumerator Delay(float f, EventInstance e)
    {
        yield return new WaitForSeconds(f);
        e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        e.release();
    }

    //Funci�n para iniciar la m�sica
    public void IniciarMusica(EventReference musicaRef)
    {
        musica = AudioManager.instance.CreateInstance(musicaRef);
        musica.start();
    }

    //Funci�n para seleccionar la m�sica usando el script MusicTrack
    public void SeleccionarMusica(MusicTrack track)
    {
        musica.setParameterByName("Cambio de m�sica", (float)track);
    }

    //Funci�n para detener la m�sica
    public void DetenerReanudarMusica(bool b)
    {
        if (b) { musica.start(); } else { musica.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); }
    }

    //Manejo del volumen de la m�sica
    public void VolumenMusica(float f)
    {
        musica.setParameterByName("Intensidad", f);
    }

    //Sonido al iniciar  el menu
    public void IniciarMenu(EventReference menuRef)
    {
        menu = CreateInstance(menuRef);
        menu.setParameterByName("Menus", 0);
        menu.start();
    }

    //Funci�n para los sonidos del menu
    public void SonidoMenu(int valor)
    {
        menu.setParameterByName("Menus", valor);
        menu.start();
    }

    public void CerrarMenu() { menu.release(); }

    //Funci�n para el sonido del ambiente
    private void IniciarAmbiente(EventReference ambiente)
    {
        ambientaci�n = CreateInstance(ambiente);
        ambientaci�n.start();
    }

    //Funci�n para alterar el ambiente
    public void CambiarAmbiente(string parametro, float valor)
    {
        ambientaci�n.setParameterByName(parametro, valor);
    }
}
