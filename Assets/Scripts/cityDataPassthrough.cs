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
    [System.NonSerialized] public cityScript currentCity;

    // Start is called before the first frame update
    public void populateCityScreen()
    {
        cityCanvas.SetActive(true);
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
