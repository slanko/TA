using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class repairScreenScript : MonoBehaviour
{

    [SerializeField] Slider truckHealthSlider, possibleChangeSlider, amountSlider;
    [SerializeField] Text titleText, junkAmount, junkCost, timeCost;
    [SerializeField] Button repairButton;
    float playerJunk;
    godPointToThing gPTT;
    // Start is called before the first frame update
    void Awake()
    {
        gPTT = GameObject.Find("GOD").GetComponent<godPointToThing>();
    }

    private void OnEnable()
    {
        populateRepairScreen();
    }

    public void populateRepairScreen()
    {
        truckHealthSlider.value = gPTT.pR.truckHealth;
        possibleChangeSlider.value = gPTT.pR.truckHealth;
        playerJunk = gPTT.pR.getItemAmount(globalValuesData.itemType.JUNK);
        junkAmount.text = playerJunk.ToString();
    }

    public void doTheRepair()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        float newValue;
        float cost;

        newValue = truckHealthSlider.value + amountSlider.value * 100;
        possibleChangeSlider.value = newValue;
        cost = amountSlider.value * 5;
        junkCost.text = cost.ToString();
        if(cost > playerJunk)
        {
            repairButton.interactable = false;
        }
        else
        {
            repairButton.interactable = true;
        }
    }
}
