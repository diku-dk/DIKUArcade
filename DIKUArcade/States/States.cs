namespace DIKUArcade.States {
    /// <summary>
    ///     Example configuration of game states to structure control flow
    ///     in any game using DIKUArcade.
    /// </summary>
    public enum GameStates {
        QuitGame,
        Continue,
        GameRunning,
        GamePaused,

        MainMenu
        // ...
    }

    /// <summary>
    ///     States that the StateMachine propagates on key input back to
    ///     the application's main routine, indicating whether or not
    ///     the program should halt.
    /// </summary>
    public enum ApplicationStates {
        Quit, // used from main routine to quit application
        Running // take no action
    }

    public enum KeyPressAction {
        KeyActionPress,
        KeyActionRelease
    }
}