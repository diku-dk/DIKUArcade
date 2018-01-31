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
                Message = message, Parameter1 = parameter1, Parameter2 = parameter2
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
            T sender, T processor, string message, string parameter1, string parameter2)
        {
            return new GameEvent<T>() { EventType = gameEventType, From = sender, To= processor,
                Message = message, Parameter1 = parameter1, Parameter2 = parameter2
            };
        }
    }
}