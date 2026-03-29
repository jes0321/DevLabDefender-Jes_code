using DevlabDefender.Chipmunk.Building;
using TMPro;
using UnityEngine;
using Works.JES._01.Scripts.Core.EventSystems;
using Works.JES._01.Scripts.Towers;

namespace Works.JES._01.Scripts.UI.UpgradePanels
{
    public class SellButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private InGameSettingSO gameSettingSo;
        [SerializeField] private BuildingContainer buildingContainer;
        [SerializeField] private GameEventChannelSO upgradeUIEventChannel;

        private TowerInfoSO _towerInfoSO;
        private int _sellPrice;
        private Tower _tower;
        public void Initialize(Tower tower,TowerInfoSO infoSO)
        {
            _sellPrice = infoSO.GetCurrentSellPrice();
            priceText.text = _sellPrice.ToString();
            _towerInfoSO = infoSO;
            _tower = tower;
        }

        public void SellBtnClick()
        {
            gameSettingSo.Money += _sellPrice;
            if (buildingContainer.RemoveTower(_tower.gameObject))
            {
                upgradeUIEventChannel.RaiseEvent(UpgradeEvents.OffUpgradeUIEvent);
                Destroy(_tower.gameObject);
                _towerInfoSO = null;
            }
            else
            {
                Debug.LogError("Failed to remove building from container.");
            }
        }
    }
}