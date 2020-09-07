using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameCameraScript : MonoBehaviour
{
    [SerializeField] KeyCode camLeftKey, camRightKey;
    [SerializeField] float camLerpSpeed, camRotateSpeed;
    [SerializeField] Transform targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
