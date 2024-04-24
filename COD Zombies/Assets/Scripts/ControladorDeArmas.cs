using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControladorDeArmas : MonoBehaviour
{
    public NewBehaviourScript[] armas;  //Se refiere al archivo de LogicaArma.
    public int indiceArmaActual;

    // Start is called before the first frame update
    void Start()
    {
        actualizarIndiceArmaActual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CambiarArmaActual( int indiceNuevaArma)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        indiceArmaActual = indiceNuevaArma;

        armas[indiceArmaActual].gameObject.SetActive(true);

    }

    public void actualizarIndiceArmaActual()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            
        }

        armas[indiceArmaActual].gameObject.SetActive(true);
    }

    public void GiveGunDoubleTap()
    {
        armas[indiceArmaActual].MejorarRitmoDisparo();
        
    }

    public void MejoraDeArma()
    {
        armas[indiceArmaActual].Mejorar();
    }

    public bool Tomo()
    {
        return armas[indiceArmaActual].getPlayer1HasDoubleTap();
    }

    public bool Mejoro()
    {
        return armas[indiceArmaActual].getArmaYaMejorada();
    }

    public void ReiniciarArmaActual()
    {
        armas[indiceArmaActual].ReiniciarBalasYCargador();
    }

    /*

    void RevisarCambioDeArma()
    {
        float ruedaMouse = Input.GetAxis("Mouse ScrollWheel");

        if(ruedaMouse > 0f ) 
        {
            SeleccionarArmaAnterior();
            armas[indiceArmaActual].recargando = false;
            armas[indiceArmaActual].tiempoNoDisparo = false;
            armas[indiceArmaActual].estaADS = false;
        }
        else if(ruedaMouse < 0f ) 
        {
            SeleccionarArmaSiguiente();
            armas[indiceArmaActual].recargando = false;
            armas[indiceArmaActual].tiempoNoDisparo = false;
            armas[indiceArmaActual].estaADS = false;
        }
    }

    
    void SeleccionarArmaAnterior()
    {
        if(indiceArmaActual == 0)
        {
            indiceArmaActual = armas.Length - 1;
        }
        else
        {
            indiceArmaActual--;
        }

        CambiarArmaActual();

    }


    void SeleccionarArmaSiguiente()
    {
        if (indiceArmaActual >= (armas.Length-1))
        {
            indiceArmaActual = 0;
        }
        else
        {
            indiceArmaActual++;
        }

        CambiarArmaActual();

    }
    */

}
