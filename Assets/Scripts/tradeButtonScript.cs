using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tradeButtonScript : MonoBehaviour
{
    public enum buttonType
    {
        TRADE,
        WINGAME,
        BAR,
        REPAIR,
        REST,
        SCROUNGE
    }
    GameObject shopCanvas;
    [SerializeField] buttonType type;
    shopScript sSc;
    Button meButton;
    GameObject GOD;
    godPointToThing gPTT;
    void Awake()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
        meButton = GetComponent<Button>();
        if(type == buttonType.TRADE)
        {
            shopCanvas = gPTT.shopCanvas;
            sSc = shopCanvas.GetComponent<shopScript>();
            meButton.onClick.AddListener(delegate { shopCanvas.SetActive(true); sSc.populateShopScreen(); });
        }
        if(type == buttonType.WINGAME)
        {
            meButton.onClick.AddListener(delegate { gPTT.winScreenAnim.SetTrigger("die"); });
        }
        if(type == buttonType.REST)
        {
            meButton.onClick.AddListener(delegate { 
                gPTT.restScreen.SetActive(true); 
                gPTT.cityCanvas.SetActive(false); 
                gPTT.rSS.populateRestScreen(gPTT.PLAYER.currentCity.rSD);
                gPTT.popupCanvas.SetActive(false);
                gPTT.dashBoard.SetActive(false);
            });
        }
    }
}
