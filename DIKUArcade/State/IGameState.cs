using DIKUArcade.Input;

namespace DIKUArcade.State {
    public interface IGameState {
        /// <summary>
        /// Reset this state to set its private variables to their initial default
        /// values. This is useful when e.g. leaving a game and entering a new game,
        /// where calling this method could reset the player's position, reset the
        /// score counter, or perform other similar actions.
        /// </summary>
        void ResetState();

        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        void UpdateState();
        
        /// <summary>
        /// Render all entities in this GameState
        /// </summary>
        void RenderState();
        
        /// <summary>
        /// Each state can react to key events, delegated from the host StateMachine.
        /// </summary>
        /// <param name="KeyboardAction">Enumeration representing key press/release.</param>
        /// <param name="KeyboardKey">Enumeration representing the keyboard key.</param>
        void HandleKeyEvent(KeyboardAction action, KeyboardKey key);
    }
}
