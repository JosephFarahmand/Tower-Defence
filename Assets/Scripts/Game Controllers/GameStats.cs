using System;
using UnityEngine;

public class GameStats
{
    public float castleHealth = 100;

    public Action<float> onTackDamage;
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
            UserInterfaceManager.Open<LosePage>();
        }
    }
}
