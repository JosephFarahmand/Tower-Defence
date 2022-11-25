using UnityEngine;

public class BuildController : MonoBehaviour
{
    [SerializeField] private BuildingCanvas canvas;

    private BuildingPoint _buildingPoint;

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

    public void SetCanvasDisable()
    {
        canvas.gameObject.SetActive(false);
    }

    public void SetBuildingPoint(BuildingPoint buildingPoint)
    {
        _buildingPoint = buildingPoint;
    }

    public void SetTowerToBuild(TowerType type, TowerLevel level)
    {
        var towerPrefab = GameManager.Instance.GameData.GetTower(type, level);
        _buildingPoint.CreateTower(towerPrefab, type, level);

        SetCanvasDisable();
    }
}
