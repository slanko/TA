using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] List<GameObject> destinationList;
    [SerializeField] playerState currentState = playerState.STOPPED;
    NavMeshAgent nav;
    cityScript currentCity;
    //placeholder
    [SerializeField] Camera mapCam;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //map opening stuff
        if (Input.GetKeyDown(openAtlasKey) && mapCam.gameObject.activeSelf)
        {
            mapCam.gameObject.SetActive(false);
        }

        if(currentState == playerState.STOPPED)
        {
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

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("fired ray");
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, mapRayLayerMask))
                {
                    if (rayHit.collider.gameObject.tag == "City")
                    {
                        currentCity = rayHit.collider.gameObject.GetComponentInParent<cityScript>();
                        destinationList.Add(currentCity.gameObject);
                    }
                    else
                    {
                        currentCity = null;
                    }
                }
            }
        }

        //am i in city check
        var distanceToDestination = nav.remainingDistance;
        if (distanceToDestination < 2f)
        {
            reachedDestination();
        }
    }

    public void startTravelling()
    {
        nav.SetDestination(destinationList[0].gameObject.transform.position);
        currentState = playerState.TRAVELLING;

    }

    public void clearRoute()
    {
        destinationList.Clear();
    }

    void reachedDestination()
    {
        if (currentState == playerState.TRAVELLING)
        {
            Debug.Log("destination reached!!");
            destinationList.Remove(destinationList[0]);
            if (destinationList.Count != 0)
            {
                startTravelling();
            }
            else
            {
                currentState = playerState.STOPPED;
            }
        }

    }
}
