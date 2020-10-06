using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tradeButtonScript : MonoBehaviour
{
    GameObject shopCanvas;
    shopScript sSc;
    Button meButton;
    void Awake()
    {
        shopCanvas = GameObject.Find("GOD").GetComponent<godPointToThing>().shopCanvas;
        sSc = shopCanvas.GetComponent<shopScript>();
        meButton = GetComponent<Button>();
        meButton.onClick.AddListener(delegate { shopCanvas.SetActive(true); });
        meButton.onClick.AddListener(delegate { sSc.populateShopScreen(); });
    }
}
