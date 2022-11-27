using System.Collections.Generic;

public class EnemyController
{
    public List<Enemy> enemies;
    GameStats stats;

    public int totalEnemiesCount = 0;

    public EnemyController()
    {
        enemies = new List<Enemy>();
        stats = GameManager.Instance.Stats;

        totalEnemiesCount = GameManager.Instance.EnviromentController.TotalEnemyCount;
    }

    public void Assign(Enemy enemy)
    {

    }

    public void Unassign(Enemy enemy)
    {
        totalEnemiesCount--;
        if (totalEnemiesCount == 0)
        {
            UserInterfaceManager.Open<WinPage>();
        }
    }

    public void OnLastPointReach(Enemy enemy)
    {
        stats.TackDamage(enemy.damage);
    }
}