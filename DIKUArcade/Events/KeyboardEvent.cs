using DIKUArcade.Input;

namespace DIKUArcade.Events
{
    /// <summary>
    /// Represents a keyboard input event to be processed by an EventBus.
    /// </summary>
    public class KeyboardEvent
    {
        public KeyboardAction Action { get; set; }
    }
}
