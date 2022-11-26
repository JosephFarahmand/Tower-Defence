using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newTowerData", menuName = "Data/Tower", order = 1)]
public class TowerData : ScriptableObject
{
    [SerializeField] private TowerType type;
    [SerializeField] private List<TowerInfo> towers;
    public Tower GetTower(TowerLevel level) => towers.Find(x => x.level == level).prefab;

    [System.Serializable]
    private struct TowerInfo
    {
        public TowerLevel level;
        public Tower prefab;
    }

    public bool Equals(TowerType type)
    {
        return this.type == type;
    }
}
public enum TowerLevel
{
    Lv1,
    Lv2,
    Lv3,
    Lv4A,
    Lv4B,
}

public enum TowerType
{
    Empty,
    Cannon,
    Archer
}