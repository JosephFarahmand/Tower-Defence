public static class Shop
{
    public static bool CanPurcheseTower(int towerPrice)
    {
        return GameManager.Instance.ProfileController.HasEnoughGold(towerPrice);
    }

    public static void PurcheseTower(TowerType type, TowerLevel level)
    {
        var towerPrice = GameManager.Instance.GameData.GetTowerPrice(type, level);
        if (!CanPurcheseTower(towerPrice)) return;
        GameManager.Instance.ProfileController.AddGold(-towerPrice);
        GameManager.Instance.BuildController.SetTowerToBuild(type, level);
    }
}
