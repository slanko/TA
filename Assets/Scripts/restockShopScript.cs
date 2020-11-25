using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class restockShopScript : MonoBehaviour
{
    godPointToThing gPTT;
    shopData currentShop;
    [SerializeField] int daysBetweenRestock;
    bool restocked = false;

    private void Start()
    {
        gPTT = GameObject.Find("GOD").GetComponent<godPointToThing>();
        currentShop = GetComponent<cityScript>().cityLD.tradeArea;
    }

    private void Update()
    {
        if(gPTT.theTime.day % daysBetweenRestock == 1)
        {
            if(restocked == false)
            {
                restockShop();
                restocked = true;
            }
        }
        else
        {
            restocked = false;
        }
    }

    public void restockShop()
    {
        var tempStockList = new List<stockItem>();

        foreach (stockItem stock in currentShop.GetShopStock())
        {
            tempStockList.Add(stock);
        }

        var tempListArray = tempStockList.ToArray();
        var missingStockTempList = new List<stockItem>();

        foreach (stockItem baseStock in currentShop.GetBaseShopStock())
        {
            bool found = false;
            for (int i = 0; i < tempListArray.Length; i++)
            {
                var tempStock = tempListArray[i];
                if (tempStock.stockType == baseStock.stockType)
                {
                    found = true;

                    if (tempStock.stockAmount < baseStock.stockAmount)
                    {
                        tempListArray[i].stockAmount = baseStock.stockAmount;
                    }
                    break;
                }
            }
            if (!found)
            {
                missingStockTempList.Add(baseStock);
            }
        }
        //after that, take our array and set it to the temp list.
        tempStockList = tempListArray.ToList<stockItem>();

        foreach (stockItem missingStock in missingStockTempList)
        {
            tempStockList.Add(missingStock);
        }
        currentShop.SetShopStock(tempStockList);
        Debug.Log(currentShop + " restocked.");
    }
}
