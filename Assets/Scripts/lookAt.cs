using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public GameObject targetObject;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetObject.transform.position);
    }
}
