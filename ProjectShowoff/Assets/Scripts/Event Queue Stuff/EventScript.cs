using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
	public static EventScript Instance;
	public EventManager EventQueue => eventQueue;

	private EventManager eventQueue;
	// Start is called before the first frame update
	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(Instance);
		}
		eventQueue = new EventManager();

	}
}
