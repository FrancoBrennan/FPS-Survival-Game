using UnityEngine;
using System.Collections;

public class KraftAKick : MonoBehaviour
{
    //bool showPerkMachineGUI = false;
    public int KraftAKickPrice = 3000;
    public GameObject[] effectObjects;
    public GUISkin mySkin;
    public AudioClip vendingSound;
    bool inUse = false;
    float timer = 0f;
    public Puntaje killsPantalla;
    public ControladorDeArmas controlador;
    public UpgradeText upgradeText;
    public bool botonApretado=false;
    public MostrarBoton boton;
    

    // Use this for initialization
    void Start()
    {
        TriggerEffects(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inUse)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                TriggerEffects(false);
                inUse = false;
                timer = 0f;
            }
        }

    }

    /*
    void OnGUI()
    {
        GUI.skin = mySkin;
        GUIStyle style1 = mySkin.customStyles[0];
        if (showPerkMachineGUI)
        {
            GUI.Label(new Rect(Screen.width - (Screen.width / 1.7f),
                               Screen.height - (Screen.height / 1.4f), 800, 100),
                      "Press key << E >> to upgrade weapon  $" + KraftAKickPrice, style1);
        }

    }
    */

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !controlador.Mejoro())
        {
            upgradeText.MostrarTexto();
            boton.Mostrar();

            if (/*Input.GetKeyDown("e")*/ botonApretado == true && killsPantalla.GetComponent<Puntaje>().puntaje.valor >= KraftAKickPrice && !inUse && controlador.Mejoro() == false)
            {
                boton.Quitar();
                upgradeText.QuitarTexto();

                TriggerEffects(true);
                killsPantalla.GetComponent<Puntaje>().puntaje.valor -= KraftAKickPrice;
                inUse = true;

                GetComponent<AudioSource>().PlayOneShot(vendingSound, 1.0f / GetComponent<AudioSource>().volume);

                //ControladorDeArmas controladorDeArmas = other.GetComponentInChildren<ControladorDeArmas>();

                //if (controladorDeArmas != null)
                //{
                    // Llamar a la función en el "ControladorDeArmas"
                    controlador.MejoraDeArma();
                //}
                botonApretado = false;
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
            upgradeText.QuitarTexto();
            boton.Quitar();

        }
    }

    void TriggerEffects(bool set)
    {
        foreach (GameObject effects in effectObjects)
        {
            effects.SetActive(set);
        }
    }
}
