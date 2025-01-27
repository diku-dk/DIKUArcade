namespace DIKUArcade.Events;

using DIKUArcade.Timers;
using System;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.Linq;

/// <summary>
/// A GameEventBus that uses Action delegates for handling events. This implementation
/// allows for the registration of events, subscribing and unsubscribing of actions,
/// and processing of both immediate and timed events.
/// </summary>
/// <remarks>Parallel event processing is not supported.</remarks>
public class GameEventBus {
    private IdGenerator idGenerator = new IdGenerator();
    private Dictionary<Type, object> subscribers = new Dictionary<Type, object>();
    private Queue<object> gameEventQueue = new Queue<object>();
    private SortedList<ulong, TimedGameEvent>[] timedEvents =
        new SortedList<ulong, TimedGameEvent>[2] {
            new SortedList<ulong, TimedGameEvent>(),
            new SortedList<ulong, TimedGameEvent>()
        };
    private int activeTimedEvent = 0;
    private int inactiveTimedEvent = 1;

    /// <summary>
    /// Initializes a new instance of the GameEventBus class.
    /// </summary>
    public GameEventBus() { }

    /// <summary>
    /// Initializes a new instance of the GameEventBus class with a specified window. 
    /// Registers a key event handler for the window.
    /// </summary>
    /// <param name="window">The window to which the key event handler will be attached.</param>
    public GameEventBus(Window window) {
        window.SetKeyEventHandler((action, key) => {
            (KeyboardAction Action, KeyboardKey Key) input = (action, key);
            RegisterEvent(input);
        });
    }

    /// <summary>
    /// Subscribes an Action delegate to the event bus, allowing it to listen for events 
    /// of a specified type.
    /// </summary>
    /// <typeparam name="Arg">The type of event argument the Action will handle.</typeparam>
    /// <param name="action">The Action delegate to subscribe.</param>
    /// <exception cref="ArgumentNullException">Thrown if the action is null.</exception>
    public void Subscribe<Arg>(Action<Arg> action) {
        if (action is null) {
            throw new ArgumentNullException(nameof(action));
        } else if (subscribers.ContainsKey(typeof(Arg))) {
            var temp = (Action<Arg>) subscribers[typeof(Arg)] + action;
            subscribers[typeof(Arg)] = temp;
            return;
        }
        
        subscribers.Add(typeof(Arg), action);
    }

    /// <summary>
    /// Unsubscribes an Action delegate from the event bus, stopping it from receiving events.
    /// </summary>
    /// <typeparam name="Arg">The type of event argument the Action was handling.</typeparam>
    /// <param name="action">The Action delegate to unsubscribe.</param>
    /// <exception cref="ArgumentNullException">Thrown if the action is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the action was never subscribed.</exception>
    public void Unsubscribe<Arg>(Action<Arg> action) {
        if (action is null) {
            throw new ArgumentNullException(nameof(action));
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
    /// Registers an event to the event queue.
    /// </summary>
    /// <typeparam name="Arg">The type of the event argument.</typeparam>
    /// <param name="arg">The event argument to be queued.</param>
    public void RegisterEvent<Arg>(Arg arg) {
        gameEventQueue.Enqueue(arg!);
    }

    /// <summary>
    /// Processes all queued events and timed events, notifying the appropriate subscribers.
    /// </summary>
    public void ProcessEvents() {
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
    /// Clears the event queue, removing all pending events.
    /// </summary>
    public void Flush() {
        gameEventQueue.Clear();
    }

    /// <summary>
    /// Registers a timed event to be processed after a specified time period.
    /// </summary>
    /// <typeparam name="Arg">The type of the event argument.</typeparam>
    /// <param name="arg">The event argument to be queued as a timed event.</param>
    /// <param name="timePeriod">The time period after which the event will be processed.</param>
    /// <returns>The unique ID of the registered timed event.</returns>
    public ulong RegisterTimedEvent<Arg>(Arg arg, TimePeriod timePeriod) {
        var id = idGenerator.Get();

        var timedEvent = new TimedGameEvent(arg!, timePeriod);
        timedEvents[activeTimedEvent].Add(id, timedEvent);
        return id;
    }

    /// <summary>
    /// Adds or resets a timed event in the event bus.
    /// </summary>
    /// <typeparam name="Arg">The type of the event argument.</typeparam>
    /// <param name="arg">The event argument to be added or reset.</param>
    /// <param name="id">The unique ID of the timed event to be reset.</param>
    /// <param name="timePeriod">The time period after which the event will be processed.</param>
    public void AddOrResetTimedEvent<Arg>(Arg arg, ulong id, TimePeriod timePeriod) {
        var timedEvent = new TimedGameEvent(arg!, timePeriod);

        if (!timedEvents[activeTimedEvent].TryAdd(id, timedEvent)) {
            timedEvents[activeTimedEvent][id] = timedEvent;
        }
    }

    /// <summary>
    /// Cancels a timed event by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the timed event to be canceled.</param>
    /// <returns>True if the event was canceled; otherwise, false.</returns>
    public bool CancelTimedEvent(ulong id) {
        if (!HasTimedEvent(id)) {
            return false;
        }

        timedEvents[activeTimedEvent].Remove(id);
        idGenerator.Remove(id);
        return true;
    }

    /// <summary>
    /// Checks if a timed event exists in the event bus by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the timed event.</param>
    /// <returns>True if the event exists; otherwise, false.</returns>
    public bool HasTimedEvent(ulong id) {
        return idGenerator.Contains(id);
    }

    /// <summary>
    /// Resets the time period of a timed event by its ID.
    /// </summary>
    /// <param name="id">The unique ID of the timed event to be reset.</param>
    /// <param name="period">The new time period for the event.</param>
    /// <returns>True if the event was successfully reset; otherwise, false.</returns>
    public bool ResetTimedEvent(ulong id, TimePeriod period) {
        if (!HasTimedEvent(id)) {
            return false;
        }

        var reset = new TimedGameEvent(timedEvents[activeTimedEvent][id].GameEvent, period);
        timedEvents[activeTimedEvent][id] = reset;

        return true;
    }

    /// <summary>
    /// Processes all timed events, adding expired events to the event queue.
    /// </summary>
    private void ProcessTimedEvents() {
        timedEvents[inactiveTimedEvent].Clear();

        var currentTime = StaticTimer.GetElapsedMilliseconds();

        foreach (var timedEvent in timedEvents[activeTimedEvent]) {
            if (timedEvent.Value.HasExpired(currentTime)) {
                RegisterEvent(timedEvent.Value.GameEvent);
                idGenerator.Remove(timedEvent.Key);
            } else {
                timedEvents[inactiveTimedEvent].Add(timedEvent.Key, timedEvent.Value);
            }
        }

        SwapTimedEvents();
    }

    /// <summary>
    /// Swaps the active and inactive timed event buffers.
    /// </summary>
    private void SwapTimedEvents() {
        activeTimedEvent = (activeTimedEvent + 1) % 2;
        inactiveTimedEvent = (inactiveTimedEvent + 1) % 2;
    }
}
