using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float valor;
    public Vida padreRef;
    public float multiplicadorDeDaño = 1.0f;
    public GameObject textoFlotantePrefab;
    public float dañoTotal;
    public float maxVida;
    

    // Use this for initialization
    void Start()
    {
        valor = maxVida = 100;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecibirDaño(float daño)
    {
        daño *= multiplicadorDeDaño;
        
        if(padreRef != null)
        {
            padreRef.RecibirDaño(daño);
            return;
        }

        valor -= daño;
        dañoTotal = daño;

        
        if (valor >= 0) MostrarTextoFlotante();

        if (valor < 0)
        {
            valor = 0;
            MostrarTextoFlotante();
        }
        
    }

    
    void MostrarTextoFlotante()
    {
        var go = Instantiate(textoFlotantePrefab,transform.position, Quaternion.identity,transform);
        go.GetComponent<TextMesh>().text = dañoTotal.ToString(); //De esta forma muestra la vida que le saca al zombie.

        //go.GetComponent<TextMesh>().text = valor.ToString(); De esta forma muestra la vida que le queda al zombie
    }
    

    public void Regenerar(float cantidad)
    {
        valor += cantidad;
        valor = Mathf.Min(valor, maxVida);
    }
}
