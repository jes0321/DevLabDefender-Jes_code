using System;
using TMPro;
using UnityEngine;
using Works.JES._01.Scripts.Core.EventSystems;
using Works.JES._01.Scripts.Towers;
using Works.JES._01.Scripts.UI.UpgradePanels;

namespace Works.JES._01.Scripts.UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO upgradeChannel;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("UIs")] [SerializeField] private TowerInfoUI infoUI;
        [SerializeField] private UpgradeInfoUI upgradeInfoUI;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TargetingBtn targetingBtn;
        [SerializeField] private SellButtonUI sellButtonUI;

        [Header("Camera")] [SerializeField] private Camera towerPreviewCamera;
        [SerializeField] private Vector3 towerPreviewCameraOffset;

        private int _currentCost;

        [SerializeField] private InGameSettingSO moneySO;
        private int Money => moneySO.Money;

        private bool isMaxLevel = false;

        private void Start()
        {
            upgradeChannel.AddListener<OnUpgradeUIEvent>(HandleOnUI);
            upgradeChannel.AddListener<OffUpgradeUIEvent>(HandleOffUI);
            OnOffPanel(false);
            moneySO.Money = 600;
        }

        private void OnDestroy()
        {
            upgradeChannel.RemoveListener<OnUpgradeUIEvent>(HandleOnUI);
            upgradeChannel.RemoveListener<OffUpgradeUIEvent>(HandleOffUI);
        }

        private void HandleOffUI(OffUpgradeUIEvent obj)
        {
            OnOffPanel(false);
        }

        private void HandleOnUI(OnUpgradeUIEvent evt)
        {
            TowerInfoSO info = evt.info;

            Tower tower = evt.tower;

            if (info.GetUpgradeCost(out _currentCost) == true)
            {
                isMaxLevel = false;
                priceText.text = $"{_currentCost}";
            }
            else
            {
                isMaxLevel = true;
                priceText.text = "MAX";
            }

            targetingBtn.SetTower(tower);
            sellButtonUI.Initialize(tower, evt.info);

            infoUI.SetTowerInfo(info, evt.statCompo);
            upgradeInfoUI.SetText(info, evt.statCompo);

            towerPreviewCamera.transform.position = tower.transform.position + towerPreviewCameraOffset;
            towerPreviewCamera.gameObject.transform.LookAt(tower.transform);

            OnOffPanel(true);
        }

        public void LevelUpBtnClick()
        {
            if (Money < _currentCost || isMaxLevel) return;
            moneySO.Money -= _currentCost;
            upgradeChannel.RaiseEvent(UpgradeEvents.LevelUpEvent);

        }

        public void CloseBtnClick()
        {
            OnOffPanel(false);
            upgradeChannel.RaiseEvent(UpgradeEvents.OffUpgradeUIEvent);
        }

        public void OnOffPanel(bool isOn)
        {
            towerPreviewCamera.gameObject.SetActive(isOn);
            canvasGroup.alpha = isOn ? 1 : 0;
            canvasGroup.interactable = isOn;
            canvasGroup.blocksRaycasts = isOn;
        }
    }
}