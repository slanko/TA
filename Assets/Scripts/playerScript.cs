using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    //probably needed
    [SerializeField] KeyCode openAtlasKey;
    [SerializeField] LayerMask mapRayLayerMask;

    //placeholder
    [SerializeField] Camera mapCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

       Ray ray = mapCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if(Physics.Raycast(ray, out rayHit, Mathf.Infinity, mapRayLayerMask))
            {
                if (rayHit.collider.gameObject.tag == "city")
                    {

                    }
            }
            

    }
}
