using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventHandler(Event e);
public class EventManager
{
	private Dictionary<EventType, EventHandler> subscribers;
	private List<Event> events;

	public EventManager()
	{
		subscribers = new Dictionary<EventType, EventHandler>();
		events = new List<Event>();
	}

	public void Subscribe(EventType eventType, EventHandler eventHandler)
	{
		if (!subscribers.ContainsKey(eventType))
		{
			EventHandler handler = null;
			subscribers.Add(eventType, handler);
			Debug.Log("Added event of type " + eventType);
		}
		subscribers[eventType] += eventHandler;
	}

	public void UnSubscribe(EventType eventType, EventHandler eventHandler)
	{
		if (subscribers.ContainsKey(eventType))
		{
			subscribers[eventType] -= eventHandler;
		}
		else
		{
			Debug.Log("ey bro this subscriber is missin");
		}
	}

	public void AddEvent(Event e)
	{
		Debug.Log("Event added: " + e.type);
		// events.Add(e);
		if (subscribers.ContainsKey(e.type))
		{
			subscribers[e.type]?.Invoke(e);
		}
	}

	// public void Update()
	// {
	// 	for (int i = events.Count - 1; i >= 0; i--)
	// 	{
	// 		Event e = events[i];
	// 		Debug.Log("Event type to be invoked: " + e.type);
	// 		if (subscribers.ContainsKey(e.type))
	// 		{
	// 			subscribers[e.type]?.Invoke(e);
	// 		}
	// 		else
	// 		{
	// 			Debug.Log("event type: " + e.type + " dont exist in subscribers bruh");
	// 		}
	// 		events.Remove(e);
	// 	}
	// }
}
