using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Planet : MonoBehaviour
{
	[SerializeField] private PlanetUI ui;
	[SerializeField] private UnityEvent<Planet> OnClick;
	public Dictionary<ItemType, float> needs { get; private set; } = new Dictionary<ItemType, float>();

	private void Start()
	{
		// For now randomly create planet properties
		InitRandom();
		ui.Contents = needs;
	}

	public void Deliver(ContainerData box)
	{
		foreach (ItemBoxData b in box.Contents)
		{
			foreach (Item i in b.Contents)
			{
				if (needs.ContainsKey(i.Type))
				{
					needs[i.Type] -= i.Value;
					if (needs[i.Type] <= 0f)
					{
						needs.Remove(i.Type);
					}
				}
			}
		}
		if (needs.Count <= 0)
		{
			InitRandom();
		}
		ui.Contents = needs;
	}

	private void OnMouseDown()
	{
		OnClick?.Invoke(this);
	}

	private void InitRandom()
	{
		needs.Clear();
		int numProps = Random.Range(7, 10);
		for (int i = 0; i < numProps; i++)
		{
			ItemType t = Extensions.RandomEnumValue<ItemType>();
			if (needs.ContainsKey(t)) needs[t] += Random.Range(15f, 30f);
			else needs[t] = Random.Range(15f, 30f);
		}
	}
}
