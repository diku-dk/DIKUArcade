namespace DIKUArcade.State {
    public interface IGameState {
        /// <summary>
        /// The game loop can be structured differently depending on what the
        /// current game state needs.
        /// </summary>
        void GameLoop();
        /// <summary>
        /// Use this method to initialize all the GameState's variables.
        /// Call this method at the end of the constuctor.
        /// </summary>
        void InitializeGameState();
        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        void UpdateGameLogic();
        /// <summary>
        /// Render all entities in this GameState
        /// </summary>
        void RenderState();
        /// <summary>
        /// Each state can react to key events, delegated from the host StateMachine.
        /// </summary>
        /// <param name="keyAction">Eiter "KEY_PRESS" or "KEY_RELEASE".</param>
        /// <param name="keyValue">The string key value (see DIKUArcade.Input.KeyTransformer
        /// for details).</param>
        void HandleKeyEvent(string keyValue, string keyAction);
    }
}