using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportObjectScript : MonoBehaviour
{
    [SerializeField] private LayerMask transportLayer;
    [SerializeField] private Transform transportTo;

    private void OnTriggerEnter(Collider other)
    {
        if (InMask(other.gameObject.layer))
        {
            if (transportTo != null)
            {
                other.transform.position = transportTo.position;
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    private bool InMask(int layer)
    {
        return (transportLayer == (transportLayer | (1 << layer)));
    }
}
