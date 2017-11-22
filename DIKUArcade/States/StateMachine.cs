using System.ComponentModel;
using OpenTK.Input;

namespace DIKUArcade.States {
    public class StateMachine {
        private IState _activeState;

        private IState _stateGamePaused;

        // TODO: Could use Dictionary as public data structure to dynamically add/remove states?
        private IState _stateGameRunning;

        private IState _stateMainMenu;

        public StateMachine() {
            _activeState = _stateGamePaused;
            // in this constructor, all internal state objects should be initialized,
            // as far that it is possible (desired) to do so.
        }

        private ApplicationStates HandleKeyEvent(Key key, KeyPressAction action) {
            // sample implementation - will always be specific to the game being developed
            if (key == Key.Escape) {
                return ApplicationStates.Quit;
            }
            switch (_activeState.HandleKeyEvent(key, action)) {
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

        private void Render() {
            _activeState.Render();
        }

        private void Update() {
            _activeState.Update();
        }
    }
}