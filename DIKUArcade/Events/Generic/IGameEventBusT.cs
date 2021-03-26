namespace DIKUArcade.Events.Generic
{
    /// <summary
    /// Generic interface equivalent for the DIKUArcade.Events.IGameEventBus.
    /// Interface for an event bus which may subscribe receivers to certain types of
    /// events, unsubsribe them again, and register events to be processed for one or
    /// more registered event processors.
    /// </summary>
    /// <typeparam name="EventT">Enumeration type representing type of game events.</typeparam>
    public interface IGameEventBus<EventT> where EventT : System.Enum
    {
        /// <summary>
        /// Subscribe a game event processor to process events of eventType.
        /// </summary>
        /// <param name="eventType">Type of events which a processors wants to process, e.g. sound events.</param>
        /// <param name="gameEventProcessor">Reference to game event processor.</param>
        void Subscribe(EventT eventType, IGameEventProcessor<EventT> gameEventProcessor);

        /// <summary>
        /// Unsubscribe a game event processor to process events of eventType.
        /// </summary>
        /// <param name="eventType">Type of events the processor registered for, e.g. sound events.</param>
        /// <param name="gameEventProcessor">Reference to game event processor, i.e. this.</param>
        void Unsubscribe(EventT eventType, IGameEventProcessor<EventT> gameEventProcessor);

        /// <summary>
        /// Register event for processing in the game event bus.
        /// </summary>
        /// <see cref=""/>
        /// <param name="gameEvent">Game event to be processed.</param>
        void RegisterEvent(GameEvent<EventT> gameEvent);
    }
}
