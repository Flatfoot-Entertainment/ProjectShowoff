using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreatorTest : MonoBehaviour
{
	// Start is called before the first frame update
	private GameObject currentGameObject;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			if (currentGameObject)
			{
				Destroy(currentGameObject);
			}
			currentGameObject = BoxCreator.Instance.Create(
				new Vector3(0f, 1f, 0f),
				new Vector3(
					Random.Range(0.5f, 3f),
					Random.Range(0.5f, 3f),
					Random.Range(0.5f, 3f)
				),
				null
			);
			currentGameObject.SetActive(true);
		}
	}
}
