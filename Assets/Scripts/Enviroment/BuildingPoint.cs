using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingPoint : MonoBehaviour
{
    [SerializeField] private Color hoverColor;

    private Renderer _renderer;
    private Color _startColor;

    private Tower _tower;
    public TowerType currentTowerType { get; private set; } = TowerType.Empty;
    public TowerLevel currentTowerLevel { get; private set; }

    BuildController buildController;

    private void Start()
    {
        buildController = GameManager.Instance.BuildController;

        _renderer = GetComponentInChildren<Renderer>();
        _startColor = _renderer.material.color;
    }

    private void OnMouseDown()
    {
        buildController.SetCanvasEnable(this);
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }

    public void CreateTower(Tower towerPrefab, TowerType type, TowerLevel level)
    {
        _tower = Instantiate(towerPrefab, transform.position, transform.rotation);
        currentTowerType = type;
        currentTowerLevel = level;
    }
}
