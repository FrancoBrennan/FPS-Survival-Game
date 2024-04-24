using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarBoton : MonoBehaviour
{
    public GameObject botonUI;
    public float distanciaMaxima = 2.0f;

    public void Mostrar()
    {
        botonUI.SetActive(true);
    }

    public void Quitar()
    {
        botonUI.SetActive(false);
    }
}
