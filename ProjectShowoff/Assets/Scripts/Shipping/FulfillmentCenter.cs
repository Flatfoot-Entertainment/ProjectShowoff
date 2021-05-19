using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FulfillmentCenter : MonoBehaviour
{
	[SerializeField] private Transform containerPos;
	[SerializeField] private Transform boxPos;
	[SerializeField] private PlanetaryShipmentCenter planetaryShipment;
	private ItemBoxController fillableBox;
	private ShippableItemBox shippedBox;
	private ContainerController fillableContainer;
	private ShippableContainer shippedContainer;

	private void Start()
	{
		SpawnContainer();
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

	public void CloseContainer()
	{
		// TODO ship the container, if money is available (and if theres a non shipped container)
		// TODO maybe destroy the box???

		// TODO notify planetary shipment center about a new available ship

		planetaryShipment.ReadyForShipment(fillableContainer.Box);

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

	private void SpawnContainer()
	{
		if (fillableContainer) return;
		fillableContainer = BoxCreator.Instance.Create<ContainerData>(
			containerPos.position,
			new Vector3(
				Random.Range(2.5f, 3f),
				Random.Range(2f, 3f),
				Random.Range(2f, 3f)
			),
			null
		).GetComponent<ContainerController>();
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
