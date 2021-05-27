using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Tooling/Item Collider Settings")]
public class ItemColliderSettings : ScriptableObject
{
	public SerializableDictionary<string, GameObject> simpleColliderPrefabs
		= new SerializableDictionary<string, GameObject>();
	public SerializableDictionary<string, GameObject> complexColliderPrefabs 
		= new SerializableDictionary<string, GameObject>();
}
