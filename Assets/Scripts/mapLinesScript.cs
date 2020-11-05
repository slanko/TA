using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLinesScript : MonoBehaviour
{
    GameObject GOD;
    [SerializeField] List<roadListScript.roadStruct> connectedRoads;
    godPointToThing gPTT;

    private void Start()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
    }

    public void getCityConnectedRoads(cityScript city, bool highlightRoads)
    {
        roadListScript roadList = GOD.GetComponent<roadListScript>();
        foreach (roadListScript.roadStruct road in roadList.allRoads)
        {
            if (road.Location1 == city || road.Location2 == city)
            {
                connectedRoads.Add(road);
                //this sucks
                LineRenderer LRBuddy = road.LR.gameObject.transform.Find("Map Buddy").GetComponent<LineRenderer>();
                if (highlightRoads == true)
                {
                    LRBuddy.startColor = gPTT.mapLineAvailableColour;
                    LRBuddy.endColor = gPTT.mapLineAvailableColour;
                }
            }
            if (road.Location1 != city && road.Location2 == city)
            {
                road.Location1.markerRenderer.material = gPTT.mapMarkerAvailableColour;
            }
            if (road.Location1 == city && road.Location2 != city)
            {
                road.Location2.markerRenderer.material = gPTT.mapMarkerAvailableColour;
            }
            city.markerRenderer.material = gPTT.mapMarkerSelectedColor;
        }
    }

    public void roadHighlight(cityScript city1, cityScript city2)
    {
        roadListScript roadList = GOD.GetComponent<roadListScript>();
        foreach (roadListScript.roadStruct road in roadList.allRoads)
        {
            if (road.Location1 == city1 && road.Location2 == city2 || road.Location1 == city2 && road.Location2 == city1)
            {
                //this line SUCKS
                LineRenderer LRBuddy = road.LR.gameObject.transform.Find("Map Buddy").GetComponent<LineRenderer>();
                LRBuddy.startColor = gPTT.mapLineSelectedColour;
                LRBuddy.endColor = gPTT.mapLineSelectedColour;
                Debug.Log("highlighted road " + road.Location1.ToString() + " + " + road.Location2.ToString());
            }
        }
    }

    public void clearRoadColours(bool clearHighlights, bool resetToNormal)
    {
        roadListScript roadList = GOD.GetComponent<roadListScript>();
        foreach (roadListScript.roadStruct road in connectedRoads)
        {
            LineRenderer LRBuddy = road.LR.gameObject.transform.Find("Map Buddy").GetComponent<LineRenderer>();
            //this is my least favourite line of code in the whole project so far
            if (LRBuddy.startColor == gPTT.mapLineSelectedColour && clearHighlights == false)
            {
                Debug.Log("did not clear road " + road.Location1.ToString() + " + " + road.Location2.ToString() + "since it is highlighted");
            }
            else
            {
                LRBuddy.startColor = gPTT.mapLineUnselectedColour;
                LRBuddy.endColor = gPTT.mapLineUnselectedColour;
                road.Location1.markerRenderer.material = gPTT.mapMarkerUnselectedColour;
                road.Location2.markerRenderer.material = gPTT.mapMarkerUnselectedColour;
            }
        }
        if(resetToNormal == true)
        {
            getCityConnectedRoads(gPTT.PLAYER.currentCity, true);
        }
    }
    public void hitDepartButtonBecauseUnityEventsSuck()
    {
        clearRoadColours(true, true);
    }
}
