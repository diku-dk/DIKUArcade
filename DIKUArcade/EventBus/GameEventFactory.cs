using System.Runtime.InteropServices.WindowsRuntime;

namespace DIKUArcade.EventBus
{
    public class GameEventFactory<T>
    {
        public static GameEvent<T> CreateGameEventForAllProcessors(GameEventType gameEventType, T sender, string message, string parameter1, string parameter2)
        {
            return new GameEvent<T>()
            {
                EventType = gameEventType, From= sender, To=default(T),
                Message = message, Parameter1 = parameter1, Parameter2 = parameter2
            };
        }

        public static GameEvent<T> CreateGameEventForSpecificProcessor(GameEventType gameEventType,
            T sender, T processor, string message, string parameter1, string parameter2)
        {
            return new GameEvent<T>() { EventType = gameEventType, From = sender, To= processor,
                Message = message, Parameter1 = parameter1, Parameter2 = parameter2
            };
        }
    }
}