using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuantityController : MonoBehaviour
{
    public TMP_InputField quantityInput;
    public Button increase;
    public Button decrease;
    private int quantity = 0;
    private int maxQuantity = 99;

    void Start()
    {
        UpdateDisplay();
        increase.onClick.AddListener(IncreaseQuantity);
        decrease.onClick.AddListener(DecreaseQuantity);
    }

    private void IncreaseQuantity()
    {
        if(quantity<maxQuantity)
        {
            quantity++;
            UpdateDisplay();
        }
    }
    private void DecreaseQuantity()
    {
        if (quantity > 0)
        {
            quantity--;
            UpdateDisplay();
        }
        else if (quantity < 0)
        {
            quantity = 0;
        }
    }

    private void UpdateDisplay()
    {
        quantityInput.text = quantity.ToString();
    }
}
