using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    private int baseValue;
    private int maxValue;

    [SerializeField] private Image fill;
    [SerializeField] private Text amount;

    public void SetValues(int valor, int maxValor)
    {
        baseValue = valor;
        maxValue = maxValor;

        amount.text = baseValue.ToString();

        CalculateFillAmount();
    }

    private void CalculateFillAmount()
    {
        float fillAmount = (float)baseValue / (float)maxValue;
        fill.fillAmount = fillAmount;
    }
}
