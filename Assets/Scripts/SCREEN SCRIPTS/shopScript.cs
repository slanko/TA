using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopScript : MonoBehaviour
{
    godPointToThing gPTT;

    shopData currentShop;
    public globalValuesData globalValueFile;
    [SerializeField] cityDataPassthrough cDP;

    [SerializeField] Text vendorNameText, vendorNameText2, shopDescription, shopFlavourText;
    [SerializeField] Image vendorSprite;

    [SerializeField] GameObject shopItemEntry, playerStuffZone, shopStuffZone;
    [SerializeField] List<GameObject> entryList;
    [SerializeField] Slider repSlider;
    [SerializeField] Text repChangeText, currentRepText;
    [SerializeField] Button tradeButton;
    int currentChatter = 0;

    //list value checking stuff
    public List<tradeItemScript> giveTISList, receiveTISList;
    private void Awake()
    {
        gPTT = GameObject.Find("GOD").GetComponent<godPointToThing>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            populateShopScreen(false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            gPTT.PLAYER.currentCity.restocker.restockShop();
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
        tradeButton.interactable = true;
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
        foreach(playerResources.InventoryEntry entry in gPTT.pR.playerInventory)
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
                    gPTT.pR.giveItem(tIS.myType, tIS.valueSlider.value * -1);
                    changeStock(tIS.myType, (int)tIS.valueSlider.value);
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
                    gPTT.pR.giveItem(tIS.myType, tIS.valueSlider.value);
                    changeStock(tIS.myType, (int)tIS.valueSlider.value * -1);
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
                repSlider.value = gPTT.pR.banditRep + repChange;
                currentRepText.text = gPTT.pR.banditRep.ToString();
                break;

            case globalValuesData.factionType.FREETRADE:
                repSlider.value = gPTT.pR.freeTradeRep + repChange;
                currentRepText.text = gPTT.pR.freeTradeRep.ToString();
                break;

            case globalValuesData.factionType.CORPORATION:
                repSlider.value = gPTT.pR.corporationRep + repChange;
                currentRepText.text = gPTT.pR.corporationRep.ToString();
                break;

            case globalValuesData.factionType.FACTIONLESS:
                repSlider.value = gPTT.pR.globalRep + repChange;
                currentRepText.text = gPTT.pR.globalRep.ToString();
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
        if(repChange < -10 || repChange <= 0 && repSlider.value <= 0)
        {
            tradeButton.interactable = false;
        }
        else
        {
            tradeButton.interactable = true;
        }
        if(trade == true)
        {
            gPTT.pR.reputationChange((int)repChange, currentShop.faction);
            populateShopScreen(false);
        }
    }

    public void vendorChat()
    {
        if (currentChatter < currentShop.shopTalk.Length)
        {
            shopDescription.text = currentShop.shopTalk[currentChatter];
            currentChatter++;
            if(currentChatter > currentShop.shopTalk.Length)
            {
                currentChatter = 0;
            }
        }
    }

    void changeStock(globalValuesData.itemType myType, int amount)
    {
        bool addNewStock = true;
        if (amount < 0)
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
                stock.stockAmount = stock.stockAmount + amount;
                Debug.Log(stock.stockType + " " + stock.stockAmount);
                if (stock.stockAmount <= 0)
                    {
                        removedList.Add(allStock[i]);
                    }
                allStock[i] = stock;
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
                if (gPTT.pR.banditRep >= 150)
                {
                    shopDescription.text = currentShop.friendlyShopDescription;
                    shopFlavourText.text = currentShop.friendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (gPTT.pR.banditRep < 150 && gPTT.pR.banditRep > -150)
                {
                    shopDescription.text = currentShop.shopDescription;
                    shopFlavourText.text = currentShop.shopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (gPTT.pR.banditRep <= -150)
                {
                    shopDescription.text = currentShop.unfriendlyShopDescription;
                    shopFlavourText.text = currentShop.unfriendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                break;
            case globalValuesData.factionType.CORPORATION:
                if (gPTT.pR.corporationRep >= 150)
                {
                    shopDescription.text = currentShop.friendlyShopDescription;
                    shopFlavourText.text = currentShop.friendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (gPTT.pR.corporationRep < 150 && gPTT.pR.corporationRep > -150)
                {
                    shopDescription.text = currentShop.shopDescription;
                    shopFlavourText.text = currentShop.shopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (gPTT.pR.corporationRep <= -150)
                {
                    shopDescription.text = currentShop.unfriendlyShopDescription;
                    shopFlavourText.text = currentShop.unfriendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                break;
            case globalValuesData.factionType.FREETRADE:
                if (gPTT.pR.freeTradeRep >= 150)
                {
                    shopDescription.text = currentShop.friendlyShopDescription;
                    shopFlavourText.text = currentShop.friendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (gPTT.pR.freeTradeRep < 150 && gPTT.pR.freeTradeRep > -150)
                {
                    shopDescription.text = currentShop.shopDescription;
                    shopFlavourText.text = currentShop.shopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (gPTT.pR.freeTradeRep <= -150)
                {
                    shopDescription.text = currentShop.unfriendlyShopDescription;
                    shopFlavourText.text = currentShop.unfriendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                break;
            case globalValuesData.factionType.FACTIONLESS:
                if (gPTT.pR.globalRep >= 150)
                {
                    shopDescription.text = currentShop.friendlyShopDescription;
                    shopFlavourText.text = currentShop.friendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (gPTT.pR.globalRep < 150 && gPTT.pR.globalRep > -150)
                {
                    shopDescription.text = currentShop.shopDescription;
                    shopFlavourText.text = currentShop.shopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                if (gPTT.pR.globalRep <= -150)
                {
                    shopDescription.text = currentShop.unfriendlyShopDescription;
                    shopFlavourText.text = currentShop.unfriendlyShopFlavourText;
                    vendorSprite.sprite = currentShop.vendorSprite;
                }
                break;
        }
    }


}