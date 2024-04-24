using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public int round = 1;
    int zombiesInRound = 10;
    public int zombiesLeftInRound = 10;
    int zombiesSpawnedInRound = 0;
    float zombieSpawnTimer = 0;
    public Transform[] zombieSpawnPoints;
    public GameObject zombieEnemy;
    public int tiempo = 3;
    public bool player1HasJug = false;
    public LogicaJugador jugador;
    bool terminarJuego;

    float countDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        terminarJuego = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (terminarJuego == false)
        {
            if (zombiesSpawnedInRound < zombiesInRound && countDown == 0)
            {
                if (zombieSpawnTimer > tiempo)
                {
                    SpawnZombie();
                    zombieSpawnTimer = 0;
                }
                else
                {
                    zombieSpawnTimer += Time.deltaTime;
                }
            }
            else if (zombiesLeftInRound == 0)
            {
                StartNextRound();
            }

            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                countDown = 0;
            }
        }
        else
        {
            jugador.Ganaste();
        }
        

        
    }

    void SpawnZombie()
    {
        Vector3 randomSpawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length)].position;
        Instantiate(zombieEnemy, randomSpawnPoint, Quaternion.identity);
        zombiesSpawnedInRound++;
    }

    void StartNextRound()
    {
        
        if(round == 3)
        {
            terminarJuego = true;
        }
        else
        {
            countDown = 15;
            zombiesInRound = zombiesLeftInRound += 10;
            zombiesSpawnedInRound = 0;
            round++;
            tiempo--;
        }
        
        
    }

    public int getRound()
    {
        return round;
    }
}
