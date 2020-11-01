using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godPointToThing : MonoBehaviour
{
    //used in button allocation
    public GameObject shopCanvas;
    public Animator winScreenAnim;

    //used to billboard city names
    public GameObject gameCamera;

    //colours for lines on da map
    public Color mapLineUnselectedColour;
    public Color mapLineSelectedColour, mapLineAvailableColour;

    //colours for points on da map
    public Material mapMarkerUnselectedColour, mapMarkerSelectedColor, mapMarkerAvailableColour;
}
