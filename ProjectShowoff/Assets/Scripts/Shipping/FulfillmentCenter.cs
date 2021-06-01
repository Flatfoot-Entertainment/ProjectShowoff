using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FulfillmentCenter : MonoBehaviour
{
	[SerializeField] private Transform boxPos;

	[SerializeField] private PlanetaryShipmentCenter planetaryShipment;

	private ItemBoxController fillableBox;

	[Header("Obstructed space")] 
	
	[SerializeField] private Vector3 center;
	[SerializeField] private Vector3 halfExtends;
	[SerializeField] private Vector3 orientation;
	[SerializeField] private LayerMask mask;
	
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		var oldMat = Gizmos.matrix;
		Matrix4x4 rot = Matrix4x4.Translate(boxPos.position + center) * Matrix4x4.Rotate(Quaternion.Euler(orientation));
		Gizmos.matrix = rot;
		
		Gizmos.DrawWireCube(Vector3.zero, halfExtends * 2);
		
		Gizmos.matrix = oldMat;
	}
#endif

	public bool CanShipBox()
	{
		return fillableBox && fillableBox.Shippable;
	}

	public void CloseBox()
	{
		fillableBox.Close();
	}

	public void SpawnBox(GameObject boxPrefab)
	{
		if (fillableBox) return;
		//TODO finish
		//EventScript.Instance.EventManager.InvokeEvent(new ManageBoxSelectEvent(boxPrefab));
		fillableBox = Instantiate(boxPrefab, boxPos.position, Quaternion.identity).GetComponent<ItemBoxController>();
	}

	public bool SpaceIsFree()
	{
		// TODO i actually don't like this approach to much anymore, as it has to be queried every frame
		var coll = new Collider[1];
		return Physics.OverlapBoxNonAlloc(
			boxPos.position + center,
			halfExtends, 
			coll,
			Quaternion.Euler(orientation), mask
		) == 0;
	}
}
