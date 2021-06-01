using UnityEngine;
using UnityEngine.UI;

public class PlanetaryShipmentCenter : MonoBehaviour
{
	private Planet selectedPlanet;
	[SerializeField] private ShipComponents ship;
	[SerializeField] private Button shipButton;

	private bool inShipment = false;

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
		
		inShipment = true;
	}

	public void OnPlanetClicked(Planet planet)
	{
		// Only allow selecting a planet, if you can actually ship stuff
		if (inShipment || ship.LoadingHandler.IsEmpty()) return;
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
		inShipment = false;
	}
}
