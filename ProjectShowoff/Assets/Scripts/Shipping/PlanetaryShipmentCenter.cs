using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryShipmentCenter : MonoBehaviour
{
	private Planet selectedPlanet;
	[SerializeField] private ShipComponents ship;

	public void DeliverSelected()
	{
		// We need both a selected planet and a selected ship
		if (!selectedPlanet) return;
		// fulfillmentCenter.OnSendShip(shipsOnStandby[selectedShip]); //TODO its own event?
		// Instantiate a ship
		ship.LoadingHandler.Ship();
		ship.Ship.DeliverTo(selectedPlanet);

		selectedPlanet.Deselect();

		selectedPlanet = null;
	}

	public void OnPlanetClicked(Planet planet)
	{
		if (selectedPlanet) selectedPlanet.Deselect();
		selectedPlanet = planet;
		selectedPlanet.Select();
	}
	public void DeselectPlanet()
	{
		if (selectedPlanet) selectedPlanet.Deselect();
		selectedPlanet = null;
	}
}
