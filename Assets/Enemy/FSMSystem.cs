using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMSystem : MonoBehaviour //ESTO ES PARA ESTADOS
{
    //EN FSMstate SE AGREGA TRANSICIONES
    //ACA SE AGREGA ESTADOS
    //public List<Enemies> listaEstados;
    //public AIState estadoActualID;
    public Enemies estadoActual;

    private void Update()
    {
        RunFSM();
    }

    //public FSMSystem()
    //{
    //    listaEstados = new List<Enemies>();
    //}

    private void RunFSM()
    {
        Enemies next = estadoActual?.GetEnemies();

        if (next != null)
        {
            AgregarEstado(next);
        }
    }

    public void AgregarEstado(Enemies newState)
    {
        estadoActual = newState;
        //si el nuevo estado es nulo, entonces tira error
        //if (newState == null)
        //{
        //    Debug.LogError("ERROR: Se ingresó un estado nulo!");
        //    return;
        //}

        ////Si no existen estados, acá se va a agregar por primera vez uno y será el base.
        //if (listaEstados.Count == 0)
        //{
        //    listaEstados.Add(newState);
        //    estadoActual = newState;
        //    //estadoActualID = newState.ID;
        //    return; //esto es para salir de la funcion, debido que despues se agregaran más estados.
        //}

        //foreach (Enemies estado in listaEstados)
        //{
        //    if (estado.ID == newState.ID)
        //    {
        //        Debug.LogError("ERROR: Imposible agregar este estado, debido a que ya se realizo dicha accion.");
        //        return;
        //    }
        //}
        //listaEstados.Add(newState);
    }

    //public void BorrarEstado(AIState IDborrar)
    //{
    //    if (IDborrar == AIState.NullState) //Verifica si es nulo el estado ingresado
    //    {
    //        Debug.LogError("ERROR: El estado ingresado es nulo");
    //        return;
    //    }

    //    foreach (Enemies estado in listaEstados)
    //    {
    //        if (estado.ID == IDborrar) //verifica que el ID ingresado exista, y en base a eso ahí borra
    //        {
    //            listaEstados.Remove(estado);
    //            return;
    //        }
    //    }
    //    Debug.LogError("ERROR: El ID ingresado no se puede borrar. No existe?");
    //}

    //public void HacerTransicion(Transicion link)
    //{
    //    if (link == Transicion.NullTransition)
    //    {
    //        Debug.LogError("ERROR: La transicion que está intentando hacer es incorrecta");
    //        return;
    //    }

    //    AIState changeState = estadoActual.GetOutput(link);
    //    if (changeState == AIState.NullState)
    //    {
    //        Debug.Log(link);
    //        Debug.Log(changeState);
    //        Debug.LogError("ERROR: No se puede hacer la transicion, el estado es nulo");
    //        return;
    //    }

    //    estadoActualID = changeState;
    //    foreach (Enemies estado in listaEstados)
    //    {
    //        //ACA SE CAMBIA EL ESTADO
    //        if (estado.ID == estadoActualID)
    //        {
    //            estadoActual = estado;
    //        }
    //    }
    //}
}

