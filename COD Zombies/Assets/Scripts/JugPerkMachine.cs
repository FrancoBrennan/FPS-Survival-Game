using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugPerkMachine : MonoBehaviour
{
    public int jugPerkPrice=2500;
    public AudioClip vendingSound;
    public Puntaje killsPantalla;
    GameManagement game;
    public PickUpText2 pickUpText;
    public MostrarBoton boton;
    public bool botonApretado=false;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && !game.player1HasJug)
        {
            pickUpText.MostrarTexto();
            boton.Mostrar();
            if(/*Input.GetKeyDown("e")*/ botonApretado == true && killsPantalla.GetComponent<Puntaje>().puntaje.valor >= jugPerkPrice)
            {
                killsPantalla.GetComponent<Puntaje>().puntaje.valor -= jugPerkPrice;
                game.player1HasJug = true;

                GetComponent<AudioSource>().PlayOneShot(vendingSound, 1.0f / GetComponent<AudioSource>().volume);

                other.SendMessage("GivePlayerPerk", "Jug");

                pickUpText.QuitarTexto();
                boton.Quitar();
                botonApretado = false;
            }
        }
    }

    public void BotonApretado()
    {
        botonApretado= true;
    }

    public void BotonSuelto()
    {
        botonApretado = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //showMysteryBoxGUI = false;
            
            pickUpText.QuitarTexto();
            boton.Quitar();

        }
    }
}
