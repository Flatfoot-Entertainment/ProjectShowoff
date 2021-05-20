using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSetupScript : MonoBehaviour
{
    public float InitialSpeed => initialSpeed;
    public ItemSpawner[] ItemSpawners => itemSpawners;
    public SimpleConveyor[] ConveyorScripts => conveyorScripts;
    [SerializeField] private float initialSpeed;
    [SerializeField] private SimpleConveyor[] conveyorScripts;
    [SerializeField] private ItemSpawner[] itemSpawners;
    // Start is called before the first frame update
    private void Awake()
    {
        conveyorScripts = GetComponentsInChildren<SimpleConveyor>();
        for (int i = 0; i < conveyorScripts.Length; i++)
        {
            SimpleConveyor conveyor = conveyorScripts[i];
            conveyor.InitialSpeed = initialSpeed;
        }
    }
}