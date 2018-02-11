namespace DIKUArcade.State {
    public interface IGameState {
        // TODO: Should we have an `InitializeGameState()` method, so that `ResetGameState` will simply call initialize?
        void GameLoop();
        void ResetGameState();
        void UpdateGameLogic();
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