using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryShipmentCenter : MonoBehaviour
{
	// For testing purposes
	[SerializeField] private ItemFactory factory;
	[SerializeField] private Transform shipSpawnPos;
	[SerializeField] private Ship shipPrefab;
	[SerializeField] private PlanetaryShipmentCenterUI ui;
	private Planet selectedPlanet;
	// We are shipping the box on the ship, so the rest doesn't matter
	private Ship selectedShip;

	private List<Ship> shipsOnStandby = new List<Ship>();

	// Start is called before the first frame update
	void Start()
	{
		ui.OnShipSelected += OnShipSelected;
		// For testing spawn 4 ships
		ReadyForShipment(RandomCrate());
		ReadyForShipment(RandomCrate());
		ReadyForShipment(RandomCrate());
		ReadyForShipment(RandomCrate());
	}

	public void DeliverSelected()
	{
		// We need both a selected planet and a selected ship
		if (!selectedPlanet || !selectedShip) return;
		// Instantiate a ship
		shipsOnStandby.Remove(selectedShip);
		selectedShip.DeliverTo(selectedPlanet);

		selectedPlanet.Deselect();

		// Once shipped, you shouldn't be able to resend it again
		// No need to deselect, as it gets removed afterwards, but better safe than sorry
		ui.DeselectButton(selectedShip);
		ui.RemoveButton(selectedShip);

		selectedPlanet = null;
		selectedShip = null;
	}

	// TODO deselect stuff

	public void DeselectPlanet()
	{
		if (selectedPlanet) selectedPlanet.Deselect();
		selectedPlanet = null;
	}

	public void OnPlanetClicked(Planet planet)
	{
		if (selectedPlanet) selectedPlanet.Deselect();
		selectedPlanet = planet;
	}

	public void OnShipSelected(Ship ship)
	{
		if (selectedShip) ui.DeselectButton(selectedShip);
		selectedShip = ship;
	}

	// Mark a container/ship ready for shipment
	public void ReadyForShipment(ContainerData box)
	{
		// Instantiate a ship next to the base
		Ship ship = Instantiate<Ship>(shipPrefab, shipSpawnPos.position, shipSpawnPos.rotation);
		ship.box = box;
		ship.OnArrival += ShipHasReturned;
		shipsOnStandby.Add(ship);
		// Instantiate a button to select the ship
		ui.AddButton(ship);
	}

	public void ShipHasReturned(Ship ship)
	{
		// Delete Ship from list
		shipsOnStandby.Remove(ship);
		ship.OnArrival -= ShipHasReturned;
		Destroy(ship.gameObject);

		// TODO An Empty ship can be added to the packing screen

		// Maybe for now just call ReadyForShipment
		//! Testing
		ReadyForShipment(RandomCrate());
	}

	// For testing purposes only
	private ContainerData RandomCrate()
	{
		ContainerData data = new ContainerData();
		for (int i = 0; i < Random.Range(3, 5); i++)
		{
			ItemBoxData box = new ItemBoxData(BoxType.Type1);
			for (int j = 0; j < Random.Range(3, 5); j++)
			{
				box.AddToBox(factory.CreateRandomItem());
			}
			data.AddToBox(box);
		}
		return data;
	}
}
