using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuantityController : MonoBehaviour
{
    public TMP_InputField quantityInput;
    public Button increase;
    public Button decrease;
    public Button confirm;
    private int quantity = 0;
    private int maxQuantity = 99;

    public event Action<int> DeliverQuantity;


    void Start()
    {
        UpdateDisplay();
        increase.onClick.AddListener(IncreaseQuantity);
        decrease.onClick.AddListener(DecreaseQuantity);
        confirm.onClick.AddListener(ConfirmQuantity);
    }

    private void IncreaseQuantity()
    {
        Debug.Log("Increase");

        if (quantity<maxQuantity)
        {
            quantity++;
            Debug.Log(quantity);
            UpdateDisplay();
        }
    }
    private void DecreaseQuantity()
    {
        Debug.Log("Decrease");
        if (quantity > 0)
        {
            quantity--;
            Debug.Log(quantity);
            UpdateDisplay();
        }
    }
    private void UpdateDisplay()
    {
        quantityInput.text = quantity.ToString();
    }
    
    private void ConfirmQuantity()
    {
        DeliverQuantity?.Invoke(quantity);
        quantity = 0;
        UpdateDisplay();
        this.gameObject.SetActive(false);
    }

    public void SetMaxQuantity(int newMax)
    {
        maxQuantity = newMax;
    }

    //public override void Initialize()
    //{
    //}

    //public override void UpdateUI()
    //{
    //}
}
