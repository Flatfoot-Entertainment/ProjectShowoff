using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BoxDeliveredCallback(float value);
public delegate void BoxSentCallback(float value);
public abstract class BoxContainer<BoxT, Contained, ValType> : MonoBehaviour where BoxT : IBox<Contained, ValType>
{
	// TODO maybe make this in some way generic
	// -> This way we could store a smaller Box in a bigger box
	//    -> This is useful for the collective shipments (1. load in box -> 2. load in truck -> ship off)

	// TODO maybe in the future have a class that handles shipments.
	// That way, we don't have a static event, which might mess with lifetimes, etc.
	public static event BoxDeliveredCallback OnBoxDelivered;
	public static event BoxSentCallback OnBoxSent;
	//private List<GameObject> containing = new List<GameObject>();
	private BoxLid lid;
	private BoxBody body;
	private ItemBox box;
	public abstract BoxT Box { get; }

	[SerializeField] private float finalPositionThreshold = 0.1f;
	[SerializeField] private float sampleBoxCost = 50.0f;
	[SerializeField] private ShippableBox shippableBoxPrefab;

	private void Awake()
	{
		lid = GetComponentInChildren<BoxLid>();
		body = GetComponentInChildren<BoxBody>();
		// box = new ItemBox(BoxType.Type1);
		OnAwake();
	}

	protected virtual void OnAwake() { }

	private void Start()
	{
		lid.OnExitCallback += LidExit;
		lid.OnEnterCallback += LidEnter;
	}

	private void Update()
	{
		// TODO other thing than space
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnBoxSent?.Invoke(sampleBoxCost);
			ShippableBox shippable = Instantiate<ShippableBox>(shippableBoxPrefab, transform.position, transform.rotation, transform.parent);
			shippable.Init(GetComponent<BoxParts>().Dimensions, box);
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
		// box.ShowBoxContents();
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
			// box.ShowBoxContents();
		}
	}

	// If something intersects with the Lid, it is not completely in the box anymore
	private void LidEnter(ItemScript subject)
	{
		if (body.Has(subject.gameObject))
		{
			subject.transform.parent = null;
			box.RemoveItemFromBox(subject.Item);
			// TODO call functions in child classes
			// box.ShowBoxContents();
		}
	}

	// TODO I hate this very much xD
	public static void Deliver(float value)
	{
		OnBoxDelivered?.Invoke(value);
	}
}
