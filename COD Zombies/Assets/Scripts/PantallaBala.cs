
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaBala : MonoBehaviour
{
    public Text magazineSizeText;
    public Text magazineCountText;
    public NewBehaviourScript logicaArma;
    public Image icon;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        magazineSizeText.text = logicaArma.balasEnCartucho.ToString();
        magazineCountText.text = logicaArma.balasRestantes.ToString();
        icon.sprite = logicaArma.icon;
    }
}
