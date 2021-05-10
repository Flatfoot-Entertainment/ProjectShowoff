using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxParts : MonoBehaviour
{
	[SerializeField] private BoxContainer container;
	public BoxContainer Container => container;

	public GameObject MainObject => gameObject;

	[SerializeField] private GameObject floor;
	public GameObject Floor => floor;
	[SerializeField] private GameObject leftWall;
	public GameObject LeftWall => leftWall;
	[SerializeField] private GameObject rightWall;
	public GameObject RightWall => rightWall;
	[SerializeField] private GameObject frontWall;
	public GameObject FrontWall => frontWall;
	[SerializeField] private GameObject backWall;
	public GameObject BackWall => backWall;
	[SerializeField] private BoxBody body;
	public BoxBody Body => body;
	public GameObject BodyObject => body.gameObject;
	[SerializeField] private BoxLid lid;
	public GameObject LidObject => lid.gameObject;
}
