using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreator : MonoBehaviour
{
	// TODO singletons are scummy
	[SerializeField] private BoxParts box;

	public static BoxCreator Instance { get; private set; }

	private void Awake()
	{
		if (Instance)
		{
			Debug.LogWarning($"BoxCreator instance is already filled with {Instance.name}. Destroying {gameObject.name} which treid to take its place.");
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}

	public GameObject Create(Vector3 position, Vector3 dimensions, Transform parent)
	{
		BoxParts instantiated = parent ?
			Instantiate<BoxParts>(box, parent) :
			Instantiate<BoxParts>(box, position, Quaternion.identity);
		instantiated.BodyObject.transform.localScale = dimensions;
		instantiated.RightWall.transform.position = new Vector3(
			dimensions.x / 2f, dimensions.y / 2f, 0f
		);
		instantiated.RightWall.transform.localScale = new Vector3(
			instantiated.RightWall.transform.localScale.x,
			instantiated.RightWall.transform.localScale.y,
			dimensions.z
		);

		instantiated.LeftWall.transform.position = new Vector3(
			-dimensions.x / 2f, dimensions.y / 2f, 0f
		);
		instantiated.LeftWall.transform.localScale = new Vector3(
			instantiated.RightWall.transform.localScale.x,
			instantiated.RightWall.transform.localScale.y,
			dimensions.z
		);

		instantiated.FrontWall.transform.position = new Vector3(
			0f, dimensions.y / 2f, -dimensions.z / 2f
		);

		instantiated.FrontWall.transform.localScale = new Vector3(
			dimensions.x,
			instantiated.RightWall.transform.localScale.y,
			instantiated.RightWall.transform.localScale.z
		);

		instantiated.BackWall.transform.position = new Vector3(
			0f, dimensions.y / 2f, dimensions.z / 2f
		);
		instantiated.BackWall.transform.localScale = new Vector3(
			dimensions.z,
			instantiated.RightWall.transform.localScale.y,
			instantiated.RightWall.transform.localScale.z
		);

		// instantiated.LidObject.transform.localScale =

		return instantiated.gameObject;
	}
}
