using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LogicaEnemigo : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agente;
    private Vida vida;
    private Animator animator;
    private Collider collider;
    private Vida vidaJugador;
    private LogicaJugador logicaJugador;
    public bool Vida0 = false;
    public bool estaAtacando = false;
    public bool estaAtacandoSpawnWall=false;
    public float speed = 1.0f;
    public float angularSpeed = 120;
    public float daño = 15;
    protected AudioSource audioSource;

    public bool mirando;
    public bool mirandoSpawnWall;
    public bool sumarPuntos = false;
    public GameObject killsPantalla;
   // bool behindWall;  //SpawnWall
    public GameObject cabeza;

    public AudioClip SonidoAtaque;

    //public float distanciaParaSaltar = 2.0f;
    //public float alturaMaximaParaSaltar = 1.5f;
    //public LayerMask objetoSaltable;

    GameManagement game;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.Find("Jugador");
        vidaJugador = target.GetComponent<Vida>();
        if (vidaJugador == null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente Vida");
        }

        logicaJugador = target.GetComponent<LogicaJugador>();

        if (logicaJugador == null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente LogicaJugador");
        }

        agente = GetComponent<NavMeshAgent>();
        vida = GetComponent<Vida>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();

        game = FindObjectOfType<GameManagement>();

        if (game.round == 2)
        {
            vida.valor = 150;
            daño = 20;
            //speed *= 2;
            //angularSpeed = 130;
        }
        else if (game.round == 3)
        {
            vida.valor = 200;
            daño = 30;
            //speed *= 1.5f;
            //angularSpeed = 140;
        }

    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
        Perseguir();
        RevisarAtaque();
        //RevisarAtaqueSpawnWall();
        EstaDefrenteAlJugador();
        //EstaDeFrenteAlSpawnWall();
    }

    /*
    public void DetectarSalto()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaParaSaltar, objetoSaltable))
        {
            if (hit.collider.bounds.size.y <= alturaMaximaParaSaltar)
            {
                animator.SetTrigger("DebeSaltar");
            }
        }
    }
    */

    void EstaDefrenteAlJugador()
    {
        Vector3 adelanteSpawnWall = transform.forward;
        Vector3 targetJugador = (GameObject.Find("Jugador").transform.position - transform.position).normalized;

        if (Vector3.Dot(adelanteSpawnWall, targetJugador) < 0.6f)
        {
            mirando = false;
        }
        else
        {
            mirando = true;
        }

    }

    /*
    void EstaDeFrenteAlSpawnWall()
    {
        Vector3 adelanteSpawnWall = transform.forward;
        Vector3 targetSpawnWall = (GameObject.Find("spawnWall").transform.position - transform.position).normalized;

        if (Vector3.Dot(adelanteSpawnWall, targetSpawnWall) < 0.6f)
        {
            mirandoSpawnWall = false;
        }
        else
        {
            mirandoSpawnWall = true;
        }
    }
    */

    void RevisarVida()
    {
        if (Vida0) return;
        if (vida.valor <= 0)
        { 
            
            sumarPuntos = true;

            if(sumarPuntos) 
            {
                killsPantalla.GetComponent<Puntaje>().valor += 150;

                sumarPuntos=false;
            }
            

            Vida0 = true;
            agente.isStopped = true;
            collider.enabled = false;
            animator.CrossFadeInFixedTime("Vida0", 0.1f);


            game.zombiesLeftInRound -= 1;
            Destroy(cabeza);
            Destroy(gameObject, 3f);
        }

    }

    void Perseguir()
    {
        if (Vida0) return;
        if (logicaJugador.Vida0) return;
        agente.destination = target.transform.position;
    }

    void RevisarAtaque()
    {
        if (Vida0) return;
        if (estaAtacando) return;
        if (logicaJugador.Vida0) return;
        float distanciaDelBlanco = Vector3.Distance(target.transform.position, transform.position);

        if (distanciaDelBlanco <= 2.0 && mirando)
        {
            Atacar();
        }
    }

    /*
    void RevisarAtaqueSpawnWall()
    {
        if(estaAtacandoSpawnWall) return;

        float distanciaDelBlancoSpawnWall = Vector3.Distance(target.transform.position, transform.position);

        if (distanciaDelBlancoSpawnWall <= 2.0 && mirandoSpawnWall)
        {
            AtacarSpawnWall();
        }
    }

    */

    void Atacar()
    {
        vidaJugador.RecibirDaño(daño);
        agente.speed = 0;
        agente.angularSpeed = 0;
        estaAtacando = true;
        logicaJugador.siendoAtacado();
        animator.SetTrigger("DebeAtacar");
        audioSource.PlayOneShot(SonidoAtaque);
        Invoke("ReiniciarAtaque", 2.5f);
    }

    /*
    void AtacarSpawnWall()
    {
        agente.speed = 0;
        agente.angularSpeed = 0;
        estaAtacandoSpawnWall = true;
        animator.SetTrigger("DebeAtacar");
        audioSource.PlayOneShot(SonidoAtaque);
        Invoke("ReiniciarAtaqueSpawnWall", 2.5f);
    }

    */

    void ReiniciarAtaque()
    {
        estaAtacando = false;
        agente.speed = speed;
        agente.angularSpeed = angularSpeed;
    }

    /*
    void ReiniciarAtaqueSpawnWall()
    {
        estaAtacandoSpawnWall = false;
        agente.speed = speed;
        agente.angularSpeed = angularSpeed;
    }


    
    void OnTriggerStay(Collider collide)
    {
        if(collide.gameObject.tag == "SpawnWall")
        {
            if (collide.gameObject.GetComponent<Renderer>().enabled)
            {
                
                behindWall = true;

                agente.Stop();
                
                if(estaAtacandoSpawnWall)
                {
                    collider.SendMessageUpwards("RemoveBoard", SendMessageOptions.RequireReceiver);
                }

            }
            else if(behindWall)
            {
                agente.Resume();
                behindWall =false;
                animator.SetTrigger("DebeSaltar");
            }
            
        }
    }
    */
}
