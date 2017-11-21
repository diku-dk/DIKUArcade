using OpenTK.Input;

namespace DIKUArcade.States
{
    public interface IState
    {
        /// <summary>
        /// Delegate method for a state to handle key input.
        /// Based on return value, the state machine using an implementation
        /// of this interface may do the appropriate state transition.
        /// </summary>
        /// <param name="key">Key code</param>
        /// <param name="action">Key event action (press/release)</param>
        /// <returns>The state to which to do a transition</returns>
        GameStates HandleKeyEvent(OpenTK.Input.Key key, KeyPressAction action);

        /// <summary>
        /// Render all entities attached to this state.
        /// </summary>
        void Render();

        /// <summary>
        /// Update game logic of all entities attached to this state.
        /// </summary>
        void Update();
    }
}