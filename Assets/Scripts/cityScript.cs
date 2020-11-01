using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cityScript : MonoBehaviour
{
    public locationData cityLD;
    [SerializeField] Text myText;
    public List<cityScript> adjacentCities;
    public GameObject cityMarker;
    public MeshRenderer markerRenderer;
    private void Start()
    {
        myText.text = cityLD.cityName;
        markerRenderer = cityMarker.GetComponent<MeshRenderer>();
    }
}
