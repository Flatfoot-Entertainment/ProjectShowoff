using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FulfillmentCenter : MonoBehaviour
{
	[SerializeField] private Transform boxPos;

	[SerializeField] private PlanetaryShipmentCenter planetaryShipment;

	private ItemBoxController fillableBox;

	public bool CanShipBox()
	{
		return fillableBox && fillableBox.Shippable;
	}

	public void CloseBox()
	{
		fillableBox.Close();
	}

	public void SpawnBox(GameObject boxPrefab)
	{
		if (fillableBox) return;
		//TODO finish
		//EventScript.Instance.EventManager.InvokeEvent(new ManageBoxSelectEvent(boxPrefab));
		fillableBox = Instantiate(boxPrefab, boxPos.position, Quaternion.identity).GetComponent<ItemBoxController>();
	}
}
