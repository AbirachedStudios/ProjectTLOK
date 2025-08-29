using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientArea : MonoBehaviour
{
    public string nombre;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioManager.instance.CambiarAmbiente(nombre, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.CambiarAmbiente(nombre, 0);
        }
    }
}
