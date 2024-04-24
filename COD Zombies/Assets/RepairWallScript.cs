using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairWallScript : MonoBehaviour
{
    public GameObject textoUI;
    public float distanciaMaxima = 2.0f;

    public void MostrarTexto()
    {
        textoUI.SetActive(true);
    }

    public void QuitarTexto()
    {
        textoUI.SetActive(false);
    }
}
