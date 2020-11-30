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
        meButton.onClick.AddListener(delegate { killTooltip(); });
        switch (type)
        {
            case buttonType.TRADE:
                shopCanvas = gPTT.shopCanvas;
                sSc = shopCanvas.GetComponent<shopScript>();
                meButton.onClick.AddListener(delegate { shopCanvas.SetActive(true); sSc.populateShopScreen(true); });
                break;

            case buttonType.WINGAME:
                if (gPTT.pR.getItemAmount(globalValuesData.itemType.JUNK) >= 10)
                {
                    meButton.onClick.AddListener(delegate { gPTT.winScreenAnim.SetTrigger("die"); });
                }
                else
                {
                    meButton.onClick.AddListener(delegate { gPTT.setUniversalPopup("you don't have enough JUNK to get a ticket", "accept and move on"); });
                    Debug.Log("not enough stuff to leave!! set button to open popup instead.");
                }
                break;

            case buttonType.REST:
                meButton.onClick.AddListener(delegate {
                    gPTT.popupCanvas.SetActive(false);
                    gPTT.dashBoard.SetActive(false);
                    gPTT.restScreen.SetActive(true);
                    gPTT.cityCanvas.SetActive(false);
                    gPTT.rSS.populateRestScreen(gPTT.PLAYER.currentCity.rSD);
                });
                break;

            case buttonType.REPAIR:
                meButton.onClick.AddListener(delegate
                {
                    gPTT.popupCanvas.SetActive(false);
                    gPTT.dashBoard.SetActive(false);
                    gPTT.repairScreen.SetActive(true);
                    gPTT.cityCanvas.SetActive(false);
                    gPTT.repSS.populateRepairScreen();
                });
                break;

            case buttonType.SCROUNGE:
                meButton.onClick.AddListener(delegate { scroungeFunction(); });
                void scroungeFunction()
                {
                    int randomChance = Random.Range(0, 100);
                    int amount = Random.Range(1, 5);
                    if (randomChance <= 65)
                    {
                        gPTT.pR.giveItem(globalValuesData.itemType.LUXURIES, amount);
                        string popupText = "you found " + amount.ToString() + " STUFF while scrounging around NOWHERE.";
                        gPTT.setUniversalPopup(popupText, "sweet");
                    }
                    if (randomChance > 65 && randomChance <= 90)
                    {
                        gPTT.pR.giveItem(globalValuesData.itemType.JUNK, amount);
                        string popupText = "you found " + amount.ToString() + " JUNK, obviously thrown over the walls in an attempt to dispose of it.";
                        gPTT.setUniversalPopup(popupText, "sweet");
                    }
                    if (randomChance > 90)
                    {
                        gPTT.pR.giveItem(globalValuesData.itemType.TRASH, amount);
                        string popupText = "you found " + amount.ToString() + " TRASH that you thought you might be able to find a use for.";
                        gPTT.setUniversalPopup(popupText, "sweet");
                    }
                }
                break;
        }
    }

    public void giveItemWithPopup(globalValuesData.itemType type, int amount, string popupTextStart, string popupTextEnd, string popupButtonText)
    {
        gPTT.pR.giveItem(type, amount);
        string popupText = popupTextStart + amount.ToString() + popupTextEnd;
        gPTT.setUniversalPopup(popupText, popupButtonText);
    }

    void killTooltip()
    {
        gPTT.toolTipTransform.gameObject.SetActive(false);
    }
}
