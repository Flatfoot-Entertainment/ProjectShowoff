using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanPoolDestructionTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask destructionLayer;

    // TODO respect multi child objects
    // doesn't complain anymore?
    private void OnTriggerEnter(Collider other)
    {
        if (destructionLayer.Contains(other.gameObject.layer))
        {
            Lean.Pool.LeanPool.Despawn(other.gameObject);
        }
    }
}
