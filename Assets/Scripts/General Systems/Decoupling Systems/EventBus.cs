using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// The Event Bus is an Decoupling Design Pattern used to send events throughout the project.
/// It works like a radio station, there are classes that are register as an announcer and there are other that  
/// subscribe to their specific channel in order to listen to them.
/// This event bus is implemented as an Monostate Class.
/// </summary>

public enum GameEvent 
{
    COUNTDOWN, STARTGAME, SPAWN, PAUSE, GAMELOOSE, GAMEWIN, RESTART, QUIT
}

public class EventBus
{
    private static readonly IDictionary<GameEvent, UnityEvent> Events = new Dictionary<GameEvent, UnityEvent>();

    public static void Subscribe(GameEvent _eventType, UnityAction _listener)
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

    public static void Unsubscribe(GameEvent _type, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(_type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(GameEvent _type)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(_type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

}