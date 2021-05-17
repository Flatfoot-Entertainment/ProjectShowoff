using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TranslateCameraTrigger : MonoBehaviour
{
    //also needs refactoring, jesus how many times am i gonna repeat myself?

    [SerializeField] private LayerMask transportationLayer;
    [SerializeField] private Transform pointToMoveCameraTo;
    [SerializeField] private float duration = 2.0f;

    private void OnCollisionEnter(Collision other)
    {
        if (InMask(other.gameObject.layer))
        {
            Camera.main.transform.DOMove(pointToMoveCameraTo.position, duration).SetEase(Ease.InOutBack);
            Destroy(other.gameObject);
        }
    }

    private bool InMask(int layer)
    {
        return (transportationLayer == (transportationLayer | (1 << layer)));
    }
}
