using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class ShipLoadingHandler : MonoBehaviour
{
	private Ship ship;
	private ContainerData box;

	private void Awake()
	{
		ship = GetComponent<Ship>();
	}

	void Start()
	{
		ship.OnArrival += ResetShip;
		box = new ContainerData();
	}

	public bool IsEmpty()
	{
		return box.Contents.Count <= 0;
	}

	private void OnCollisionEnter(Collision other)
	{
		var comp = other.gameObject.GetComponent<ItemBoxScript>();
		if (comp)
		{
			box.AddToBox(comp.contained);
			Destroy(other.gameObject);
		}
	}

	public void Ship()
	{
		enabled = false;
		ship.ResetShip();
		ship.enabled = true;
		ship.box = box;
	}

	// Reset the loading handler
	public void ResetShip()
	{
		enabled = true;
		ship.enabled = false;
		box = new ContainerData();
	}
}
