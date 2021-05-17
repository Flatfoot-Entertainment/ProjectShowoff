using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransportTrigger : MonoBehaviour
{
    //probably to be integrated as an event, but for now here's an easy copy-paste from DestructionTrigger
    [SerializeField] private Transform pointToMoveCameraTo;
    [SerializeField] private Transform pointToTransportTo;
    [SerializeField] private LayerMask transportationLayer;
    [SerializeField] private float duration = 2.0f;

    private bool cameraIsMoving;
    private Transform containerHit;


    private void OnTriggerEnter(Collider other)
    {
        if (InMask(other.gameObject.layer))
        {
            Camera.main.transform.DOMove(pointToMoveCameraTo.position, duration).SetEase(Ease.InOutBack);
            other.transform.position = pointToTransportTo.position;
        }
    }

    private bool InMask(int layer)
    {
        return (transportationLayer == (transportationLayer | (1 << layer)));
    }
}
