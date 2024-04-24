using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour
{
    public Vida vida;
    public bool Vida0 = false;
    [SerializeField] private Animator animacionPerder;
    [SerializeField] private Animator animacionGanar;
    public GameObject ganar;
    public GameObject perder;
    public Puntaje puntaje;
    public float tiempoParaRegenerar = 10f;  // Tiempo que tarda en regenerarse completamente (en segundos)
    public float cantidadDeRegeneracionPorSegundo = 5f;  // Cuánto se regenera por segundo
    private float tiempoUltimoAtaque = 0f;
    bool canRepairWall;
    float rebuildTimer = 0;
    //public PickUpText2 texto;
    public RepairWallScript text;

    // Use this for initialization
    void Start()
    {
        vida = GetComponent<Vida>();
        puntaje.valor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
    }

    void RevisarVida()
    {
        if (Vida0) return;

        if (vida.valor <= 0)
        {
            AudioListener.volume = 0f;
            perder.SetActive(true);
            animacionPerder.SetTrigger("Mostrar");
            Vida0 = true;
            Invoke("ReiniciarJuego", 3f);
        }
        else
        {
            if (Time.time - tiempoUltimoAtaque >= tiempoParaRegenerar)
            {
                // Regenerar progresivamente
                vida.Regenerar(cantidadDeRegeneracionPorSegundo * Time.deltaTime);
            }
        }
    }

    public void Ganaste()
    {
        AudioListener.volume = 0f;
        ganar.SetActive(true);
        animacionGanar.SetTrigger("Mostrar");
        Invoke("ReiniciarJuego", 3f);
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        puntaje.valor = 0;
        AudioListener.volume = 1f;
    }


    /*
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "SpawnWall")
        {
            rebuildTimer = Time.deltaTime;
            if(canRepairWall)
            {
                text.MostrarTexto();
                if(Input.GetKeyDown("e") && rebuildTimer > 1.0f) {
                    other.SendMessage("AddBoard", SendMessageOptions.RequireReceiver);

                    rebuildTimer = 0;
                }
            }
            text.QuitarTexto();
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnWall") {
            canRepairWall = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "SpawnWall")
        {
            canRepairWall=false;
            text.QuitarTexto();
        }    
    }

    */

    public void siendoAtacado()
    {
        tiempoUltimoAtaque = Time.time;
    }

    public void GivePlayerPerk(string nombre)
    {
        if(nombre == "Jug")
        {
            vida.valor = vida.maxVida = 200;
            cantidadDeRegeneracionPorSegundo = 10f;
        }
        
    }

}
