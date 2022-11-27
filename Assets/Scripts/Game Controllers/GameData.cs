using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private List<TowerData> towers;
    [SerializeField] private List<Enemy> enemies;

    #region Tower

    public Tower GetTower(TowerType type, TowerLevel level) => towers.Find(x => x.Equals(type)).GetTower(level);
    public int GetTowerPrice(TowerType type, TowerLevel level) => towers.Find(x => x.Equals(type)).GetTowerPrice(level);

    #endregion

    #region Enemy

    public Enemy GetEnemy(CharacterType type) => enemies.FindAll(x => x.Type == type).RandomItem();
    public Enemy GetRandomEnemy() => enemies.RandomItem();

    #endregion

}