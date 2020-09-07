using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location Data", menuName = "Trucker's Atlas/Location Data")]
public class locationData : ScriptableObject
{
    public string cityName;
    public List<locationData> adjacentCities;

}
