using UnityEngine;

public class ShippableBox<BoxT, Contained> : MonoBehaviour where BoxT : IBoxData<Contained>
{
	// A callback for when this box is shipped
	public event System.Action OnShipment;
	public BoxT Box { get; private set; }
	// A flag to prevent a box from being shipped twice
	private bool delivered;

	// A callback to subclasses, called when Init is called
	protected virtual void OnInit() { }

	public void Init(BoxT box)
	{
		delivered = false;
		this.Box = box;
		OnInit();
	}

	// A function for subclasses to call when their specific requirements for shipment are fulfilled
	// (A trigger hit, etc.)
	protected void Deliver()
	{
		if (delivered) return;
		OnShipment?.Invoke();
		delivered = true;
	}

	private void OnDestroy()
	{
		// Reset the callback for safety
		OnShipment = null;
	}
}
