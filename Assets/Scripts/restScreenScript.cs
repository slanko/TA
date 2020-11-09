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
    [SerializeField] Text descriptionText, sliderText, dateText1Time, dateText1DMY, dateText2Time, dateText2DMY;
    [SerializeField] Slider sleepySlider;
    [SerializeField] Image sleepySprite;

    float restHealRate;

    void Start()
    {
        GOD = GameObject.Find("GOD");
        dAT = GOD.GetComponent<datesAndTimes>();
    }

    void Update()
    {
        sliderText.text = sleepySlider.value.ToString();
        dateText2Time.text = dAT.getAdjustedTime(0, (int)sleepySlider.value);
        dateText2DMY.text = dAT.getDate();
    }

    public void populateRestScreen(restStopData rSD)
    {
        titleText1.text = rSD.stopName;
        titleText2.text = rSD.restDesc;
        descriptionText.text = rSD.stopDesc;
        sleepySprite.sprite = rSD.restImage;
        toggleCityNames(false);
        dateText1Time.text = dAT.getTime();
        dateText1DMY.text = dAT.getDate();
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
}
