using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopScript : MonoBehaviour
{
    shopData currentShop;
    public globalValuesData globalValueFile;
    [SerializeField] cityDataPassthrough cDP;
    [SerializeField] playerResources pR;

    [SerializeField] Text vendorNameText, vendorNameText2, shopDescription, shopFlavourText;
    [SerializeField] Image vendorSprite;

    [SerializeField] GameObject shopItemEntry, playerStuffZone, shopStuffZone;
    [SerializeField] List<GameObject> entryList;
    [SerializeField] Slider repSlider;
    [SerializeField] Text repChangeText, currentRepText;

    //list value checking stuff
    public List<tradeItemScript> giveTISList, receiveTISList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            populateShopScreen(false);
        }
        if(currentShop != null)
        {
            calculateValues(false);
        }
    }

    public void populateShopScreen(bool resetText)
    {
        //initialization
        currentShop = cDP.currentCity.cityLD.tradeArea;
        vendorNameText.text = currentShop.shopOwnerName;
        vendorNameText2.text = currentShop.shopOwnerName;
        if (resetText)
        {
            setShopTextBasedOnRep();
        }


        //BUTTON STUFF OH GOD

        //player stuff section
        if (entryList.Count != 0)
        {
            foreach(GameObject entry in entryList)
            {
                Destroy(entry);
            }
            entryList.Clear();
        }
        if(giveTISList.Count != 0)
        {
            giveTISList.Clear();
        }
        if(receiveTISList.Count != 0)
        {
            receiveTISList.Clear();
        }
        foreach(playerResources.InventoryEntry entry in pR.playerInventory)
        {
            if(entry.amountHeld > 0)
            {
                var tIS = Instantiate(shopItemEntry, playerStuffZone.transform).GetComponent<tradeItemScript>();
                tIS.itemName.text = entry.entryType.ToString();
                tIS.myType = entry.entryType;
                tIS.valueSlider.minValue = 0;
                tIS.valueSlider.maxValue = entry.amountHeld;
                entryList.Add(tIS.gameObject);
                giveTISList.Add(tIS);
            }
        } 

        foreach(stockItem stock in currentShop.GetShopStock())
        {
            if(stock.stockAmount > 0)
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
    }

    public void calculateValues(bool trade)
    {
        float giveValue = 0, receiveValue = 0, repChange = 0;

        foreach(tradeItemScript tIS in giveTISList)
        {
            int globalItemValue;
            //grab associated value
            for(int i = 0; i < globalValueFile.globalValues.Length; i++)
            {
                if(globalValueFile.globalValues[i].item == tIS.myType)
                {
                    globalItemValue = globalValueFile.globalValues[i].itemValue;
                    giveValue = giveValue + ((int)tIS.valueSlider.value * (int)globalItemValue);
                }
            }

            if(trade == true)
            {
                if(tIS.valueSlider.value != 0)
                {
                    pR.giveItem(tIS.myType, tIS.valueSlider.value * -1);
                    changeStock(tIS.myType, (int)tIS.valueSlider.value, true);
                    Debug.Log("changed " + tIS.myType + " by " + tIS.valueSlider.value);
                    tIS.valueSlider.value = 0;
                }
            }
        }
        foreach (tradeItemScript tIS in receiveTISList)
        {
            int globalItemValue;
            for (int i = 0; i < globalValueFile.globalValues.Length; i++)
            {
                if ((globalValueFile.globalValues[i].item == tIS.myType))
                {
                    globalItemValue = globalValueFile.globalValues[i].itemValue;
                    receiveValue = receiveValue + ((int)tIS.valueSlider.value * (int)globalItemValue);
                }
            }

            if(trade == true)
            {
                if(tIS.valueSlider.value != 0)
                {
                    pR.giveItem(tIS.myType, tIS.valueSlider.value);
                    changeStock(tIS.myType, (int)tIS.valueSlider.value, false);
                    Debug.Log("changed " + tIS.myType + " by " + tIS.valueSlider.value);
                    tIS.valueSlider.value = 0;
                }
            }
        }

        repChange = (giveValue - receiveValue) * globalValueFile.globalRepMult;
        repChange = (int)repChange;
        //set slider to correct faction amount, + or - rep change
        switch (currentShop.faction)
        {
            case globalValuesData.factionType.BANDIT:
                repSlider.value = pR.banditRep + repChange;
                currentRepText.text = pR.banditRep.ToString();
                break;

            case globalValuesData.factionType.FREETRADE:
                repSlider.value = pR.freeTradeRep + repChange;
                currentRepText.text = pR.freeTradeRep.ToString();
                break;

            case globalValuesData.factionType.CORPORATION:
                repSlider.value = pR.corporationRep + repChange;
                currentRepText.text = pR.corporationRep.ToString();
                break;

            case globalValuesData.factionType.FACTIONLESS:
                repSlider.value = pR.globalRep + repChange;
                currentRepText.text = pR.globalRep.ToString();
                break;
        }
        if(repChange > 0)
        {
            repChangeText.text = "+" + repChange.ToString();
        }
        else
        {
            repChangeText.text = repChange.ToString();
        }
        if(trade == true)
        {
            pR.reputationChange((int)repChange, currentShop.faction);
            populateShopScreen(false);
        }
    }

    public void vendorChat()
    {
        shopDescription.text = currentShop.shopTalk[Random.Range(0, currentShop.shopTalk.Length)];
    }

    void changeStock(globalValuesData.itemType myType, int amount, bool addOrSubtract)
    {
        bool addNewStock = true;
        if (addOrSubtract == false)
        {
            addNewStock = false;
        }
        var allStock = currentShop.GetShopStock();
        var removedList = new List<stockItem>();
        for (int i = 0; i < allStock.Count; i++)
        {
            stockItem stock = allStock[i];
            if(stock.stockType == myType)
            {
                addNewStock = false;
                Debug.Log("found stock of type " + myType);
                if(addOrSubtract)
                {
                    stock.stockAmount = stock.stockAmount + amount;
                }
                else
                {
                    stock.stockAmount = stock.stockAmount - amount;
                    Debug.Log(stock.stockType + " " + stock.stockAmount);
                    if(stock.stockAmount <= 0)
                    {
                        removedList.Add(allStock[i]);
                    }
                }
            }
        }
        foreach (var stock in removedList)
        {
            allStock.Remove(stock);
        }
        if (addNewStock == true)
        {
            stockItem newStock = new stockItem();
            newStock.stockType = myType;
            newStock.stockAmount = amount;
            allStock.Add(newStock);
        }
        currentShop.SetShopStock(allStock);
    }

    void setShopTextBasedOnRep()
    {
        switch (currentShop.faction)
        {
            case globalValuesData.factionType.BANDIT:
                if (pR.banditRep >= 150)
                {
                    shopDescription.text = currentShop.friendlyShopDescription;
                    shopFlavourText.text = currentShop.friendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (pR.banditRep < 150 && pR.banditRep > -150)
                {
                    shopDescription.text = currentShop.shopDescription;
                    shopFlavourText.text = currentShop.shopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (pR.banditRep <= -150)
                {
                    shopDescription.text = currentShop.unfriendlyShopDescription;
                    shopFlavourText.text = currentShop.unfriendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                break;
            case globalValuesData.factionType.CORPORATION:
                if (pR.corporationRep >= 150)
                {
                    shopDescription.text = currentShop.friendlyShopDescription;
                    shopFlavourText.text = currentShop.friendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (pR.corporationRep < 150 && pR.corporationRep > -150)
                {
                    shopDescription.text = currentShop.shopDescription;
                    shopFlavourText.text = currentShop.shopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (pR.corporationRep <= -150)
                {
                    shopDescription.text = currentShop.unfriendlyShopDescription;
                    shopFlavourText.text = currentShop.unfriendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                break;
            case globalValuesData.factionType.FREETRADE:
                if (pR.freeTradeRep >= 150)
                {
                    shopDescription.text = currentShop.friendlyShopDescription;
                    shopFlavourText.text = currentShop.friendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (pR.freeTradeRep < 150 && pR.freeTradeRep > -150)
                {
                    shopDescription.text = currentShop.shopDescription;
                    shopFlavourText.text = currentShop.shopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (pR.freeTradeRep <= -150)
                {
                    shopDescription.text = currentShop.unfriendlyShopDescription;
                    shopFlavourText.text = currentShop.unfriendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                break;
            case globalValuesData.factionType.FACTIONLESS:
                if (pR.globalRep >= 150)
                {
                    shopDescription.text = currentShop.friendlyShopDescription;
                    shopFlavourText.text = currentShop.friendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (pR.globalRep < 150 && pR.globalRep > -150)
                {
                    shopDescription.text = currentShop.shopDescription;
                    shopFlavourText.text = currentShop.shopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (pR.globalRep <= -150)
                {
                    shopDescription.text = currentShop.unfriendlyShopDescription;
                    shopFlavourText.text = currentShop.unfriendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                break;
        }
    }

}