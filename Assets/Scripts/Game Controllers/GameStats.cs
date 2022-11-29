using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GameStats
{
    private float castleHealth = 100;
    private State gameState;

    public event Action<State> OnChangeState;
    public Action<float> onTackDamage;

    public State GameState
    {
        get => gameState;
        private set
        {
            gameState = value;

            OnChangeState?.Invoke(gameState);

            switch (gameState)
            {
                case State.Home:
                    OnHomeState();
                    break;
                case State.Play:
                    OnPlayState();
                    break;
                case State.Resault_Win:
                    OnWinState();
                    break;
                case State.Resault_Lose:
                    OnLoseState();
                    break;
                case State.Reset:
                    OnResetState();
                    break;
            }
        }
    }
    public float MaxHealth { get; private set; }

    public GameStats(float health)
    {
        castleHealth = health;
        MaxHealth = health;
    }

    public void TackDamage(float value)
    {
        castleHealth -= value;
        onTackDamage?.Invoke(castleHealth);

        if (castleHealth <= 0)
        {
            ChangeState(State.Resault_Lose);
        }
    }

    public void ChangeState(State newState) => GameState = newState;

    private void OnHomeState()
    {
        TimeScaleHelper.ChangeTimeScale(TimeScaleHelper.TimeScale.x0);
    }

    private void OnPlayState()
    {
        TimeScaleHelper.ChangeTimeScale(TimeScaleHelper.TimeScale.x1);
        UserInterfaceManager.Open<GamePage>();
    }

    private void OnWinState()
    {
        TimeScaleHelper.ChangeTimeScale(TimeScaleHelper.TimeScale.x0);
        UserInterfaceManager.Open<WinPage>();
    }

    private void OnLoseState()
    {
        TimeScaleHelper.ChangeTimeScale(TimeScaleHelper.TimeScale.x0);
        UserInterfaceManager.Open<LosePage>();
    }

    private void OnResetState()
    {
        castleHealth = MaxHealth;
        onTackDamage?.Invoke(castleHealth);
        ChangeState(State.Play);
    }

    public enum State
    {
        Home,
        Play,
        Resault_Win,
        Resault_Lose,
        Reset
    }
}
