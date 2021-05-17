using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippableBox<BoxT, Contained> : MonoBehaviour where BoxT : IBoxData<Contained>
{
	public delegate void ShipmentCallback();
	public event ShipmentCallback OnShipment;
	public BoxT Box => box;
	private BoxT box;
	private bool delivered;

	protected virtual void OnInit() { }

	public void Init(Vector3 dimentions, BoxT box)
	{
		delivered = false;
		this.box = box;
		transform.localScale = dimentions;
		OnInit();
	}

	protected void Deliver()
	{
		if (delivered) return;
		OnShipment?.Invoke();
		delivered = true;
	}

	private void OnDestroy()
	{
		OnShipment = null;
	}

}
