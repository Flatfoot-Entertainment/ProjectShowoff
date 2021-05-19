using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularConveyor : MonoBehaviour
{
    //TODO could be extended to different types of conveyors (item, box)

    [SerializeField] private GameObject startPrefab, middlePrefab, endPrefab;
    [SerializeField] private GameObject itemSpawner, itemDestroyer;
    [SerializeField] private Transform startPoint, endPoint;

    [SerializeField] private int conveyorPartLength;

    private GameObject conveyorParent;

    private void Start()
    {
        CreateConveyor();
    }

    private void CreateConveyorPartsParent()
    {
        conveyorParent = new GameObject("ConveyorParent");
        conveyorParent.transform.parent = transform;
    }

    private void ClearPreviousConveyor()
    {
        if (conveyorParent)
        {
            if (Application.isPlaying)
            {
                Destroy(conveyorParent);
            }
            else
            {
#if UNITY_EDITOR
                DestroyImmediate(conveyorParent);
#endif
            }
        }
    }

    public void CreateConveyor()
    {
        ClearPreviousConveyor();
        CreateConveyorPartsParent();
        Vector3 startEndDiff = endPoint.position - startPoint.position;
        Vector3 buildDirection = startEndDiff.normalized;
        float angle = Mathf.Atan2(startEndDiff.z, startEndDiff.x) * Mathf.Rad2Deg;
        Quaternion startEndRotation = Quaternion.LookRotation(buildDirection, Vector3.up);
        float conveyorLength = (int)startEndDiff.magnitude;
        Debug.Log("Conveyor length: " + conveyorLength);
        for (int i = 0; i < conveyorLength; i += conveyorPartLength)
        {
            Debug.Log("Conveyor iteration: " + i);
            GameObject conveyorPart = null;
            Vector3 partPosition = new Vector3(startPoint.position.x + i * buildDirection.x, startPoint.position.y, startPoint.position.z + i * buildDirection.z);
            if (i == 0)
            {
                conveyorPart = startPrefab;
            }
            else if (i >= conveyorLength - conveyorPartLength)
            {
                conveyorPart = endPrefab;
                startEndRotation *= Quaternion.Euler(0, 180, 0);
            }
            else
            {
                conveyorPart = middlePrefab;
            }
            Instantiate(conveyorPart, partPosition, startEndRotation * Quaternion.Euler(0, -90, 0), conveyorParent.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(startPoint.position, endPoint.position, Color.red);
        if (conveyorParent)
        {
            Vector3 lastPartPosition = conveyorParent.transform.GetChild(conveyorParent.transform.childCount - 1).position;
            Debug.DrawRay(startPoint.position, lastPartPosition, Color.green);
        }
    }
}
