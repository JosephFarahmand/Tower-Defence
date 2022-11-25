
using System.Collections.Generic;
using UnityEngine;

public class BuildingPoint : MonoBehaviour
{
    BuildController buildController;

    [SerializeField] private Color hoverColor;
    [SerializeField] private GameObject defaultBuilding;

    Dictionary<Renderer, Color> _renderers;

    private Tower currentTower;
    private List<Tower> towers;

    public TowerType currentTowerType { get; private set; } = TowerType.Empty;
    public TowerLevel currentTowerLevel { get; private set; }

    protected virtual void Start()
    {
        _renderers = new Dictionary<Renderer, Color>();
        towers = new List<Tower>();

        buildController = GameManager.Instance.BuildController;
        SetRenderers();
    }

    private void SetRenderers()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer renderer = renderers[i];
            if (_renderers.ContainsKey(renderer)) continue;
            _renderers.Add(renderer, renderer.material.color);
        }
    }

    private void OnMouseDown()
    {
        buildController.SetCanvasEnable(this);
    }

    private void OnMouseOver()
    {
        HoverColor();
    }

    private void OnMouseEnter()
    {
        HoverColor();
    }

    private void OnMouseExit()
    {
        DefaultColor();

        buildController.SetCanvasDisable();
    }

    private void HoverColor()
    {
        foreach (var info in _renderers)
        {
            Renderer renderer = info.Key;
            if (!renderer.gameObject.activeInHierarchy) continue;
            renderer.material.color = hoverColor;
        }
    }

    private void DefaultColor()
    {
        foreach (var info in _renderers)
        {
            Renderer renderer = info.Key;
            if (!renderer.gameObject.activeInHierarchy) continue;
            renderer.material.color = info.Value;
        }
    }

    public void CreateTower(Tower towerPrefab, TowerType type, TowerLevel level)
    {
        defaultBuilding.SetActive(false);

        var tower = towers.Find(x => x.Type == type && x.Level == level);
        if (tower == null)
        {
            currentTower = Instantiate(towerPrefab, transform.position, transform.rotation);
            currentTower.transform.SetParent(transform);
            towers.Add(currentTower);
        }
        else
        {
            tower.gameObject.SetActive(true);
            currentTower = tower;
        }

        SetInfo(type, level);

        SetRenderers();
    }

    private void SetInfo(TowerType type, TowerLevel level)
    {
        currentTowerType = type;
        currentTowerLevel = level;
    }

    public void ClearTower()
    {
        defaultBuilding.SetActive(true);
        currentTower.gameObject.SetActive(false);
    }
}
