using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreator : MonoBehaviour
{
	// TODO singletons are scummy
	[SerializeField] private ItemBoxParts itemBox;
	[SerializeField] private ContainerParts boxBox;

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

	public GameObject Create<BoxT>(Vector3 position, Vector3 dimensions, Transform parent)
	{
		switch (typeof(BoxT))
		{
			// Nice syntax xD
			case var cls when cls == typeof(ItemBoxData):
				{
					Debug.Log("Create Item Box");
					return CreateGenericHelper<ItemBoxData, Item>(itemBox, position, dimensions, parent);
				}
			case var cls when cls == typeof(ContainerData):
				{
					Debug.Log("Create Container");
					return CreateGenericHelper<ContainerData, ItemBoxData>(boxBox, position, dimensions, parent);
				}
		}
		// TODO
		return null;
	}

	private GameObject CreateGenericHelper<BoxT, Contained>(
		BoxParts<BoxT, Contained> prefab,
		Vector3 position, Vector3 dimensions,
		Transform parent)
		where BoxT : IBoxData<Contained>
	{
		BoxParts<BoxT, Contained> instantiated = parent ?
			Instantiate<BoxParts<BoxT, Contained>>(prefab, parent) :
			Instantiate<BoxParts<BoxT, Contained>>(prefab, position, Quaternion.identity);

		instantiated.SetDimensions(dimensions);

		instantiated.BodyObject.transform.localScale = dimensions;
		instantiated.BodyObject.transform.localPosition = new Vector3(
			0f,
			dimensions.y / 2f,
			0f
		);

		float wallThickness = instantiated.RightWall.transform.localScale.x / 2f;
		instantiated.RightWall.transform.localPosition = new Vector3(
			dimensions.x / 2f + wallThickness, dimensions.y / 2f, 0f
		);
		instantiated.RightWall.transform.localScale = new Vector3(
			instantiated.RightWall.transform.localScale.x,
			dimensions.y,
			dimensions.z
		);

		wallThickness = instantiated.LeftWall.transform.localScale.x / 2f;
		instantiated.LeftWall.transform.localPosition = new Vector3(
			-dimensions.x / 2f - wallThickness, dimensions.y / 2f, 0f
		);
		instantiated.LeftWall.transform.localScale = new Vector3(
			instantiated.LeftWall.transform.localScale.x,
			dimensions.y,
			dimensions.z
		);

		wallThickness = instantiated.FrontWall.transform.localScale.z / 2f;
		instantiated.FrontWall.transform.localPosition = new Vector3(
			0f, dimensions.y / 2f, -dimensions.z / 2f - wallThickness
		);
		instantiated.FrontWall.transform.localScale = new Vector3(
			dimensions.x,
			dimensions.y,
			instantiated.FrontWall.transform.localScale.z
		);

		wallThickness = instantiated.BackWall.transform.localScale.z / 2f;
		instantiated.BackWall.transform.localPosition = new Vector3(
			0f, dimensions.y / 2f, dimensions.z / 2f + wallThickness
		);
		instantiated.BackWall.transform.localScale = new Vector3(
			dimensions.x,
			dimensions.y,
			instantiated.BackWall.transform.localScale.z
		);

		float floorThickness = instantiated.Floor.transform.localScale.y / 2f;
		instantiated.Floor.transform.localPosition = new Vector3(
			instantiated.Floor.transform.localPosition.x,
			-floorThickness,
			instantiated.Floor.transform.localPosition.z
		);
		instantiated.Floor.transform.localScale = new Vector3(
			dimensions.x,
			instantiated.Floor.transform.localScale.y,
			dimensions.z
		);

		instantiated.LidObject.transform.localPosition = new Vector3(
			instantiated.Floor.transform.localPosition.x,
			dimensions.y,
			instantiated.Floor.transform.localPosition.z
		);
		instantiated.LidObject.transform.localScale = new Vector3(
			dimensions.x,
			instantiated.LidObject.transform.localScale.y,
			dimensions.z
		);

		instantiated.gameObject.SetActive(true);
		return instantiated.gameObject;
	}
}
