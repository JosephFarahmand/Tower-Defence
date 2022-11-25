using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private List<TowerData> towers;

    public Tower GetTower(TowerType type, TowerLevel level) => towers.Find(x => x.Equals(type)).GetTower(level);

    public T GetTower<T>(TowerType type, TowerLevel level) where T : Tower => GetTower(type, level) as T;
}