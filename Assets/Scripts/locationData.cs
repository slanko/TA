﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Location Data", menuName = "Trucker's Atlas/Location Data")]
public class locationData : ScriptableObject
{
    public string cityName;
    public List<locationData> adjacentCities;
    public Sprite backgroundSprite;
    [TextArea(0, 20)]
    public string cityDescription, cityActivitiesDescription;
    public GameObject[] cityActivities;
}