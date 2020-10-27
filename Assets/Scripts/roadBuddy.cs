using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class roadBuddy : MonoBehaviour
{
    LineRenderer LR;
    [SerializeField] LayerMask raycastMask;
    [SerializeField] LineRenderer mapBuddy;

    public void alignLinesToTerrain()
    {
        LR = GetComponent<LineRenderer>();
        Vector3[] linePositions = new Vector3[LR.positionCount];
        LR.GetPositions(linePositions);
        List<Vector3> positionStorageList = new List<Vector3>();
        foreach (Vector3 positionVector in linePositions)
        {
            RaycastHit hit;
            if(Physics.Raycast(new Vector3(positionVector.x, 100, positionVector.z), Vector3.down, out hit, Mathf.Infinity, raycastMask))
            {
                positionStorageList.Add(new Vector3(positionVector.x, hit.point.y + .1f, positionVector.z));
            }
        }
        LR.positionCount = positionStorageList.Count;

        LR.SetPositions(positionStorageList.ToArray());

        transform.position = (positionStorageList[0] + positionStorageList[positionStorageList.Count - 1]) / 2;
    }

    public void lineUpMapBuddy()
    {
        LR = GetComponent<LineRenderer>();
        Vector3[] linePositions = new Vector3[LR.positionCount];
        LR.GetPositions(linePositions);
        List<Vector3> positionStorageList = new List<Vector3>();
        foreach(Vector3 positionVector in linePositions)
        {
            positionStorageList.Add(new Vector3(positionVector.x, -1f, positionVector.z));
        }
        mapBuddy.positionCount = positionStorageList.Count;
        mapBuddy.SetPositions(positionStorageList.ToArray());
    }
}