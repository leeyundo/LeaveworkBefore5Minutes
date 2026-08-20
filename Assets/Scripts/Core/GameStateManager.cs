using System;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Victory
}

public sealed class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;

    public GameState CurrentState { get; private set; } = GameState.Playing;

    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (gameTimer != null)
            gameTimer.OnTimerFinished += SetGameOver;
    }

    private void OnDestroy()
    {
        if (gameTimer != null)
            gameTimer.OnTimerFinished -= SetGameOver;
    }

    public void SetPlaying() => SetState(GameState.Playing);
    public void SetPaused() => SetState(GameState.Paused);
    public void SetGameOver() => SetState(GameState.GameOver);
    public void SetVictory() => SetState(GameState.Victory);

    private void SetState(GameState state)
    {
        if (CurrentState == state)
            return;

        CurrentState = state;
        OnStateChanged?.Invoke(CurrentState);
    }
}
