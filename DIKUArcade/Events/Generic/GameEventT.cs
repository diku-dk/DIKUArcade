namespace DIKUArcade.Events.Generic
{
    /// <summary>
    /// Generic equivalent of the DIKUArcade.Events.GameEvent struct.
    /// Represents an event which may be processed by certain subscribers of an event bus,
    /// and contains data fields which may be relevant to the receiver(s) of the event.
    /// </summary>
    /// <typeparam name="EventT">Enumeration type representing type of game events.</typeparam>
    public struct GameEvent<EventT> where EventT : System.Enum
    {
        /// <summary>
        ///  EventType is a classifier to distinguish event system parts, e.g. sound, graphics and game logic.
        /// </summary>
        public EventT EventType;

        /// <summary>
        /// From is where did the message originate from
        /// </summary>
        public object From;

        /// <summary>
        /// To is where should the message go. If this value is not set,
        /// every GameEventProcessor registered in the event category EventType will receive this event.
        /// </summary>
        public IGameEventProcessor<EventT> To;

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

        public int IntArg1;

        /// <summary>
        /// Assign a unique identification number to a GameEvent,
        /// to prevent the same event from being registered several times simultaneously.
        /// </summary>
        public uint Id;
    }
}
