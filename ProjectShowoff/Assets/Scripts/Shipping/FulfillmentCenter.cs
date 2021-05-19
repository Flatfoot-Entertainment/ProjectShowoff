using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FulfillmentCenter : MonoBehaviour
{
	[System.Serializable]
	public class DockingSpace
	{
		public Transform location;
		public Button closeButton;
		[HideInInspector] public bool free = false;
		[HideInInspector] public ContainerController container;
		[HideInInspector] public int index;
		[HideInInspector] public bool readyForShipment = false;
	}

	// Make it readonly, because we need to preprocess it with indices, etc.
	[PlayModeReadOnly, SerializeField] private List<DockingSpace> dockingSpaces;

	[SerializeField] private Transform boxPos;
	[SerializeField] private PlanetaryShipmentCenter planetaryShipment;
	private ItemBoxController fillableBox;
	private ShippableItemBox shippedBox;
	private ContainerController fillableContainer;
	private ShippableContainer shippedContainer;

	private void Start()
	{
		// Preprocess List
		for (int i = 0; i < dockingSpaces.Count; i++)
		{
			Debug.Log($"Index A {i}");
			dockingSpaces[i].index = i;
			int val = i; // Clone i, because captures suck
			dockingSpaces[i].closeButton.onClick.AddListener(() =>
			{
				CloseContainer(val);
			});
			dockingSpaces[i].free = true;
		}
		foreach (var _ in dockingSpaces) SpawnContainer();
		//SpawnBox();
	}

	public bool CanShipBox()
	{
		return fillableBox && fillableBox.Shippable;
	}

	public bool CanShipContainer()
	{
		return fillableContainer && fillableContainer.Shippable;
	}

	public void OnShipReturn()
	{
		// TODO let specific ship return by moving in the correct place
		SpawnContainer();
	}

	public void OnSendShip(int dockIndex)
	{
		// TODO validate index
		Destroy(dockingSpaces[dockIndex].container.gameObject);
		dockingSpaces[dockIndex].container = null;
		dockingSpaces[dockIndex].free = true;
		dockingSpaces[dockIndex].readyForShipment = false;
	}

	public void CloseContainer(int index)
	{
		if (dockingSpaces[index].readyForShipment) return;
		// TODO ship the container, if money is available (and if theres a non shipped container)
		// TODO maybe destroy the box???
		planetaryShipment.ReadyForShipment(dockingSpaces[index].container.Box, index);
		dockingSpaces[index].readyForShipment = true;

		// shippedContainer = (ShippableContainer)fillableContainer.Ship();
		// if (!shippedContainer)
		// {
		// 	Debug.LogWarning("ShippedContainer --- wrong type!!!");
		// 	return;
		// }

		// // at this point the shipped stage is reached and we can set up events
		// shippedContainer.OnShipment += () =>
		// {
		// 	SpawnContainer();
		// 	// TODO also add money
		// 	Debug.Log("Shipment");
		// 	EventScript.Instance.EventQueue.AddEvent(new ManageMoneyEvent(shippedContainer.Box.MoneyValue));
		// 	EventScript.Instance.EventQueue.AddEvent(new ManageTimeEvent(20)); // TODO must be moved somewhere else
		// };
	}

	public void CloseBox()
	{
		StartCoroutine(StartBoxClosing());
	}

	public void SpawnBox()
	{
		if (fillableBox) return;
		fillableBox = BoxCreator.Instance.Create<ItemBoxData>(
			boxPos.position,
			new Vector3(
				Random.Range(1f, 2f),
				Random.Range(0.75f, 1.25f),
				Random.Range(1f, 2f)
			),
			null
		).GetComponent<ItemBoxController>();
	}

	public void SpawnBox(GameObject boxPrefab)
	{
		//tobias help
		if (fillableBox) return;
		fillableBox = Instantiate(boxPrefab, boxPos.position, Quaternion.identity).GetComponent<ItemBoxController>();
	}

	/// Spawn a container in the first free space
	/// Returns if there was a free space (so if a ship could be spawned successfully)
	private bool SpawnContainer()
	{
		// TODO move ships in and out of dock
		var empty = dockingSpaces.Where((DockingSpace s) => s.free).FirstOrDefault();
		Debug.Log("A");
		if (empty == null) return false;
		Debug.Log("B");
		int index = dockingSpaces.IndexOf(empty);
		empty.container = BoxCreator.Instance.Create<ContainerData>(
			empty.location.position,
			new Vector3(
				10f,
				4f,
				6f
			),
			null
		).GetComponent<ContainerController>();
		empty.free = false;
		empty.readyForShipment = false;
		return true;
	}

	private IEnumerator StartBoxClosing()
	{
		Animator boxAnimator = fillableBox.GetComponentInChildren<Animator>();
		if (boxAnimator == null) Debug.LogError("BoxAnimator not found.");
		else
		{
			Debug.Log("Found a BoxAnimator");
			boxAnimator.SetBool("isClosing", true);
			yield return new WaitForSeconds(fillableBox.ClosingAnimation.length);
			FinalizeBoxClosing();
		}
	}

	private void FinalizeBoxClosing()
	{
		// TODO ship the box, if money is available (and if theres a non shipped box)
		shippedBox = (ShippableItemBox)fillableBox.Ship();
		if (!shippedBox)
		{
			Debug.LogWarning("ShippedBox --- wrong type!!!");
			return;
		}
		// at this point the shipped stage is reached and we can set up events
		shippedBox.OnShipment += () =>
		{
			SpawnBox();
		};
	}

	// OTHER THINGS
	// void SpawnBox() -> don't spawn box automatically
}
