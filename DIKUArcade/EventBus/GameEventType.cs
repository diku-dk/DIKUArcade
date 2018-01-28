namespace DIKUArcade.EventBus
{
    /// <summary>
    /// GameEventTypes identifying the different event systems in the game framework.
    /// </summary>
    public enum GameEventType
    {
        PlayerEvent,
        NonPlayerEvent,
        GraphicsEvent,
        InputEvent,
        ControlEvent, //?
        MovementEvent,
        SoundEvent,
        StatusEvent,
        GameStateEvent
    }
}