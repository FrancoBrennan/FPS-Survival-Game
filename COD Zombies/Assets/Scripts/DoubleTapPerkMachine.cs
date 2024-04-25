using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapPerkMachine : MonoBehaviour
{

    public int DoubleTapPerkPrice = 3500;
    public AudioClip vendingSound;
    public Puntaje killsPantalla;
    public ControladorDeArmas controlador;
    public PickUpText2 texto;
    //public MostrarBoton boton;
    public bool botonApretado=false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !controlador.Tomo())
        {
            texto.MostrarTexto();
            //boton.Mostrar();

            if (Input.GetKeyDown("e") /*botonApretado == true*/ && killsPantalla.GetComponent<Puntaje>().puntaje.valor >= DoubleTapPerkPrice && controlador.Tomo() == false)
            {
                killsPantalla.GetComponent<Puntaje>().puntaje.valor -= DoubleTapPerkPrice;

                GetComponent<AudioSource>().PlayOneShot(vendingSound, 1.0f / GetComponent<AudioSource>().volume);

                //ControladorDeArmas controladorDeArmas = other.GetComponentInChildren<ControladorDeArmas>();

                //if (controladorDeArmas != null)
                //{
                    // Llamar a la funci�n en el "ControladorDeArmas"
                controlador.GiveGunDoubleTap();
                texto.QuitarTexto();
                //boton.Quitar();
                botonApretado = false;
                //}
            }
        }
    }

    public void BotonApretado()
    {
        botonApretado = true;
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

            texto.QuitarTexto();
            //boton.Quitar();



        }
    }
}
