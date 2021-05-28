using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightPositionScript : MonoBehaviour
{
    // Update is called once per frame
    private float startingYPos;
    private void Start() {
        startingYPos = transform.position.y;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){
        transform.position = new Vector3(hit.point.x, startingYPos + Mathf.Abs(hit.point.y), hit.point.z);
        }
    }
}
