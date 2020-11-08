using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cityDataPassthrough : MonoBehaviour
{
    [SerializeField] playerScript pS;
    [SerializeField] GameObject cityCanvas, activityHolder; 
    [SerializeField] Text cityName1, cityName2, cityDesc, cityActivitiesDesc;
    [SerializeField] Image cityBG;
    public cityScript currentCity;

    //prayers
    GameObject GOD;
    godPointToThing gPTT;
    private void Start()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
    }
    //amen

    // Start is called before the first frame update
    public void populateCityScreen()
    {
        cityCanvas.SetActive(true);
        gPTT.arrivedAtCityPopup.gameObject.SetActive(false);
        cityName1.text = currentCity.cityLD.cityName;
        cityName2.text = currentCity.cityLD.cityName;
        cityDesc.text = currentCity.cityLD.cityDescription;
        cityActivitiesDesc.text = currentCity.cityLD.cityActivitiesDescription;
        cityBG.sprite = currentCity.cityLD.backgroundSprite;
        foreach (Transform child in activityHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (GameObject activity in currentCity.cityLD.cityActivities)
        {
            Instantiate(activity, activityHolder.transform);
        }
    }
}
