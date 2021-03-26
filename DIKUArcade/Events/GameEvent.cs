namespace DIKUArcade.Events
{
    /// <summary>
    /// GameEvent is used in the GameEventBusSystem to encode events that are send between system parts.
    /// Events are generated using the GameEventFactory methods. Events are produced by event sources and
    /// are consumed by event sinks. Event sinks needs to be registered at the event bus for receiving events
    /// on the bus.
    /// Event sinks can register for multiple event types and event sources can generated different event types.
    /// </summary>
    public struct GameEvent<T>
    {       
        /// <summary>
        ///  EventType is a classifier to distinguish event system parts, e.g. sound, graphics and game logic.
        /// </summary>
        public GameEventType EventType;

        /// <summary>
        /// From is where did the message originate from
        /// </summary>
        public T From;
        
        /// <summary>
        /// To is where should the message go. If this value is not set,
        /// every GameEventProcessor registered in the event category EventType will receive this event.
        /// </summary>
        public IGameEventProcessor<T> To;
        
        /// <summary>
        /// Message depends on the event type. Various event processors interpret it as commands,
        /// e.g. sound: SOUNDEFFECT_PLAY.
        /// </summary>
        public string Message;

        /// <summary>
        /// Paramter of the message/command, e.g. sound: sound filename or identifier 
        /// </summary>
        public string StringArg1;
        
        /// <summary>
        /// Additional parameter for message/command
        /// </summary>
        public string StringArg2;
        
        /// <summary>
        /// Additional object parameter to pass different types.
        /// </summary>
        public object ObjectArg1;

        /// <summary>
        /// Assign a unique identification number to a GameEvent,
        /// to prevent the same event from being registered several times simultaneously.
        /// </summary>
        public uint Id;
    }
}
