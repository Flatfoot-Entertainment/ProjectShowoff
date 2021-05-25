using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FulfillmentCenter : MonoBehaviour
{

	//TODO put docking space functionalities in a separate class
	[System.Serializable]
	public class DockingSpace
	{
		public Transform location;
		public Button closeButton;
		public bool isUnlocked = false;
		[HideInInspector] public GameObject lid;
		[HideInInspector] public bool free = false;
		[HideInInspector] public ContainerController container;
		[HideInInspector] public int index;
		[HideInInspector] public bool readyForShipment = false;
	}

	// Make it readonly, because we need to preprocess it with indices, etc.
	[PlayModeReadOnly, SerializeField] private List<DockingSpace> dockingSpaces;

	[SerializeField] private Transform boxPos;

	[SerializeField] private PlanetaryShipmentCenter planetaryShipment;

	[SerializeField] private GameObject lidPrefab;

	[SerializeField] private Transform shipParent;


	//things below should definitely be implemented in a seperate class
	private ItemBoxController fillableBox;
	// private ShippableItemBox shippedBox;
	private ContainerController fillableContainer;

	private void Start()
	{
		// Preprocess List - Docking stuff class
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
			if (dockingSpaces[i].isUnlocked) SpawnContainer(dockingSpaces[i]);
			else dockingSpaces[i].closeButton.gameObject.SetActive(false);
		}
	}

	public bool CanShipBox()
	{
		return fillableBox && fillableBox.Shippable;
	}

	public void CloseContainer(int index)
	{
		if (dockingSpaces[index].readyForShipment) return;
		// TODO ship the container, if money is available (and if theres a non shipped container)
		planetaryShipment.ReadyForShipment(dockingSpaces[index].container.Box, index);
		dockingSpaces[index].readyForShipment = true;
		dockingSpaces[index].lid.SetActive(true);
		dockingSpaces[index].closeButton.interactable = false;
	}

	public void CloseBox()
	{
		fillableBox.Close();
	}

	public void OnShipReturn()
	{
		SpawnContainer();
	}

	public void OnSendShip(int dockIndex)
	{
		if (dockIndex < 0 || dockIndex >= dockingSpaces.Count) return;
		Destroy(dockingSpaces[dockIndex].container.gameObject);
		dockingSpaces[dockIndex].container = null;
		dockingSpaces[dockIndex].free = true;
		dockingSpaces[dockIndex].readyForShipment = false;
		dockingSpaces[dockIndex].closeButton.gameObject.SetActive(false);
	}

	public void SpawnBox(GameObject boxPrefab)
	{
		if (fillableBox) return;
		//TODO finish
		//EventScript.Instance.EventManager.InvokeEvent(new ManageBoxSelectEvent(boxPrefab));
		fillableBox = Instantiate(boxPrefab, boxPos.position, Quaternion.identity).GetComponent<ItemBoxController>();
	}

	/// Spawn a container in the first free space
	/// Returns if there was a free space (so if a ship could be spawned successfully)

	//will not be necessary when using premade ships
	private bool SpawnContainer(DockingSpace dockSpace = null)
	{
		// TODO move ships in and out of dock
		if (dockSpace == null) dockSpace = dockingSpaces.Where((DockingSpace s) => s.free && s.isUnlocked).FirstOrDefault();
		if (dockSpace == null) return false;
		int index = dockingSpaces.IndexOf(dockSpace);
		dockSpace.container = BoxCreator.Instance.Create<ContainerData>(
			dockSpace.location.position,
			new Vector3(
				10f,
				4f,
				6f
			),
			shipParent
		).GetComponent<ContainerController>();

		dockSpace.lid = Instantiate(lidPrefab, dockSpace.location.position + Vector3.up * 4f, Quaternion.identity, dockSpace.container.transform);
		dockSpace.lid.transform.localScale = new Vector3(10f, 0.2f, 6f);
		dockSpace.lid.SetActive(false);

		dockSpace.free = false;
		dockSpace.readyForShipment = false;
		dockSpace.closeButton.gameObject.SetActive(true);
		return true;
	}

	//docking class
	public void AddShip()
	{
		var empty = dockingSpaces.Where((DockingSpace s) => !s.isUnlocked).FirstOrDefault();
		if (empty == null)
		{
			Debug.LogError("No more ships to spawn, bruh");
			return;
		}

		empty.isUnlocked = true;
		SpawnContainer(empty);
	}

	private void Update()
	{
		foreach (var space in dockingSpaces)
		{
			space.closeButton.interactable = space.container && space.container.Shippable;
		}
	}
}
