using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum ModoDeDisparo
{
    SemiAuto,
    FullAuto
}

public class NewBehaviourScript : MonoBehaviour
{

    protected Animator animator;
    protected AudioSource audioSource;
    public bool tiempoNoDisparo = false;
    public bool puedeDisparar = false;
    public bool recargando = false;
    public bool armaYaMejorada=false;
    public bool player1HasDoubleTap = false;

    [Header("Referencia de Objetos")]
    public ParticleSystem fuegoDeArma;
    public Camera camaraPrincipal;
    public Transform puntoDeDisparo;
    public GameObject efectoDañoPrefab;
    public Sprite icon;  //Agregado para el tema del weaponUI y PantallaBalas

    [Header("Referencia de Sonido")]
    public AudioClip SonidoDeDisparo;
    public AudioClip SonidoSinBalas;
    public AudioClip SonidoCartuchoEntra;
    public AudioClip SonidoCartuchoSale;
    public AudioClip SonidoVacio;
    public AudioClip SonidoDeDesenfundado;

    [Header("Atributos de Arma")]
    public ModoDeDisparo modoDeDisparo = ModoDeDisparo.SemiAuto;
    public float daño;
    public float dañoInicial;
    public float ritmoDeDisparo;
    public float ritmoDeDisparoInicial;
    public int balasRestantes;
    public int balasEnCartucho;
    public int tamañoDeCartucho;
    public int tamañoDeCartuchoInicial; //Agregado
    public int maxBalas;
    public int maxBalasInicial; //Agregado
    public bool estaADS = false;
    public Vector3 disCadera;
    public Vector3 ADS;
    public float tiempoApuntar;
    public float zoom;
    public float normal;

    public bool disparoAuto;

    // Start is called before the first frame update
    void Start()
    {
        disparoAuto = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        balasEnCartucho = tamañoDeCartuchoInicial;
        balasRestantes = maxBalasInicial;

        Invoke("HabilitarArma", 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (modoDeDisparo == ModoDeDisparo.FullAuto && Input.GetButton("Fire1"))
        {
            RevisarDisparo();
        }
        else if (modoDeDisparo == ModoDeDisparo.SemiAuto && Input.GetButtonDown("Fire1"))
        {
            RevisarDisparo();
        }
        
        

        if (disparoAuto == true)
        {
            RevisarDisparo();
        }

        if (Input.GetButtonDown("Reload"))
        {
            RevisarRecarga();
        }

        
        
        if (Input.GetMouseButton(1)) // El 1 hace referencia al click derecho y 0 al click izquierdo
        {
            //Esto lo que hace es que al presionar click derecho transforme la posici�n del arma
            //en la posici�n ADS y el tiempo que va a demorar.
            //El Vector3.Slerp es para que ocurra la transici�n del apuntado.

            transform.localPosition = Vector3.Slerp(transform.localPosition, ADS, tiempoApuntar * Time.deltaTime);
            estaADS = true;
            camaraPrincipal.fieldOfView = Mathf.Lerp(camaraPrincipal.fieldOfView, zoom, tiempoApuntar * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(1))
        {
            estaADS = false;
        }

        

        if (estaADS == false)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, disCadera, tiempoApuntar * Time.deltaTime);
            camaraPrincipal.fieldOfView = Mathf.Lerp(camaraPrincipal.fieldOfView, normal, tiempoApuntar * Time.deltaTime);
        }

        if (estaADS == true)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, ADS, tiempoApuntar * Time.deltaTime);
            
            camaraPrincipal.fieldOfView = Mathf.Lerp(camaraPrincipal.fieldOfView, zoom, tiempoApuntar * Time.deltaTime);
        }


    }


    public void ReiniciarBalasYCargador()
    {
        balasEnCartucho = tamañoDeCartuchoInicial;
        balasRestantes = maxBalasInicial;
        daño = dañoInicial;
        ritmoDeDisparo = ritmoDeDisparoInicial;
        player1HasDoubleTap = false;
        armaYaMejorada = false;
    }

    void HabilitarArma()
    {
        puedeDisparar = true;
    }

    void RevisarDisparo()
    {
        if (!puedeDisparar) return;

        if (tiempoNoDisparo) return;

        if (recargando) return;

        if (balasEnCartucho > 0)
        {
            Disparar();
        }
        else
        {
            SinBalas();
        }
    }

    void Disparar()
    {
        audioSource.PlayOneShot(SonidoDeDisparo);
        tiempoNoDisparo = true;
        fuegoDeArma.Stop();
        fuegoDeArma.Play();
        ReproducirAnimacionDisparo();
        balasEnCartucho--;
        StartCoroutine(ReiniciarTiempoNoDisparo());
        DisparoDirecto();
    }

    public void CrearEfectoDaño(Vector3 pos, Quaternion rot)
    {
        GameObject efectoDaño = Instantiate(efectoDañoPrefab, pos, rot);
        Destroy(efectoDaño, 1f);
    }

    void DisparoDirecto()
    {
        RaycastHit hit; //Punto en que va a chocar la bala con lo primero que toca, en este caso un zombie.


        if (Physics.Raycast(puntoDeDisparo.position, puntoDeDisparo.forward, out hit))
        {
            if (hit.transform.CompareTag("Enemigo"))
            {

                float dañoInfligido = daño; // Por defecto, el daño es el normal

                // Verificar si el arma es una escopeta, un subfusil o una MP5
                if (gameObject.name == "DefenderShotgun" || gameObject.name == "Compact9mm" || gameObject.name == "UMP45")
                {
                    // Obtener la distancia entre el jugador y el enemigo
                    float distancia = Vector3.Distance(transform.position, hit.transform.position);

                    // Calcular un factor de reducción de daño basado en la distancia
                    float factorDeReduccion = 1f; // Por defecto, no hay reducción de daño

                    // Aquí puedes ajustar los valores según tu preferencia
                    if (distancia > 10f) // Por ejemplo, si la distancia es mayor a 10 unidades, se reduce el daño
                    {
                        if(gameObject.name == "DefenderShotgun")
                        {
                            factorDeReduccion = 0.5f; // Reducir el daño a la mitad
                        }
                        else
                        {
                            factorDeReduccion = 0.75f; // Reducir el daño a 3/4
                        }
                        
                    }
                    else if (distancia > 5f && gameObject.name == "DefenderShotgun") // Otra distancia para aplicar otra reducción
                    {
                        factorDeReduccion = 0.75f; // Reducir el daño a 3/4
                    }

                    // Aplicar el factor de reducción al daño que se va a infligir al enemigo
                    dañoInfligido = daño * factorDeReduccion;
                }

                // Aplicar el daño modificado
                Vida vida = hit.transform.GetComponent<Vida>();
                if (vida == null)
                {
                    throw new System.Exception("No se encontro el componente Vida del Enemigo");
                }
                else
                {
                    vida.RecibirDaño((int)dañoInfligido);
                    CrearEfectoDaño(hit.point, hit.transform.rotation);
                }
            }
        }
    }

    public virtual void ReproducirAnimacionDisparo()
    {
        if (gameObject.name == "Police9mm")
        {
            if (balasEnCartucho > 1)
            {
                animator.CrossFadeInFixedTime("Fire", 0.1f);
            }
            else
            {
                animator.CrossFadeInFixedTime("FireLast", 0.1f);
            }
        }
        else
        {
            animator.CrossFadeInFixedTime("Fire", 0.1f);
        }


    }

    void SinBalas()
    {
        audioSource.PlayOneShot(SonidoSinBalas);
        tiempoNoDisparo = true;
        StartCoroutine(ReiniciarTiempoNoDisparo());
    }

    IEnumerator ReiniciarTiempoNoDisparo()
    {
        yield return new WaitForSeconds(ritmoDeDisparo);
        tiempoNoDisparo = false;
    }

    public void RevisarRecarga()
    {
        if (balasRestantes > 0 && balasEnCartucho < tamañoDeCartucho)
        {
            Recargar();
        }
    }

    void Recargar()
    {
        if (recargando) return;
        recargando = true;
        
        if(gameObject.name == "DefenderShotgun")
        {
            animator.CrossFadeInFixedTime("ReloadStart", 0.1f);
            StartCoroutine(RecargarDefenderShotgun());
            animator.CrossFadeInFixedTime("ReloadEnd", 0.1f);
        }
        else
        {
            animator.CrossFadeInFixedTime("Reload", 0.1f);
        }
        
    }

    IEnumerator RecargarDefenderShotgun()
    {
        

        while (balasEnCartucho < tamañoDeCartucho && balasRestantes > 0)
        {
            animator.CrossFadeInFixedTime("ReloadInsert", 0.1f);
            balasEnCartucho++;
            balasRestantes--;
            yield return new WaitForSeconds(1f); // Ajusta el tiempo entre cada recarga
        }

        
        recargando = false;
    }

    void RecargarMuniciones()
    {
        int balasParaRecargar = tamañoDeCartucho - balasEnCartucho;
        int restarBalas = (balasRestantes >= balasParaRecargar) ? balasParaRecargar : balasRestantes;
        //Es como un IF, si balas restantes es mayor igual que las balas para recargar el restar balas se vuelve el balas para recargar.
        // Sino, si es menor el restar balas se vuelve balas restantes.
        //El ? lo convierte en un IF, y los : es el ELSE.


        balasRestantes -= restarBalas;
        balasEnCartucho += balasParaRecargar;
    }

    public void DesenfundarOn()
    {
        audioSource.PlayOneShot(SonidoDeDesenfundado);
    }

    public void CartuchoEntraOn()
    {
        audioSource.PlayOneShot(SonidoCartuchoEntra);
        RecargarMuniciones();
    }

    public void CartuchoEntraOnEscopeta()
    {
        audioSource.PlayOneShot(SonidoCartuchoEntra);
    }

    public void CartuchoSaleOn()
    {
        audioSource.PlayOneShot(SonidoCartuchoSale);
    }

    public void VacioOn()
    {
        audioSource.PlayOneShot(SonidoVacio);
        Invoke("ReiniciarRecargar", 0.1f);
    }

    void ReiniciarRecargar()
    {
        recargando = false;
    }

    public void armaMejorada()
    {
        armaYaMejorada = true;
    }

    public void MejorarRitmoDisparo()
    {
        ritmoDeDisparo *= 0.8f;
        player1HasDoubleTap = true;
    }

    public void Mejorar()
    {
        daño*=2;
        ritmoDeDisparo*=0.8f;

        tamañoDeCartucho *= 2;
        balasEnCartucho = tamañoDeCartucho;

        balasRestantes = maxBalas;
        balasRestantes *= 2;

        armaYaMejorada = true;
    }


    public bool getArmaYaMejorada()
    {
        return armaYaMejorada;
    }
    public bool getPlayer1HasDoubleTap()
    {
        return player1HasDoubleTap;
    }

    public void DispararArma()
    {
        if (modoDeDisparo == ModoDeDisparo.FullAuto && Input.GetButton("Fire1"))
        {
            RevisarDisparo();
        }
        else if (modoDeDisparo == ModoDeDisparo.SemiAuto && Input.GetButtonDown("Fire1"))
        {
            RevisarDisparo();
        }
    }

   public void BotonApretado()
    {
        disparoAuto = true;
    }

    public void BotonSuelto()
    {
        disparoAuto = false;
    }

    public void BotonApretadoADS()
    {
        estaADS = true;
    }

    public void BotonSueltoADS()
    {
        estaADS = false;
    }
}
    
        
    

