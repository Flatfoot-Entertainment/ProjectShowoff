using UnityEngine;
using UnityEngine.UI;

public class PlanetaryShipmentCenter : MonoBehaviour
{
	private Planet selectedPlanet;
	[SerializeField] private ShipComponents ship;
	[SerializeField] private Button shipButton;

	private void Start()
	{
		ship.Ship.OnArrival += OnShipReturned;
		ship.LoadingHandler.OnContentsChanged += () => shipButton.interactable = !ship.LoadingHandler.IsEmpty();
		shipButton.interactable = !ship.LoadingHandler.IsEmpty();
	}

	public void DeliverSelected()
	{
		// We need both a selected planet and a selected ship
		if (!selectedPlanet) return;
		// fulfillmentCenter.OnSendShip(shipsOnStandby[selectedShip]); //TODO its own event?
		// Instantiate a ship
		ship.LoadingHandler.Ship();
		ship.Ship.DeliverTo(selectedPlanet);
		shipButton.interactable = false;

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

	private void OnShipReturned()
	{
		shipButton.interactable = true;
	}
}
