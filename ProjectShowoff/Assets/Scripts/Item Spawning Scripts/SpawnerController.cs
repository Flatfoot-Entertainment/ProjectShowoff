using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    // TODO has a bunch of Debug.Logs

    public int ConveyorInitialSpeed = 2;

    [SerializeField] private List<ItemSpawner> spawners = new List<ItemSpawner>();
    [SerializeField] private List<ConveyorSetupScript> conveyors = new List<ConveyorSetupScript>();
    [SerializeField] private List<ConveyorSetupScript> leftConveyors = new List<ConveyorSetupScript>();
    [SerializeField] private List<ConveyorSetupScript> rightConveyors = new List<ConveyorSetupScript>();

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

    //TODO fix issue where all conveyors stop now...
    public void StopLeftConveyors()
    {
        StopConveyorCollection(leftConveyors);
    }

    public void StopRightConveyors()
    {
        StopConveyorCollection(rightConveyors);
    }

    private void StopConveyorCollection(List<ConveyorSetupScript> collection)
    {
        foreach (var conv in collection)
        {
            Debug.Log($"Stopping {conv.name}");
            foreach (SimpleConveyor conveyorPart in conv.ConveyorScripts)
            {
                StartCoroutine(conveyorPart.Stop(conveyorDelay));
            }
            foreach (ItemSpawner spawner in conv.ItemSpawners)
            {
                StartCoroutine(spawner.Stop(conveyorDelay));
            }
        }
    }

    //TODO maybe putting the methods below somewhere else..?
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

    public void ChangeConveyorSpeed(int speed)
    {
        foreach (ConveyorSetupScript conveyor in conveyors)
        {
            foreach (SimpleConveyor simpleConveyor in conveyor.ConveyorScripts)
            {
                simpleConveyor.Speed = speed;
            }
        }
    }
}
