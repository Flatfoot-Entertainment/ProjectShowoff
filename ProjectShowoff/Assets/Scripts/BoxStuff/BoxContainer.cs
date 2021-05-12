using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainer : MonoBehaviour
{
	// TODO maybe make this in some way generic
	// -> This way we could store a smaller Box in a bigger box
	//    -> This is useful for the collective shipments (1. load in box -> 2. load in truck -> ship off)

	// TODO maybe in the future have a class that handles shipments.

	//private List<GameObject> containing = new List<GameObject>();
	private BoxLid lid;
	private BoxBody body;
	private Box box;
	public Box Box => box;

	[SerializeField] private float sampleBoxCost = 50.0f;
	[SerializeField] private ShippableBox shippableBoxPrefab;


    private void Start()
	{
        lid = GetComponentInChildren<BoxLid>();
        body = GetComponentInChildren<BoxBody>();
        box = new Box(BoxType.Type1);
		
		EventScript.Instance.EventQueue.AddEvent(new ManageMoneyEvent(-sampleBoxCost));

		lid.OnExitCallback += LidExit;
		lid.OnEnterCallback += LidEnter;
	}

	private void Update()
	{
		// TODO other thing than space
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("hello");
			EventScript.Instance.EventQueue.Update();
			EventScript.Instance.EventQueue.AddEvent(new ManageTimeEvent(20)); //must be moved somewhere else
			ShippableBox shippable = Instantiate<ShippableBox>(shippableBoxPrefab, transform.position + Vector3.up, transform.rotation, transform.parent);
			shippable.Init(GetComponent<BoxParts>().Dimensions, box);
			shippable.gameObject.layer = LayerMask.NameToLayer("Hookable"); //remove later on
			box = null;
			shippable.gameObject.SetActive(true);
			
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		lid.OnExitCallback -= LidExit;
		lid.OnEnterCallback -= LidEnter;
	}

	private void DestroyBox(float value)
	{
		Debug.Log("Contents sent...");
		box.ShowBoxContents();
		Destroy(gameObject);
	}

	private void LidExit(ItemScript subject)
	{
		// If the thing exiting the lid is in the body, it is fully in the box
		if (body.Has(subject.gameObject))
		{
			//containing.Add(subject);
			subject.transform.SetParent(transform);
			box.AddItemToBox(subject.Item);
			box.ShowBoxContents();
		}
	}

	// If something intersects with the Lid, it is not completely in the box anymore
	private void LidEnter(ItemScript subject)
	{
		if (body.Has(subject.gameObject))
		{
			subject.transform.parent = null;
			box.RemoveItemFromBox(subject.Item);
			box.ShowBoxContents();
		}
	}

	// TODO I hate this very much xD
	public static void Deliver(float value)
	{
		//OnBoxDelivered?.Invoke(value);
		EventScript.Instance.EventQueue.Update();
	}
}
