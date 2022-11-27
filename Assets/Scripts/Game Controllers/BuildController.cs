using UnityEngine;

public class BuildController : MonoBehaviour
{
    [SerializeField] private BuildingCanvas canvas;

    private BuildingPoint selectedBuildingPoint;

    private void Start()
    {
        SetCanvasDisable();
    }

    public void SetCanvasEnable(BuildingPoint buildingPoint = null)
    {
        if (buildingPoint == null)
        {
            SetCanvasDisable();
            return;
        }
        canvas.gameObject.SetActive(true);
        canvas.transform.position = buildingPoint.transform.position;
        canvas.SetValues(buildingPoint);
        SetBuildingPoint(buildingPoint);
    }

    private void SetBuildingPoint(BuildingPoint buildingPoint)
    {
        selectedBuildingPoint = buildingPoint;
    }

    public void SetTowerToBuild(TowerType type, TowerLevel level)
    {
        var towerPrefab = GameManager.Instance.GameData.GetTower(type, level);
        selectedBuildingPoint.CreateTower(towerPrefab, type, level);

        SetCanvasDisable();
    }

    public void ClearBuilding()
    {
        selectedBuildingPoint.ClearTower();

        SetCanvasDisable();
    }

    public void SetCanvasDisable()
    {
        canvas.gameObject.SetActive(false);
    }
}
