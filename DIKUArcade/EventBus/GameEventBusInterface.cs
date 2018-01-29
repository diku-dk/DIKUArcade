using System.Collections.Generic;

namespace DIKUArcade.EventBus
{
    public interface IGameEventBus<T>
    {
        /// <summary>
        /// Subscribe a game event processor to process events of eventType.
        /// </summary>
        /// <param name="eventType">Type of events which a processors wants to process, e.g. sound events.</param>
        /// <param name="gameEventProcessor">Reference to game event processor.</param>
        void Subscribe(GameEventType eventType, IGameEventProcessor<T> gameEventProcessor);
        /// <summary>
        /// Unsubscribe a game event processor to process events of eventType.
        /// </summary>
        /// <param name="eventType">Type of events the processor registered for, e.g. sound events.</param>
        /// <param name="gameEventProcessor">Reference to game event processor, i.e. this.</param>
        void Unsubscribe(GameEventType eventType, IGameEventProcessor<T> gameEventProcessor);
        /// <summary>
        /// Register event for processing in the game event bus.
        /// </summary>
        /// <see cref=""/>
        /// <param name="gameEvent">Game event to be processed.</param>
        void RegisterEvent(GameEvent<T> gameEvent);
    }
    
    public interface IGameEventBusController<T>
    {
        /// <summary>
        /// Initialize the game event bus with a list of event types that need to be processed. 
        /// The architecture is static and does not allow additional event types after the initialization.
        /// </summary>
        /// <param name="eventTypeList">List of events which shall be processed by the game event bus.</param>
        void InitializeEventBus(ICollection<GameEventType> eventTypeList);
        /// <summary>
        /// Process events contained in the event queues in parallel. All event processors are called for events that they registered for.
        /// </summary>
        /// <param name="processOrder">Order of game events for processing. This can be used for prioritizing event types, e.g. control before sound.</param>
        void ProcessEvents(IEnumerable<GameEventType> processOrder);
        /// <summary>
        /// Process events contained in the event queues in parallel. All event processors are called for events that they registered for.
        /// The default order of events used is the order of registering event types.
        /// </summary>
        void ProcessEvents();
        /// <summary>
        /// Process events contained in the event queues sequentially. All event processors are called for events that they registered for.
        /// </summary>
        /// <param name="processOrder">Order of game events for processing. This can be used for prioritizing event types, e.g. control before sound.</param>
        void ProcessEventsSequentially(IEnumerable<GameEventType> processOrder);
        /// <summary>
        /// Process events contained in the event queues sequentially. All event processors are called for events that they registered for.
        /// The default order of events used is the order of registering event types.
        /// </summary>
        void ProcessEventsSequentially();
        /// <summary>
        /// Break processing of events for all event queues. This method is called due to real-time restrictions of processing.
        /// TODO: Filter event types to let game logic pass.
        /// </summary>
        void BreakProcessing();
        /// <summary>
        /// Resets the break command. This should be called by the game control loop before registering new events.
        /// </summary>
        void ResetBreakProcessing();
        /// <summary>
        /// Flush all event queues at once. This method is called due to real-time restrictions of processing.
        /// This method should be called by the game control loop after processing all events. Internally BreakProcessing is called.
        /// TODO: Filter event types to let game logic pass.
        /// </summary>
        void Flush();
    }
}