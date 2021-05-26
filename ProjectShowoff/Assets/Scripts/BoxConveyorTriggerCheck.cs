using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxConveyorTriggerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private void OnCollisionEnter(Collision other)
    {
        if (layerMask.Contains(other.gameObject.layer))
        {
            EventScript.Handler.BroadcastEvent(new BoxConveyorPlaceEvent());
            gameObject.layer = 0;
        }
    }
}
