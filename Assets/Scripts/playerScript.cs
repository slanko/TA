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
    [SerializeField] playerState currentState = playerState.STOPPED;
    [SerializeField, Header("Map Stuff")] Text cityNameText;
    [SerializeField] GameObject cityNameTextParent, arrivalPopup;
    [SerializeField] Slider timeSlider;
    [SerializeField, Header("Route Planner")] GameObject routePlannerText;
    [SerializeField] GameObject routePlannerTextHolder;
    
    NavMeshAgent nav;
    cityScript currentCity;

    cityDataPassthrough cDP;
    godScript GOD;

    //placeholder
    [SerializeField] Camera mapCam;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        cDP = GameObject.Find("GOD").GetComponent<cityDataPassthrough>();
    }

    // Update is called once per frame
    void Update()    {
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

        //route selection stuff
        Ray ray = mapCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, mapRayLayerMask))
        {
            if(rayHit.collider.gameObject.tag == "City")
            {
                currentCity = rayHit.collider.gameObject.GetComponentInParent<cityScript>();
                cityNameTextParent.gameObject.transform.position = ray.origin;
                cityNameText.text = currentCity.cityLD.cityName;
                if (Input.GetMouseButtonDown(0))
                {
                    destinationList.Add(currentCity);
                    populateRoutePlanner();
                    Debug.Log("added " + currentCity.cityLD.cityName + " to the travel plan");
                }
            }
        }
        else
        {
            cityNameText.text = "";
        }
        //am i in city check
    }

    private void FixedUpdate()
    {
        if(destinationList.Count > 0)
        {
            var distanceToDestination = Vector3.Distance(transform.position, destinationList[0].gameObject.transform.position);
            if (distanceToDestination < 1f && distanceToDestination > 0.01f && currentState == playerState.TRAVELLING)
            {
                reachedDestination();
            }
        }
    }

    public void startTravelling()
    {
        nav.SetDestination(destinationList[0].gameObject.transform.position);
        cDP.currentCity = destinationList[0];
        currentState = playerState.TRAVELLING;
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
            Debug.Log("destination reached: " + destinationList[0].cityLD.cityName);
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
            startTravelling();
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
