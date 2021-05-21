using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
	// TODO has a bunch of Debug.Logs
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

	public void StopLeftConveyors()
	{
		foreach (var conv in leftConveyors)
		{
			Debug.Log($"Stopping {conv.name}");
			foreach (SimpleConveyor conveyorPart in conv.ConveyorScripts)
			{
				Debug.Log("Conveyor piece: " + conveyorPart.transform.name);
				if (conveyorPart.Speed > 0)
				{
					foreach (ItemSpawner spawner in conv.ItemSpawners)
					{
						StartCoroutine(conveyorPart.StopConveyor(spawner, conveyorDelay));
					}
				}
			}
		}
	}

	public void StopRightConveyors()
	{
		foreach (var conv in rightConveyors)
		{
			foreach (SimpleConveyor conveyorPart in conv.ConveyorScripts)
			{
				Debug.Log("Conveyor piece: " + conveyorPart.transform.name);
				if (conveyorPart.Speed > 0)
				{
					foreach (ItemSpawner spawner in conv.ItemSpawners)
					{
						StartCoroutine(conveyorPart.StopConveyor(spawner, conveyorDelay));
					}
				}
			}
		}
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
			Debug.Log("Conveyor to be stopped: " + conveyor.transform.parent.name);
			foreach (SimpleConveyor conveyorPart in conveyor.ConveyorScripts)
			{
				Debug.Log("Conveyor piece: " + conveyorPart.transform.name);
				if (conveyorPart.Speed > 0)
				{
					foreach (ItemSpawner spawner in conveyor.ItemSpawners)
					{
						Debug.Log("yes");
						Debug.Log("Item Spawner: " + spawner.transform.name);
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
