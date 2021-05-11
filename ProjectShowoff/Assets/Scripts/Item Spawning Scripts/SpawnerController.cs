using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
	[SerializeField] private List<ItemSpawner> spawners = new List<ItemSpawner>();

	[SerializeField] private float spawnInterval;

	private void Start()
	{
		StartCoroutine(Coroutine());
	}

	private IEnumerator Coroutine()
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
}
