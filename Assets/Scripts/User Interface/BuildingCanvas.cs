using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCanvas : MonoBehaviour
{
    BuildController BuildController;
    GameData gameData;

    [SerializeField] private TMP_Text upgradeText;

    [Header("Button")]
    [SerializeField] private ButtonInfo upgradeButton;
    [SerializeField] private ButtonInfo destroyButton;
    [SerializeField] private ButtonInfo buildCannonTowerButton;
    [SerializeField] private ButtonInfo buildArcherTowerButton;

    [System.Serializable]
    public class ButtonInfo
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text costText;

        public void SetActive(bool value)
        {
            button.gameObject.SetActive(value);
        }

        public void AddListener(Action callback, float cost = -1)
        {
            SetActive(true);

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                callback?.Invoke();
            });

            if (cost <= -1)
            {
                costText.gameObject.SetActive(false);
            }
            else
            {
                costText.gameObject.SetActive(true);
                costText.SetText(cost.ToString());
            }
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;

            costText.gameObject.SetActive(value);
        }
    }

    private void OnEnable()
    {
        gameData ??= GameManager.Instance.GameData;
        BuildController ??= GameManager.Instance.BuildController;
    }

    public void SetValues(BuildingPoint buildingPoint)
    {
        upgradeButton.SetActive(false);
        destroyButton.SetActive(false);
        buildCannonTowerButton.SetActive(false);
        buildArcherTowerButton.SetActive(false);

        if (buildingPoint.currentTowerType == TowerType.Empty)
        {
            buildCannonTowerButton.AddListener(() =>
            {
                Shop.PurcheseTower(TowerType.Cannon, TowerLevel.Lv1);
            }, gameData.GetTowerPrice(TowerType.Cannon, TowerLevel.Lv1));

            buildArcherTowerButton.AddListener(() =>
            {
                Shop.PurcheseTower(TowerType.Archer, TowerLevel.Lv1);
            }, gameData.GetTowerPrice(TowerType.Cannon, TowerLevel.Lv1));
        }
        else
        {
            if (buildingPoint.currentTowerLevel == TowerLevel.Lv5)
            {
                upgradeButton.SetInteractable(false);
                upgradeText.SetText("Max Level");
            }
            else
            {
                upgradeButton.SetInteractable(true);
                var nextLevel = buildingPoint.currentTowerLevel + 1;
                upgradeText.SetText($"Upgrade to {nextLevel}");
                upgradeButton.AddListener(() =>
                {
                    Shop.PurcheseTower(buildingPoint.currentTowerType, nextLevel);
                }, gameData.GetTowerPrice(buildingPoint.currentTowerType, nextLevel));
            }

            destroyButton.AddListener(() =>
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
