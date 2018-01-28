namespace DIKUArcade.EventBus
{
    /// <summary>
    /// GameEventTypes identifying the different event systems in the game framework.
    /// </summary>
    public enum GameEventType
    {
        PlayerEntityEvent,
        NonPlayerEntityEvent,
        GraphicsEvent,
        InputEvent,
        ControlEvent, //?
        SoundEvent,
        StatusEvent,
        GameStateEvent,
        NetworkEvent
    }
}