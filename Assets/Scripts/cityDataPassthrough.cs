using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cityDataPassthrough : MonoBehaviour
{
    [SerializeField] playerScript pS;
    [SerializeField] GameObject cityCanvas; 
    [SerializeField] Text cityName1, cityName2, cityDesc, cityActivitiesDesc;
    [SerializeField] Image cityBG;
    [System.NonSerialized] public cityScript currentCity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void populateCityScreen()
    {
        cityCanvas.SetActive(true);
        cityName1.text = currentCity.cityLD.cityName;
        cityName2.text = currentCity.cityLD.cityName;
        cityDesc.text = currentCity.cityLD.cityDescription;
        cityActivitiesDesc.text = currentCity.cityLD.cityActivitiesDescription;
        cityBG.sprite = currentCity.cityLD.backgroundSprite;
    }
}
