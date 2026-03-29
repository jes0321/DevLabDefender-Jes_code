using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Works.JES._01.Scripts.UI.UpgradePanels
{
    public class TowerStatBlockUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI valueText;

        public void SetText(StatSo stat)
        {
            nameText.text = stat.statName;
            valueText.text = $"{stat.Value}";
        }
    }
}