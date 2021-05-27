using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
[RequireComponent(typeof(ShipLoadingHandler))]
public class ShipComponents : MonoBehaviour
{
	public Ship Ship { get; private set; }
	public ShipLoadingHandler LoadingHandler { get; private set; }

	private void Awake()
	{
		Ship = GetComponent<Ship>();
		LoadingHandler = GetComponent<ShipLoadingHandler>();
	}
}
