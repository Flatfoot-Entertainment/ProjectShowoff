using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public delegate void MouseClickHandler(Vector3 mousePos);
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 target;
    [SerializeField] private LayerMask filterMask;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetNewTarget(Input.mousePosition);
        }
        if (target != Vector3.zero)
        {
            Vector3 targetRotationEulers = Quaternion.LookRotation(target - transform.position, Vector3.up).eulerAngles;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,targetRotationEulers.y, 0), 0.1f);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, movementSpeed * Time.deltaTime);
        }
    }

    void SetNewTarget(Vector3 mousePos) {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            int hitMask = hit.collider.gameObject.layer;
            if(filterMask == (filterMask | hitMask))
            {
                target = hit.point;
            }
        }
    }
}
