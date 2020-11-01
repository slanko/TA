using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    public enum playerState
    {
        TRAVELLING,
        STOPPED
    }
    //PLAYER VARIABLES
    [SerializeField] KeyCode openAtlasKey;
    public List<cityScript> destinationList;
    public playerState currentState = playerState.STOPPED;
    [SerializeField, Header("Map Stuff")] Text cityNameText;
    [SerializeField] LayerMask mapRayLayerMask;
    [SerializeField] GameObject cityNameTextParent, arrivalPopup;
    [SerializeField] Slider timeSlider;

    //ROUTE PLANNER
    [SerializeField, Header("Route Planner")] GameObject routePlannerText;
    [SerializeField] GameObject routePlannerTextHolder, routeLineRenderer;
    List<GameObject> lineRendererList=new List<GameObject>();
    [SerializeField] Text stateText, arrivedAtCityText;
    cityScript currentPlannerCity;
    [SerializeField] List<roadListScript.roadStruct> connectedRoads;

    int roadInt;
    Vector3[] roadPosArray;
    [SerializeField] bool traverseRoadForward;
    [SerializeField] float destinationDistanceCheck;

    NavMeshAgent nav;
    [SerializeField] cityScript currentCity;
    cityScript selectedCity;

    cityDataPassthrough cDP;
    GameObject GOD;
    godPointToThing gPTT;
    roadListScript rLS;

    //placeholder
    [SerializeField] Camera mapCam;
    // Start is called before the first frame update
    void Start()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
        nav = GetComponent<NavMeshAgent>();
        cDP = GOD.GetComponent<cityDataPassthrough>();
        rLS = GOD.GetComponent<roadListScript>();
    }

    // Update is called once per frame
    void Update()    
    {
        stateText.text = "STATE: " + currentState.ToString();
        //map opening stuff

            if (Input.GetKeyDown(openAtlasKey))
            {
                if (mapCam.gameObject.activeSelf)
                {
                    mapCam.gameObject.SetActive(false);
                }
                else
                {
                    mapCam.gameObject.SetActive(true);
                }
            }

            if(nav.destination != null)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                nav.nextPosition = nav.destination;
                }
            }

        //route selection stuff
        Ray ray = mapCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, mapRayLayerMask))
        {
            if(rayHit.collider.gameObject.tag == "City")
            {
                //this is what happens when you hover over a city
                selectedCity = rayHit.collider.gameObject.GetComponentInParent<cityScript>();
                cityNameTextParent.gameObject.transform.position = ray.origin;
                cityNameText.text = selectedCity.cityLD.cityName;

                //okay so this is what happens when you pick a city and it is eligible
                if (Input.GetMouseButtonDown(0))
                {
                    clearRoadColours();
                    destinationList.Add(selectedCity);
                    currentPlannerCity = selectedCity;
                    //get all current planner city's selected roads so uh let's make a new function
                    getCityConnectedRoads(selectedCity, true);
                    populateRoutePlanner();
                    Debug.Log("added " + selectedCity.cityLD.cityName + " to the travel plan");
                }
            }
        }
        else
        {
            cityNameText.text = "";
            //this part might be unnessecary (definitely i think)
            if (lineRendererList.Count > 0)
            {
                foreach (GameObject objectToDelete in lineRendererList)
                {
                    //Destroy(objectToDelete);
                }
            }
        }
    }

    void getCityConnectedRoads(cityScript city, bool highlightRoads)
    {
        roadListScript roadList = GOD.GetComponent<roadListScript>();
        foreach (roadListScript.roadStruct road in roadList.allRoads)
        {
            if(road.Location1 == city || road.Location2 == city)
            {
                connectedRoads.Add(road);
                //this sucks
                LineRenderer LRBuddy = road.LR.gameObject.transform.Find("Map Buddy").GetComponent<LineRenderer>();
                if(highlightRoads == true)
                {
                    LRBuddy.startColor = gPTT.mapLineAvailableColour;
                    LRBuddy.endColor = gPTT.mapLineAvailableColour;
                }
            }
            if(road.Location1 != city && road.Location2 == city)
            {
                road.Location1.markerRenderer.material = gPTT.mapMarkerAvailableColour;
                Debug.Log("ding");
            }
            if (road.Location1 == city && road.Location2 != city)
            {
                road.Location2.markerRenderer.material = gPTT.mapMarkerAvailableColour;
                Debug.Log("ding");
            }
            city.markerRenderer.material = gPTT.mapMarkerSelectedColor;
        }
    }

    public void clearRoadColours()
    {
        roadListScript roadList = GOD.GetComponent<roadListScript>();
        foreach(roadListScript.roadStruct road in connectedRoads)
        {
            LineRenderer LRBuddy = road.LR.gameObject.transform.Find("Map Buddy").GetComponent<LineRenderer>();
            LRBuddy.startColor = gPTT.mapLineUnselectedColour;
            LRBuddy.endColor = gPTT.mapLineUnselectedColour;
            road.Location1.markerRenderer.material = gPTT.mapMarkerUnselectedColour;
            road.Location2.markerRenderer.material = gPTT.mapMarkerUnselectedColour;
        }
    }

    private void FixedUpdate()
    {
        if(destinationList.Count > 0 && currentState == playerState.TRAVELLING)
        {
            var distanceToDestination = Vector3.Distance(transform.position, nav.destination);
            if (traverseRoadForward)
            {
                if (distanceToDestination < destinationDistanceCheck && currentState == playerState.TRAVELLING && roadInt <= roadPosArray.Length - 1)
                {
                    nextRoadStep();
                    roadInt++;
                }
                if (distanceToDestination < destinationDistanceCheck && currentState == playerState.TRAVELLING && roadInt == roadPosArray.Length)
                {
                    nav.SetDestination(destinationList[0].transform.position);
                }
            }
            if (!traverseRoadForward)
            {
                if (distanceToDestination < destinationDistanceCheck &&  currentState == playerState.TRAVELLING && roadInt >= 1)
                {
                    nextRoadStep();
                    roadInt--;
                }
                if (distanceToDestination < destinationDistanceCheck &&  currentState == playerState.TRAVELLING && roadInt == 0)
                {
                    nav.SetDestination(destinationList[0].transform.position);
                }
            }
        }
        if(destinationList.Count > 0)
        {
            if (Vector3.Distance(transform.position, destinationList[0].transform.position) < 1f)
            {
                reachedDestination();
            }
        }
    }

    public void startTravelling()
    {
        currentState = playerState.TRAVELLING;
        if(destinationList.Count > 0)
        {
            for (int i = 0; i < rLS.allRoads.Length; i++)
            {
                roadListScript.roadStruct requiredRoad;
                if (rLS.allRoads[i].Location1 == currentCity && rLS.allRoads[i].Location2 == destinationList[0] ||
                    rLS.allRoads[i].Location2 == currentCity && rLS.allRoads[i].Location1 == destinationList[0])
                {
                    requiredRoad = rLS.allRoads[i];
                    roadPosArray = new Vector3[requiredRoad.LR.positionCount];
                    requiredRoad.LR.GetPositions(roadPosArray);
                    if(currentCity == rLS.allRoads[i].Location1)
                    {
                        traverseRoadForward = true;
                    }
                    if(currentCity == rLS.allRoads[i].Location2)
                    {
                        traverseRoadForward = false;
                    }
                    if (traverseRoadForward)
                    {
                        nav.SetDestination(roadPosArray[0]);
                        roadInt = 1;
                    }
                    if(!traverseRoadForward)
                    {
                        nav.SetDestination(roadPosArray[roadPosArray.Length - 1]);
                        roadInt = roadPosArray.Length - 2;
                    }
                    Debug.Log("set destination to: " + rLS.allRoads[i].LR.name + " " + nav.destination);
                }
            }
        }
        else
        {
            Debug.Log("no destinations no travel");
            currentState = playerState.STOPPED;
        }
    }

    void nextRoadStep()
    {
        nav.SetDestination(roadPosArray[roadInt]);
        //Debug.Log("destination set: " + roadPosArray[roadInt]);
    }

    public void clearRoute()
    {
        destinationList.Clear();
        populateRoutePlanner();
    }

    void reachedDestination()
    {
        if (currentState == playerState.TRAVELLING)
        {
            currentCity = destinationList[0];
            cDP.currentCity = currentCity;
            Debug.Log("destination reached: " + destinationList[0].cityLD.cityName);
            arrivedAtCityText.text = "You have arrived at " + destinationList[0].cityLD.cityName + ". Would you like to stop here?";
            destinationList.Remove(destinationList[0]);
            Time.timeScale = 0;
            arrivalPopup.SetActive(true);
            currentState = playerState.STOPPED;
        }

    }

    public void closePopup(GameObject popup)
    {
        popup.SetActive(false);
        Time.timeScale = timeSlider.value;
        if(destinationList.Count > 0)
        {
            startTravelling();
        }
    }
    public void closePopupNoTravel(GameObject popup)
    {
        popup.SetActive(false);
        Time.timeScale = timeSlider.value;
    }

    public void populateRoutePlanner()
    {
        foreach (Transform child in routePlannerTextHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (cityScript cS in destinationList)
        {
            var currentText = Instantiate(routePlannerText, routePlannerTextHolder.transform);
            currentText.GetComponent<Text>().text = cS.cityLD.cityName;
        }
    }
}
