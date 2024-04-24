using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntaje : MonoBehaviour
{
    public float valor;
    public Puntaje puntaje;
    public Text texto;

    // Start is called before the first frame update
    void Start()
    {
        valor = 0;
        puntaje.valor = 15000;
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = "$" + puntaje.valor;
    }

    public float getValor()
    {
        return valor;
    }
}
