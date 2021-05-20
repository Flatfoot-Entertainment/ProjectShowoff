using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private List<ItemSpawner> spawners = new List<ItemSpawner>();
    [SerializeField] private List<ConveyorSetupScript> conveyors = new List<ConveyorSetupScript>();

    [SerializeField] private float spawnInterval;

    [SerializeField] private float conveyorDelay;

    private void Start()
    {
        StartCoroutine(Coroutine());
    }

    public IEnumerator Coroutine()
    {
        while (gameObject)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        spawners.ForEach(s => s.Spawn());
    }


    //ffs do it in an event or something you dum dum
    //put it in another class, not here
    public void ManageConveyors(int index)
    {
        if (conveyors.Count > 0)
        {
            //TODO get a reference of the animators to stop the conveyor animations as well
            //TODO make this as an event
            ConveyorSetupScript conveyor = conveyors[index];
            Debug.Log("Conveyor to be stopped: " + conveyor.name);
            foreach (SimpleConveyor conveyorPart in conveyor.ConveyorScripts)
            {
                if (conveyorPart.Speed == conveyorPart.InitialSpeed)
                {
                    foreach (ItemSpawner spawner in conveyor.ItemSpawners)
                    {
                        StartCoroutine(conveyorPart.StopConveyor(spawner, conveyorDelay));
                    }
                }
            }
        }
    }

    public void AddSpawner(ItemSpawner spawner)
    {
        Debug.Log("Spawner added");
        spawners.Add(spawner);
    }

    public void AddSpawners(ItemSpawner[] pSpawners)
    {
        foreach (ItemSpawner pSpawner in pSpawners)
        {
            AddSpawner(pSpawner);
        }
    }

    public void RemoveSpawner(ItemSpawner spawner)
    {
        Debug.Log("ItemSpawner removed");
        spawners.Remove(spawner);
    }

    public void RemoveSpawnerAt(int index)
    {
        if (index < 0 && index >= spawners.Count)
        {
            Debug.Log("invalid index to remove a spawner");
            return;
        }
        Debug.Log("ItemSpawner removed");
        spawners.RemoveAt(index);
    }

    public void AddConveyor(ConveyorSetupScript conveyor)
    {
        Debug.Log("Conveyor added");
        conveyors.Add(conveyor);
    }

    public void RemoveConveyor(ConveyorSetupScript conveyor)
    {
        Debug.Log("Conveyor removed");
        conveyors.Remove(conveyor);
    }

    public void RemoveConveyorAt(int index)
    {
        if (index < 0 && index >= conveyors.Count)
        {
            Debug.Log("invalid index to remove a spawner");
            return;
        }
        Debug.Log("Conveyor removed");
        conveyors.RemoveAt(index);
    }
}
