using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCanvas : MonoBehaviour
{
    BuildController BuildController;

    [Header("Button")]
    [SerializeField] private Button upgradeButton;
    TMP_Text upgradeText;
    [SerializeField] private Button destroyButton;
    [SerializeField] private Button buildCannonTowerButton;
    [SerializeField] private Button buildArcherTowerButton;

    private void Start()
    {
        BuildController = GameManager.Instance.BuildController;
        upgradeText = upgradeButton.GetComponentInChildren<TMP_Text>();
    }

    public void SetValues(BuildingPoint buildingPoint)
    {
        upgradeButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);
        buildCannonTowerButton.gameObject.SetActive(false);
        buildArcherTowerButton.gameObject.SetActive(false);

        if (buildingPoint.currentTowerType == TowerType.Empty)
        {
            buildCannonTowerButton.gameObject.SetActive(true);
            buildArcherTowerButton.gameObject.SetActive(true);

            buildCannonTowerButton.onClick.RemoveAllListeners();
            buildCannonTowerButton.onClick.AddListener(() =>
            {
                BuildController.SetTowerToBuild(TowerType.Cannon, TowerLevel.Lv1);
            });

            buildArcherTowerButton.onClick.RemoveAllListeners();
            buildArcherTowerButton.onClick.AddListener(() =>
            {
                BuildController.SetTowerToBuild(TowerType.Archer, TowerLevel.Lv1);
            });
        }
        else
        {
            upgradeButton.gameObject.SetActive(true);
            destroyButton.gameObject.SetActive(true);

            upgradeButton.onClick.RemoveAllListeners();
            if (buildingPoint.currentTowerLevel == TowerLevel.Lv4A || buildingPoint.currentTowerLevel == TowerLevel.Lv4B)
            {
                upgradeButton.interactable = false;
                upgradeText.SetText("Max Level");
            }
            else
            {
                upgradeButton.interactable = true;
                var nextLevel = buildingPoint.currentTowerLevel + 1;
                upgradeText.SetText($"Upgrade to {nextLevel}");
                upgradeButton.onClick.AddListener(() =>
                {
                    BuildController.SetTowerToBuild(buildingPoint.currentTowerType, nextLevel);
                });
            }
            destroyButton.onClick.RemoveAllListeners();
            destroyButton.onClick.AddListener(() =>
            {
                BuildController.ClearBuilding();

            });
        }
    }


    //[SerializeField] private BuildingSlot buttonPrefab;
    //[SerializeField] private Transform content;

    //public void SetData(params Info[] infos)
    //{
    //    content.DestroyChildren();

    //    foreach (var info in infos)
    //    {
    //        var buttonIns = Instantiate(buttonPrefab, content);
    //        buttonIns.SetTowerData(info.type, info.level);
    //    }
    //}

    //public struct Info
    //{
    //    public TowerType type { get;private set; }
    //    public TowerLevel level { get; private set; }

    //    public Info(TowerType type, TowerLevel level)
    //    {
    //        this.type = type;
    //        this.level = level;
    //    }
    //}
}
