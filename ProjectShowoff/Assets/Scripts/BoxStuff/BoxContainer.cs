using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BoxDeliveredCallback(Box box);
public class BoxContainer : MonoBehaviour
{
	// TODO maybe in the future have a class that handles shipments.
	// That way, we don't have a static event, which might mess with lifetimes, etc.
	public static event BoxDeliveredCallback OnBoxDelivered;
	//private List<GameObject> containing = new List<GameObject>();
	private BoxLid lid;
	private BoxBody body;
	private Box box;

	private void Awake()
	{
		lid = GetComponentInChildren<BoxLid>();
		body = GetComponentInChildren<BoxBody>();
		box = new Box(BoxType.Type1);
	}

	private void Start()
	{
		lid.OnExitCallback += LidExit;
		lid.OnEnterCallback += LidEnter;
		OnBoxDelivered += DestroyBox;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnBoxDelivered?.Invoke(box);
		}
	}

	private void OnDestroy()
	{
		lid.OnExitCallback -= LidExit;
		lid.OnEnterCallback -= LidEnter;
		OnBoxDelivered -= DestroyBox;
	}

	private void DestroyBox(Box box)
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
}
