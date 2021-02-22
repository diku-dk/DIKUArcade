namespace DIKUArcade.EventBus
{
    /// <summary>
    /// Factory methods to generate game events.
    /// </summary>
    /// <typeparam name="T">Type of the game entities processed.</typeparam>
    public class GameEventFactory<T>
    {
        /// <summary>
        /// Create a game event for all processors and initialize it correctly for the processors.
        /// </summary>
        /// <param name="gameEventType">Game event type is used for choosing the appropriate processor queue.</param>
        /// <param name="sender">Who sends the event? The processor might have an internal logic how to process the event.</param>
        /// <param name="message">What is the message sended. Some processors expect here a command, e.g. PLAY_SOUND_EFFECT.</param>
        /// <param name="parameter1">Message specific parameter, e.g. for a sound the file name or id of a sound.</param>
        /// <param name="parameter2">Message specific parameter.</param>
        /// <returns>A fully initialized game event for all processors that can be registered in the game event bus.</returns>
        public static GameEvent<T> CreateGameEventForAllProcessors(GameEventType gameEventType, T sender, string message, string parameter1, string parameter2)
        {
            return new GameEvent<T>()
            {
                EventType = gameEventType, From= sender, To=default(T),
                Message = message, String1 = parameter1, String2 = parameter2
            };
        }

        // Overload that allows passing an object parameter to avoid casting multiple times when a different type payload is desired.
        public static GameEvent<T> CreateGameEventForAllProcessors(GameEventType gameEventType, T sender, string message, string string1, string string2, object object1)
        {
            return new GameEvent<T>()
            {
                EventType = gameEventType, From= sender, To=default(T),
                Message = message, String1 = string1, String2 = string2, Object1 = object1
            };
        }

        // Overload that allows passing two object parameters to avoid casting multiple times when a different type payload is desired.
        public static GameEvent<T> CreateGameEventForAllProcessors(GameEventType gameEventType, T sender, string message, string string1, string string2, object object1, object object2)
        {
            return new GameEvent<T>()
            {
                EventType = gameEventType, From= sender, To=default(T),
                Message = message, String1 = string1, String2 = string2, Object1 = object1, Object2 = object2
            };
        }

        /// <summary>
        /// Create a game event for a specific processors and initialize it correctly for the processors.
        /// </summary>
        /// <param name="gameEventType">Game event type is used for choosing the appropriate processor queue.</param>
        /// <param name="sender">Who sends the event? The processor might have an internal logic how to process the event.</param>
        /// <param name="processor">Who should process the event?</param>
        /// <param name="message">What is the message sended. Some processors expect here a command, e.g. PLAY_SOUND_EFFECT.</param>
        /// <param name="parameter1">Message specific parameter, e.g. for a sound the file name or id of a sound.</param>
        /// <param name="parameter2">Message specific parameter.</param>
        /// <returns>A fully initialized game event for all processors that can be registered in the game event bus.</returns>
        public static GameEvent<T> CreateGameEventForSpecificProcessor(GameEventType gameEventType,
            T sender, T processor, string message, string string1, string string2)
        {
            return new GameEvent<T>() { EventType = gameEventType, From = sender, To= processor,
                Message = message, String1 = string1, String2 = string2
            };
        }
    }
}