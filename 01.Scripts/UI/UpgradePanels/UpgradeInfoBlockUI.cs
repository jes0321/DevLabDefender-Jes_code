using Code.Entities;
using TMPro;
using UnityEngine;
using Works.JES._01.Scripts.Core.StatSystem;
using Works.JES._01.Scripts.Towers;

namespace Works.JES._01.Scripts.UI.UpgradePanels
{
    public class UpgradeInfoBlockUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI currentValue;
        [SerializeField] private TextMeshProUGUI nextValue;


        public void SetText(StatUpgrade upgrade,StatSo stat)
        {
            nameText.text = upgrade.targetStat.statName;
            currentValue.text = $"{stat.Value}";
            nextValue.text = $"{upgrade.value}";
        }
    }
}