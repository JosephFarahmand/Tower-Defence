using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] private List<Archer> archers;

    protected override void Shoot(Transform target)
    {
        if (target == null) return;
        archers.ForEach(x => x.Shoot(target));
    }
}
