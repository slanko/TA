using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class repairScreenScript : MonoBehaviour
{

    [SerializeField] Slider truckHealthSlider, possibleChangeSlider, amountSlider;
    [SerializeField] Text titleText, junkAmount, junkCost, timeCost;
    [SerializeField] Button repairButton;
    [SerializeField] float repairTimeMult = 1;
    bool lerping;
    float lerpIntendedTime, currentLerpTime, lerpStart;
    float cost;
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
        lerping = true;
        currentLerpTime = 0;
        var lerpStartValue = gPTT.pR.truckHealth;
        var lerpEndValue = possibleChangeSlider.value;
        var totalLerpValue = lerpEndValue - lerpStartValue;
        lerpIntendedTime = amountSlider.value * repairTimeMult;
        lerpStart = gPTT.pR.truckHealth;
        gPTT.pR.setItemAmount(globalValuesData.itemType.JUNK, gPTT.pR.getItemAmount(globalValuesData.itemType.JUNK) - cost);
        junkAmount.text = gPTT.pR.getItemAmount(globalValuesData.itemType.JUNK).ToString();
    }



    // Update is called once per frame
    void Update()
    {
        float newValue;

        if (!lerping)
        {
            newValue = truckHealthSlider.value + amountSlider.value * 100;
            possibleChangeSlider.value = newValue;
        }
        cost = amountSlider.value * 5;
        junkCost.text = cost.ToString();
        truckHealthSlider.value = gPTT.pR.truckHealth;

        if(cost > playerJunk)
        {
            repairButton.interactable = false;
        }
        else
        {
            repairButton.interactable = true;
        }

        if(lerping == true)
        {
            Time.timeScale = 50;
            gPTT.pR.truckHealth = Mathf.Lerp(lerpStart, possibleChangeSlider.value, currentLerpTime);
            currentLerpTime += Time.unscaledDeltaTime / lerpIntendedTime;

            if(currentLerpTime >= 1)
            {
                Time.timeScale = 0;
                lerping = false;
                gPTT.pR.truckHealth = possibleChangeSlider.value;
                amountSlider.value = 0;
            }
        }
    }
}
