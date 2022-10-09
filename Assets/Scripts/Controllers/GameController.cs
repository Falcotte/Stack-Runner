using UnityEngine;
using UnityEngine.Events;
using StackRunner.StackSystem;
using StackRunner.InputSystem;

namespace StackRunner
{
    public enum GameState
    {
        Idle,
        Gameplay,
        GameWin,
        GameLose
    }

    public class GameController : MonoSingleton<GameController>
    {
        private GameState currentState = GameState.Idle;
        public GameState CurrentState => currentState;

        public static UnityAction OnGameStart;
        public static UnityAction OnGameWin, OnGameLose;

        [SerializeField] private StackSpawner stackSpawner;
        [SerializeField] private StackPath stackPath;

        private void OnEnable()
        {
            InputController.OnTouchDown += EvaluatePlayerAction;
        }

        private void OnDisable()
        {
            InputController.OnTouchDown -= EvaluatePlayerAction;
        }

        public void EvaluatePlayerAction()
        {
            if(currentState == GameState.Idle)
            {
                StartGame();
                stackSpawner.SpawnNewStack();
            }
            else if(currentState == GameState.Gameplay)
            {
                stackPath.ProcessLastStack();
            }
        }

        public void StartGame()
        {
            currentState = GameState.Gameplay;
            OnGameStart?.Invoke();
        }

        public void WinGame()
        {
            currentState = GameState.GameWin;
            OnGameWin?.Invoke();
        }

        public void LoseGame()
        {
            currentState = GameState.GameLose;
            OnGameLose?.Invoke();
        }
    }
}