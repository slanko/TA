using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopScript : MonoBehaviour
{
    shopData currentShop;
    [SerializeField] cityDataPassthrough cDP;
    [SerializeField] playerResources pR;

    [SerializeField] Text vendorNameText, vendorNameText2, shopDescription, shopFlavourText;
    [SerializeField] Image vendorSprite;

    [SerializeField] GameObject shopItemEntry, playerStuffZone, shopStuffZone;

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

        //BUTTON STUFF OH GOD
       
        //player stuff section
        foreach(playerResources.InventoryEntry entry in pR.playerInventory)
        {
            var newButton = Instantiate(shopItemEntry, playerStuffZone.transform);
            tradeItemScript tIS = newButton.GetComponent<tradeItemScript>();
            tIS.itemName.text = entry.entryType.ToString();
            tIS.valueSlider.minValue = 0;
            tIS.valueSlider.maxValue = entry.amountHeld;
        }

    }

    public void vendorChat()
    {
        shopDescription.text = currentShop.shopTalk[Random.Range(0, currentShop.shopTalk.Length)];
    }
}
