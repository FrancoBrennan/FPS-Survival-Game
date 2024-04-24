using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaRonda : MonoBehaviour
{
    public Text texto;
    GameManagement game;


    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = "Ronda " + game.getRound();
    }
}
