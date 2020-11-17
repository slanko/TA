using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameCameraScript : MonoBehaviour
{
    [SerializeField] KeyCode camLeftKey, camRightKey;
    [SerializeField] float camLerpSpeed, camRotateSpeed, mouseSensitivityX, mouseSensitivityY, camZoomSpeed;
    [SerializeField] Transform targetPosition, camLookAtTarget;
    [SerializeField] GameObject myCamera;
    float cam2TruckDist;
    Vector3 startCameraPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startCameraPos = myCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition.position, camLerpSpeed);
        if (Input.GetKey(camLeftKey))
        {
            transform.Rotate(Vector3.up * camRotateSpeed);
        }

        if (Input.GetKey(camRightKey))
        {
            transform.Rotate(Vector3.up * -1 * camRotateSpeed);
        }

        if (Input.GetMouseButton(1))
        {
            myCamera.transform.Translate(new Vector3(Input.GetAxis("Mouse X") * mouseSensitivityX, Input.GetAxis("Mouse Y") * mouseSensitivityY, 0));
        }

        if (Input.GetMouseButton(2))
        {
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, startCameraPos, .01f);
        }

        if(Input.mouseScrollDelta.y != 0)
        {
            if(Input.mouseScrollDelta.y > 0)
            {
                myCamera.transform.Translate(Vector3.forward * camZoomSpeed);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                myCamera.transform.Translate(Vector3.forward * -1 * camZoomSpeed);
            }
        }

        myCamera.transform.LookAt(camLookAtTarget);
    }
}
