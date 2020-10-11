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
    [SerializeField] List<GameObject> entryList;
    [SerializeField] Slider repSlider;

    //list value checking stuff
    public List<tradeItemScript> giveTISList, receiveTISList;

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
        if(entryList.Count != 0)
        {
            foreach(GameObject entry in entryList)
            {
                Destroy(entry);
            }
            entryList.Clear();
        }
        foreach(playerResources.InventoryEntry entry in pR.playerInventory)
        {
            var tIS = Instantiate(shopItemEntry, playerStuffZone.transform).GetComponent<tradeItemScript>();
            tIS.itemName.text = entry.entryType.ToString();
            tIS.myType = entry.entryType;
            tIS.valueSlider.minValue = 0;
            tIS.valueSlider.maxValue = entry.amountHeld;
            entryList.Add(tIS.gameObject);
            giveTISList.Add(tIS);
        }

        foreach(stockItem stock in currentShop.shopStock)
        {
            if(stock.stockAmount != 0)
            {
                var tIS = Instantiate(shopItemEntry, shopStuffZone.transform).GetComponent<tradeItemScript>();
                tIS.itemName.text = stock.stockType.ToString();
                tIS.myType = stock.stockType;
                tIS.valueSlider.minValue = 0;
                tIS.valueSlider.maxValue = stock.stockAmount;
                entryList.Add(tIS.gameObject);
                receiveTISList.Add(tIS);
            }
        }

        calculateValues(false);

    }

    public void calculateValues(bool trade)
    {
        float giveValue = 0, receiveValue = 0, repChange = 0;

        foreach(tradeItemScript tIS in giveTISList)
        {
            giveValue = giveValue + tIS.valueSlider.value;
            if(trade == true)
            {
                pR.giveItem(tIS.myType, tIS.valueSlider.value * -1);
                tIS.valueSlider.value = 0;
            }
        }
        foreach(tradeItemScript tIS in receiveTISList)
        {
            receiveValue = receiveValue + tIS.valueSlider.value;
            if(trade == true)
            {
                pR.giveItem(tIS.myType, tIS.valueSlider.value);
                tIS.valueSlider.value = 0;
            }
        }

        repChange = giveValue - receiveValue;
        
        if(trade == true)
        {
            pR.reputationChange(repChange, currentShop.faction);
            populateShopScreen();
        }
    }


    public void vendorChat()
    {
        shopDescription.text = currentShop.shopTalk[Random.Range(0, currentShop.shopTalk.Length)];
    }

}
