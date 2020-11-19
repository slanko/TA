using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //used for rest screen switching stuff
    public GameObject cityCanvas;
    public GameObject restScreen;
    public restScreenScript rSS;
    public GameObject popupCanvas;
    public GameObject dashBoard;

    //rest screen rest function stuff
    public skyboxBuddyScript sBS;

    //this one's a funny one. this is a list of all the city title text objects so we can enable and disable all of them if we'd like
    public List<Text> cityNameTextList;

    //funny city popup
    public GameObject arrivedAtCityPopup;

    //time stuff
    [System.Serializable]
    public struct timeStruct
    {
        public int minute, hour, day, month, year;
    }
    public timeStruct theTime;

    //universal popup stuff
    public GameObject uPopup;
    public Text uPopupText, uPopupButtonText;
    public void setUniversalPopup(string popupText, string popupButtonText)
    {
        uPopupText.text = popupText;
        uPopupButtonText.text = popupButtonText;
        uPopup.SetActive(true);
    }

    //um tool tip transform
    public RectTransform toolTipTransform;
    public Text toolTipText;
}
