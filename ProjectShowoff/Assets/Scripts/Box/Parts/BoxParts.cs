using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxParts<BoxT, Contained> : MonoBehaviour where BoxT : IBoxData<Contained>
{
	private Vector3 dimensions;
	public Vector3 Dimensions => dimensions;

	public void SetDimensions(Vector3 dim)
	{
		dimensions = dim;
	}

	public BoxT Box => Container.Box;

	[SerializeField] private BoxController<BoxT, Contained> container;
	public BoxController<BoxT, Contained> Container => container;

	public GameObject MainObject => gameObject;

	[SerializeField] private GameObject floor;
	public GameObject Floor => floor;
	[SerializeField] private GameObject leftWall;
	// Negative X
	public GameObject LeftWall => leftWall;
	[SerializeField] private GameObject rightWall;
	// Positive X
	public GameObject RightWall => rightWall;
	[SerializeField] private GameObject frontWall;
	// Negative X
	public GameObject FrontWall => frontWall;
	[SerializeField] private GameObject backWall;
	// Positive Z
	public GameObject BackWall => backWall;
	[SerializeField] private BoxBody body;
	public BoxBody Body => body;
	public GameObject BodyObject => body.gameObject;
	[SerializeField] private BoxLid<Contained> lid;
	public GameObject LidObject => lid.gameObject;
}
