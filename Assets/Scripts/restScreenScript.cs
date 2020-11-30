using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class restScreenScript : MonoBehaviour
{
    //god stuff amen
    GameObject GOD;
    [SerializeField] godPointToThing gPTT;
    datesAndTimes dAT;
    //end of prayers

    [SerializeField] Text titleText1, titleText2;
    [SerializeField] Text descriptionText, sliderText, dateText1Time, dateText1DMY, dateText2Time, dateText2DMY, screen2Time, screen2Date;
    [SerializeField] Slider sleepySlider, healthBar;
    [SerializeField] Image sleepySprite;
    restStopData rSDMain;

    float restHealRate;

    void Awake()
    {
        GOD = GameObject.Find("GOD");
        dAT = GOD.GetComponent<datesAndTimes>();
    }

    void Update()
    {
        sliderText.text = sleepySlider.value.ToString();
        dateText2Time.text = dAT.getAdjustedTime(0, (int)sleepySlider.value);
        dateText2DMY.text = dAT.getDate();
        healthBar.value = gPTT.pR.playerHealth;
        screen2Time.text = dAT.getTime();
        screen2Date.text = dAT.getDate();
    }

    public void populateRestScreen(restStopData rSD)
    {
        rSDMain = rSD;
        titleText1.text = rSD.stopName;
        titleText2.text = rSD.restDesc;
        descriptionText.text = rSD.stopDesc;
        sleepySprite.sprite = rSD.restImage;
        toggleCityNames(false);
        dateText1DMY.text = dAT.getDate();
        dateText1Time.text = dAT.getTime();
    }

    public void toggleCityNames(bool enableNames)
    {
        foreach(Text cityName in gPTT.cityNameTextList)
        {
            switch (enableNames)
            {
                case true:
                    cityName.gameObject.SetActive(true);
                    break;

                case false:
                    cityName.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void startResting()
    {
        gPTT.pR.rSD = rSDMain;
        gPTT.pR.resting = true;
        Time.timeScale = 50;
        gPTT.sBS.restTime = (int)sleepySlider.value;
    }

    public void stopResting()
    {
        gPTT.pR.resting = false;
        Time.timeScale = 0;
        gPTT.dashBoard.SetActive(true);
        gPTT.popupCanvas.SetActive(true);
        gPTT.cityCanvas.SetActive(true);
        transform.Find("Screen2").gameObject.SetActive(false);        
        transform.Find("Screen1").gameObject.SetActive(true);
        gameObject.SetActive(false);
        gPTT.sBS.restTime = -1;
    }
}
