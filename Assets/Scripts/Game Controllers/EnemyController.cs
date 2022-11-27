using System.Collections.Generic;

public class EnemyController
{
    public List<Enemy> enemies;
    GameStats stats;

    public EnemyController()
    {
        enemies = new List<Enemy>();
        stats = GameManager.Instance.Stats;
    }

    public void Assign(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void Unassign(Enemy enemy)
    {
        enemies.Remove(enemy);

        if(enemies.Count == 0)
        {
            UserInterfaceManager.Open<WinPage>();
        }
    }

    public void OnLastPointReach(Enemy enemy)
    {
        stats.TackDamage(enemy.damage);
    }
}