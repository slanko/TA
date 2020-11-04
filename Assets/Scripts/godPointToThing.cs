using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godPointToThing : MonoBehaviour
{
    //putting player in allcaps because it's IMPORTANT pls no bully
    public playerScript PLAYER;
    public playerResources pR;

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

    //used for hourly inventory updating
    public tabScript tS;
}
