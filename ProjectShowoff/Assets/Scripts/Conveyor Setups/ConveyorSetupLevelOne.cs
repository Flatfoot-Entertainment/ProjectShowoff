using UnityEngine;

public class ConveyorSetupLevelOne : ConveyorSetupScript
{
    protected override void Awake()
    {
        base.Awake();
        itemSpawners = GetComponentsInChildren<ItemSpawner>();
    }
}
