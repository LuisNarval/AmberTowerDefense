using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// The Event Bus is an Decoupling Design Pattern used to send events throughout the project.
/// It works like a radio station, there are classes that are register as an announcer and there are other that  
/// subscribe to their specific channel in order to listen to them.
/// This event bus is implementate as an Monostate Class.
/// </summary>

public class EventBus
{
    private static readonly IDictionary<string, UnityEvent> Events = new Dictionary<string, UnityEvent>();

    public static void Subscribe(string _eventType, UnityAction _listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(_eventType, out thisEvent))
        {
            thisEvent.AddListener(_listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(_listener);
            Events.Add(_eventType, thisEvent);
        }
    }

    public static void Unsubscribe(string _type, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(_type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(string _type)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(_type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
