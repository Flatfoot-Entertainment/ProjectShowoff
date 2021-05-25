using UnityEngine;

public abstract class ConveyorSetupScript : MonoBehaviour
{
    public float InitialSpeed => initialSpeed;
    public ItemSpawner[] ItemSpawners => itemSpawners;
    public SimpleConveyor[] ConveyorScripts => conveyorScripts;
    [SerializeField] private float initialSpeed = 2.0f;
    [SerializeField] private SimpleConveyor[] conveyorScripts;
    [SerializeField] protected ItemSpawner[] itemSpawners;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        conveyorScripts = GetComponentsInChildren<SimpleConveyor>();
        for (int i = 0; i < conveyorScripts.Length; i++)
        {
            SimpleConveyor conveyor = conveyorScripts[i];
            conveyor.InitialSpeed = initialSpeed;
        }
    }
}