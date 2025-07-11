using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioMenu : MonoBehaviour, IPointerEnterHandler
{
    //public bool change;

    private void Awake()
    {
        AudioManager.instance.IniciarMenu(FMODEvents.instance.menu);
    }

    public void SonidoDeBotón(int i)
    {
        AudioManager.instance.SonidoMenu(i);
    }

    /*public void OnOff()
    {
        if (change)
        {
            AudioManager.instance.SonidoMenu(5);
        }else { AudioManager.instance.SonidoMenu(5); }
    }*/

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
        {
            AudioManager.instance.SonidoMenu(1);
        } else { AudioManager.instance.SonidoMenu(4); }
    }
}
