using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetaryShipmentCenterUI : MonoBehaviour
{
	public event System.Action<Ship> OnShipSelected;
	[SerializeField] private Transform buttonParent;
	[SerializeField] private ShipSelectionUI buttonPrefab;

	private Dictionary<Ship, ShipSelectionUI> lookup = new Dictionary<Ship, ShipSelectionUI>();

	public void AddButton(Ship ship)
	{
		var ui = Instantiate<ShipSelectionUI>(buttonPrefab, buttonParent);
		ui.gameObject.SetActive(true);
		ui.Ship = ship;
		ui.OnSelect += () =>
		{
			OnShipSelected?.Invoke(ship);
			// Can be immediatelly removed -> should not be selected again
		};
		lookup.Add(ship, ui);
	}

	public void RemoveButton(Ship ship)
	{
		if (lookup.ContainsKey(ship))
		{
			Destroy(lookup[ship].gameObject);
			lookup.Remove(ship);
		}
	}
}
