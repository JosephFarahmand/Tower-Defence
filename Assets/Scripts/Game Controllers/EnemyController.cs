using System;
using System.Collections.Generic;
using System.Diagnostics;

public class EnemyController
{
    public List<Enemy> enemies;
    GameStats stats;

    private int enemiesCount = 0;

    public EnemyController()
    {
        enemies = new List<Enemy>();
        stats = GameManager.Instance.Stats;

        stats.OnChangeState += Stats_OnChangeState;

        enemiesCount = GameManager.Instance.EnviromentController.TotalEnemyCount;
    }

    private void Stats_OnChangeState(GameStats.State currentState)
    {
        if (currentState == GameStats.State.Reset)
        {
            ResetGame();
        }
    }

    public void Assign(Enemy enemy)
    {
        if (stats.GameState != GameStats.State.Play) return;

        enemies.Add(enemy);
    }

    public void Unassign(Enemy enemy)
    {
        if (stats.GameState != GameStats.State.Play) return;

        enemiesCount--;
        if (enemiesCount == 0)
        {
            GameManager.Instance.Stats.ChangeState(GameStats.State.Resault_Win);
        }
    }

    public void OnLastPointReach(Enemy enemy)
    {
        if (stats.GameState != GameStats.State.Play) return;

        stats.TackDamage(enemy.damage);
    }

    private void ResetGame()
    {
        foreach(var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.DestroyAterDead();
            }
        }

        enemies = new List<Enemy>();
        enemiesCount = GameManager.Instance.EnviromentController.TotalEnemyCount;
    }
}