using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private List<ItemSpawner> spawners = new List<ItemSpawner>();
    [SerializeField] private List<SimpleConveyor> conveyors = new List<SimpleConveyor>();

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
            SimpleConveyor conveyor = conveyors[index];
            Debug.Log("Conveyor to be stopped: " + conveyor.name);
            if (conveyor.Speed == conveyor.InitialSpeed)
            {
                StartCoroutine(conveyor.StopConveyor(spawners[index], conveyorDelay));
            }
        }
    }
}
