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
    //probably needed
    [SerializeField] KeyCode openAtlasKey;
    [SerializeField] LayerMask mapRayLayerMask;
    public List<cityScript> destinationList;
    public playerState currentState = playerState.STOPPED;
    [SerializeField, Header("Map Stuff")] Text cityNameText;
    [SerializeField] GameObject cityNameTextParent, arrivalPopup;
    [SerializeField] Slider timeSlider;
    [SerializeField, Header("Route Planner")] GameObject routePlannerText;
    [SerializeField] GameObject routePlannerTextHolder, routeLineRenderer;
    List<GameObject> lineRendererList=new List<GameObject>();
    [SerializeField] Text stateText, arrivedAtCityText;

    int roadInt;
    Vector3[] roadPosArray;
    [SerializeField] bool traverseRoadForward;
    [SerializeField] float destinationDistanceCheck;

    NavMeshAgent nav;
    [SerializeField] cityScript currentCity;
    cityScript selectedCity;

    cityDataPassthrough cDP;
    GameObject GOD;
    roadListScript rLS;

    //placeholder
    [SerializeField] Camera mapCam;
    // Start is called before the first frame update
    void Start()
    {
        GOD = GameObject.Find("GOD");
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
                selectedCity = rayHit.collider.gameObject.GetComponentInParent<cityScript>();
                cityNameTextParent.gameObject.transform.position = ray.origin;
                cityNameText.text = selectedCity.cityLD.cityName;
                if(destinationList.Count > 0)
                {
                    foreach (cityScript adjCit in destinationList[destinationList.Count - 1].adjacentCities)
                    {
                        LineRenderer lineRend = Instantiate(routeLineRenderer, selectedCity.gameObject.transform.position, gameObject.transform.rotation, selectedCity.transform).GetComponent<LineRenderer>();
                        lineRend.SetPosition(0, selectedCity.gameObject.transform.position);
                        lineRend.SetPosition(1, adjCit.gameObject.transform.position);
                        lineRendererList.Add(lineRend.gameObject);
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    destinationList.Add(selectedCity);
                    populateRoutePlanner();
                    Debug.Log("added " + selectedCity.cityLD.cityName + " to the travel plan");
                }
            }
        }
        else
        {
            cityNameText.text = "";
            if (lineRendererList.Count > 0)
            {
                foreach (GameObject objectToDelete in lineRendererList)
                {
                    Destroy(objectToDelete);
                }
            }
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
            currentState = playerState.TRAVELLING;
        }
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
