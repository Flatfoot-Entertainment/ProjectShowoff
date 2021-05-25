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
        }
        subscribers[eventType] += eventHandler;
    }

    public void UnSubscribe(EventType eventType, EventHandler eventHandler)
    {
        if (!subscribers.ContainsKey(eventType))
        {
            Debug.LogWarning("subscriber not found oops, here's the event type: " + eventType);
            return;
        }
        else
        {
            subscribers[eventType] -= eventHandler;
        }
        // TODO maybe handle if the subscriber could not be found
        // I know this was here before, but maybe something other than a simple Debug.Log
    }

    public void InvokeEvent(Event e)
    {
        if (subscribers.ContainsKey(e.type))
        {
            subscribers[e.type]?.Invoke(e);
        }
    }
}
