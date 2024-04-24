using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
    public GameObject texto;
    public float distanciaMaxima = 2.0f;

    public void MostrarTexto()
    {
        texto.SetActive(true);
    }

    public void QuitarTexto()
    {
        texto.SetActive(false);
    }
}
