namespace DIKUArcade.Events.Action;

using DIKUArcade.Timers;
using System;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Input;

/// <summary>
/// Alternative GameEventBus which uses Action delegates instead of using some thing like 
/// IGameEventProcessor subscribers.
/// </summary>
/// <remarks>
/// Currently it does not allow for parallel event processing.
/// </remark>
public class GameEventBus {

    private Dictionary<Type, object> subscribers;
    private Queue<object> gameEventQueue;
    private SortedList<uint, TimedGameEvent>[] timedEvents;
    private int activeTimedEvent = 0;
    private int inactiveTimedEvent = 1;
    private uint currId = 0;

    public GameEventBus() {
        subscribers = new Dictionary<Type, object>();
        gameEventQueue = new Queue<object>();

        timedEvents = new SortedList<uint, TimedGameEvent>[2] {
            new SortedList<uint, TimedGameEvent>(),
            new SortedList<uint, TimedGameEvent>()
        };
    }

    public GameEventBus(Window window) {
        subscribers = new Dictionary<Type, object>();
        gameEventQueue = new Queue<object>();

        timedEvents = new SortedList<uint, TimedGameEvent>[2] {
            new SortedList<uint, TimedGameEvent>(),
            new SortedList<uint, TimedGameEvent>()
        };

        window.SetKeyEventHandler((action, key) => {
            (KeyboardAction Action, KeyboardKey Key) input = (action, key);
            RegisterEvent(input);
        });
    }

    /// <summary>
    /// Method for subscribing delegates such they can listen for events. Any action of the same 
    /// type will will be notified.
    /// </summary>
    /// <param name="action">
    /// Some Action which takes a single argument that will listen for events.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    /// Thrown when action is null.
    /// </exception>
    public void Subscribe<Arg>(Action<Arg> action) {
        if (action is null) {
            throw new ArgumentNullException("action");
        } else if (subscribers.ContainsKey(typeof(Arg))) {
            var temp = (Action<Arg>) subscribers[typeof(Arg)] + action;
            subscribers[typeof(Arg)] = temp;
            return;
        }
        
        subscribers.Add(typeof(Arg), action);
    }

    /// <summary>
    /// Method for unsubscribing delegates such they stop listening for events.
    /// </summary>
    /// <param name="action">
    /// Some Action which takes a single argument that will listen for events.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    /// Thrown when action is null.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    /// Thrown when action have never been subscribed before.
    /// </exception>
    public void Unsubscribe<Arg>(Action<Arg> action) {
        if (action is null) {
            throw new ArgumentNullException("action cannot be null.");
        } else if (!subscribers.ContainsKey(typeof(Arg))) {
            throw new ArgumentException($"{action} was never subscribed.");
        }

        var subscriber = (Action<Arg>) subscribers[typeof(Arg)];
        var newSubscriber = subscriber - action;

        if (newSubscriber is null) {
            subscribers.Remove(typeof(Arg));
            return;
        } else if (subscriber == newSubscriber) {
            throw new ArgumentException($"{action} was never subscribed.");
        }

        subscribers[typeof(Arg)] = newSubscriber;
    }

    /// <summary>
    /// Method for appending an event to the event queue.
    /// </summary>
    /// <param name="arg">
    /// The argument that will be passed to the subscribers. It will notify the subscribers 
    /// that takes the same parameter type.
    /// </param>
    public void RegisterEvent<Arg>(Arg arg) {
        gameEventQueue.Enqueue(arg!);
    }

    /// <summary>
    /// Method that will notify subscribers with timed events and events from the event queue. It 
    /// will only notify subscribers with timed events when they expire.
    /// </summary>
    public void ProcessEventsSequentially() {
        ProcessTimedEvents();

        while (gameEventQueue.Count != 0) {
            var gameEvent = gameEventQueue.Dequeue();

            if (subscribers?[gameEvent.GetType()] is null) {
                continue;
            }

            var action = subscribers[gameEvent.GetType()].GetType().GetMethod("Invoke");
            action?.Invoke(subscribers[gameEvent.GetType()], new object[] {gameEvent});
        }
    }

    /// <summary>
    /// Method for removing events in the queue.
    /// </summary>
    public void Flush() {
        gameEventQueue.Clear();
    }
    
    /// <summary>
    /// Method for Swapping timed event buffers.
    /// </summary>
    private void SwapTimedEvents() {
        activeTimedEvent = (activeTimedEvent + 1) % 2;
        inactiveTimedEvent = (inactiveTimedEvent + 1) % 2;
    }

    /// <summary>
    /// Method that will check if the timed events have expired and adds them to the event queue.
    /// </summary>
    private void ProcessTimedEvents() {
        timedEvents[inactiveTimedEvent].Clear();

        var currentTime = StaticTimer.GetElapsedMilliseconds();

        foreach (var timedEvent in timedEvents[activeTimedEvent]) {
            if (timedEvent.Value.HasExpired(currentTime)) {
                RegisterEvent(timedEvent.Value.GameEvent);
            } else {
                timedEvents[inactiveTimedEvent].Add(timedEvent.Key, timedEvent.Value);
            }
        }
        
        SwapTimedEvents();
    }

    /// <summary>
    /// Method for appending an timed event to the timed event list.
    /// </summary>
    /// <param name="arg">
    /// The argument that will be passed to the Actions subscribing. It will notify the subscribers 
    /// which takes the same parameter.
    /// </param>
    /// <param name="period">
    /// The time it takes before the event expires.
    /// </param>
    /// <return>
    /// The ID of the event.
    /// </return>
    /// <exception cref="System.ArgumentException">
    /// Thrown when the action have never been subscribed before.
    /// </exception>
    public uint RegisterTimedEvent<Arg>(Arg arg, TimePeriod timePeriod) {
        var id = currId;
        currId++;
        if (HasTimedEvent(id)) {
            throw new ArgumentException("The id must be unique.");
        }

        var timedEvent = new TimedGameEvent(arg!, timePeriod);
        timedEvents[activeTimedEvent].Add(id, timedEvent);
        return id;
    }

    /// <summary>
    /// Method for appending or resetting an timed event. It will reset the event based on the ID so
    /// if there is a duplicate ID it will reset to the given period and arg.
    /// </summary>
    /// <param name="arg">
    /// The argument that will be passed to the Actions subscribing. It will notify the subscribers 
    /// which takes the same parameter.
    /// </param>
    /// <param name="id">
    /// The unique id of that timed event.
    /// </param>
    /// <param name="period">
    /// The time it takes before the event expires.
    /// </param>
    public void AddOrResetTimedEvent<Arg>(Arg arg, uint id, TimePeriod timePeriod) {
        var timedEvent = new TimedGameEvent(arg!, timePeriod);

        if (!timedEvents[activeTimedEvent].TryAdd(id, timedEvent)) {
            timedEvents[activeTimedEvent][id] = timedEvent;
        }
    }

    /// <summary>
    /// Method for removing timed events based by their ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the timed event that will be cancelled.
    /// </param>
    /// <return>
    /// True if the timed event got cancelled else the timed event did not exist and therefore 
    /// could not be cancelled.
    /// </return>
    public bool CancelTimedEvent(uint id) {
        if (!HasTimedEvent(id)) {
            return false;
        }

        timedEvents[activeTimedEvent].Remove(id);
        return true;
    }

    /// <summary>
    /// Method for checking if an ID exists in event bus.
    /// </summary>
    /// <param name="id">
    /// The ID of the timed event that is searched for.
    /// </param>
    /// <return>
    /// True if it exist else false.
    /// </return>
    public bool HasTimedEvent(uint id) {
        return timedEvents[activeTimedEvent].ContainsKey(id);
    }

    /// <summary>
    /// Method for changing the time of a timed event.
    /// </summary>
    /// <param name="id">
    /// The ID of the timed event to be reset.
    /// </param>
    /// <param name="period">
    /// The time it takes before the event expires.
    /// </param>
    /// <return>
    /// True if it could be changed else false if the ID did not exist.
    /// </return>
    public bool ResetTimedEvent(uint id, TimePeriod period) {
        if (!HasTimedEvent(id)) {
            return false;
        }

        var reset = new TimedGameEvent(timedEvents[activeTimedEvent][id].GameEvent, period);
        timedEvents[activeTimedEvent][id] = reset;

        return true;
    }
}