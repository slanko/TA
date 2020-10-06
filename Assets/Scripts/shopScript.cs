using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopScript : MonoBehaviour
{
    shopData currentShop;
    [SerializeField] cityDataPassthrough cDP;

    [SerializeField] Text vendorNameText, vendorNameText2, shopDescription, shopFlavourText;
    [SerializeField] Image vendorSprite;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            populateShopScreen();
        }
    }

    public void populateShopScreen()
    {
        currentShop = cDP.currentCity.cityLD.tradeArea;
        vendorNameText.text = currentShop.shopOwnerName;
        vendorNameText2.text = currentShop.shopOwnerName;
        shopDescription.text = currentShop.shopDescription;
        shopFlavourText.text = currentShop.shopFlavourText;
        vendorSprite.sprite = currentShop.vendorSprite;

    }

    public void vendorChat()
    {
        shopDescription.text = currentShop.shopTalk[Random.Range(0, currentShop.shopTalk.Length)];
    }
}
