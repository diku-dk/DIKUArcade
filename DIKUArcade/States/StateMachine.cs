using System.ComponentModel;
using OpenTK.Input;

namespace DIKUArcade.States {
    public class StateMachine {
        private IState activeState;

        private IState stateGamePaused;

        private IState stateGameRunning;

        private IState stateMainMenu;

        public StateMachine() {
            activeState = stateGamePaused;
            // in this constructor, all internal state objects should be initialized,
            // as far that it is possible (desired) to do so.
        }

        private ApplicationStates HandleKeyEvent(Key key, KeyPressAction action) {
            // sample implementation - will always be specific to the game being developed
            if (key == Key.Escape) {
                return ApplicationStates.Quit;
            }
            switch (activeState.HandleKeyEvent(key, action)) {
            case GameStates.QuitGame:
                return ApplicationStates.Quit;
            case GameStates.Continue:
                return ApplicationStates.Running;

            case GameStates.GamePaused:
                activeState = stateGamePaused;
                return ApplicationStates.Running;

            case GameStates.GameRunning:
                activeState = stateGameRunning;
                return ApplicationStates.Running;

            case GameStates.MainMenu:
                activeState = stateMainMenu;
                return ApplicationStates.Running;

            default:
                throw new InvalidEnumArgumentException();
            }
        }

        private void Render() {
            activeState.Render();
        }

        private void Update() {
            activeState.Update();
        }
    }
}