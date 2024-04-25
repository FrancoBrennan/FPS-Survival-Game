using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PantallaTimeRemaining : MonoBehaviour
{
    public GameObject textoUI;
    public Text texto;
    GameManagement game;

    void Start()
    {
        game = FindObjectOfType<GameManagement>();
    }

    public void MostrarTexto()
    {
        textoUI.SetActive(true);
    }

    public void QuitarTexto()
    {
        textoUI.SetActive(false);
    }

     void Update()
    {
        texto.text = "Next Round in... " + (int)game.getCountDown();
    }
}
