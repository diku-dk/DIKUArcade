using System.Collections.Specialized;
using System.ComponentModel;
using OpenTK.Input;

namespace DIKUArcade.States
{
    public class StateMachine
    {
        // TODO: Could use Dictionary as public data structure to dynamically add/remove states?
        private IState _stateGameRunning;
        private IState _stateGamePaused;
        private IState _stateMainMenu;

        private IState _activeState;

        public StateMachine()
        {
            _activeState = _stateGamePaused;
            // in this constructor, all internal state objects should be initialized,
            // as far that it is possible (desired) to do so.
        }

        ApplicationStates HandleKeyEvent(OpenTK.Input.Key key, KeyPressAction action)
        {
            // sample implementation - will always be specific to the game being developed
            if (key == Key.Escape)
            {
                return ApplicationStates.Quit;
            }
            switch (_activeState.HandleKeyEvent(key, action))
            {
                case GameStates.QuitGame:
                    return ApplicationStates.Quit;
                case GameStates.Continue:
                    return ApplicationStates.Running;

                case GameStates.GamePaused:
                    _activeState = _stateGamePaused;
                    return ApplicationStates.Running;

                case GameStates.GameRunning:
                    _activeState = _stateGameRunning;
                    return ApplicationStates.Running;

                case GameStates.MainMenu:
                    _activeState = _stateMainMenu;
                    return ApplicationStates.Running;

                default:
                    throw new InvalidEnumArgumentException();
            }

        }

        void Render()
        {
            _activeState.Render();
        }

        void Update()
        {
            _activeState.Update();
        }
    }
}