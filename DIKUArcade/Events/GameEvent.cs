namespace DIKUArcade.Events
{
    /// <summary>
    /// Represents an event which may be processed by certain subscribers of an event bus,
    /// and contains data fields which may be relevant to the receiver(s) of the event.
    /// </summary>
    public struct GameEvent
    {
        /// <summary>
        ///  EventType is a classifier to distinguish event system parts, e.g. sound, graphics and game logic.
        /// </summary>
        public GameEventType EventType;

        /// <summary>
        /// From is where did the message originate from
        /// </summary>
        public object From;

        /// <summary>
        /// To is where should the message go. If this value is not set,
        /// every GameEventProcessor registered in the event category EventType will receive this event.
        /// </summary>
        public IGameEventProcessor To;

        /// <summary>
        /// Message depends on the event type. Various event processors interpret it as commands,
        /// e.g. sound: SOUNDEFFECT_PLAY.
        /// </summary>
        public string Message;

        /// <summary>
        /// Parameter of the message/command, e.g. sound: sound filename or identifier 
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
