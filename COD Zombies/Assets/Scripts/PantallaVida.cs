using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaVida : MonoBehaviour
{
    public Vida vida;
    [SerializeField] private BarraVida barraVida;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.SetValues((int)vida.valor,(int)vida.maxVida);
    }
}
