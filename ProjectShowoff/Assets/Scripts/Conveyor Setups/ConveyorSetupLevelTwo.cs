using UnityEngine;

public class ConveyorSetupLevelTwo : ConveyorSetupScript
{
    protected override void Awake()
    {
        base.Awake();
        itemSpawners = transform.parent.GetComponentsInChildren<ItemSpawner>();
    }
}
