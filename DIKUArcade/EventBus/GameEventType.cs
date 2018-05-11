namespace DIKUArcade.EventBus
{
    /// <summary>
    /// GameEventTypes identifying the different event systems in the game framework.
    /// E.g. `PlayerEvent` can be issued to a player object.
    /// </summary>
    public enum GameEventType
    {
        // TODO: Add/remove enumerations so that all relevant event types can be covered
        PlayerEvent,
        GraphicsEvent,
        InputEvent,
        ControlEvent,
        MovementEvent,
        SoundEvent,
        StatusEvent,
        GameStateEvent,
        WindowEvent,
        TimedEvent
    }
}