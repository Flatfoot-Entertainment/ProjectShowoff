using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDeselector : MonoBehaviour
{
	[SerializeField] private PlanetaryShipmentCenter center;
	private void OnMouseDown()
	{
		if (center) center.DeselectPlanet();
	}
}
