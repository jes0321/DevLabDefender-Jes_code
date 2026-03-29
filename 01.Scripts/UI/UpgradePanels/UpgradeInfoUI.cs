using Code.Entities;
using TMPro;
using UnityEngine;
using Works.JES._01.Scripts.Towers;

namespace Works.JES._01.Scripts.UI.UpgradePanels
{
    public class UpgradeInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private UpgradeInfoBlockUI blockPrefab;
        [SerializeField] private RectTransform blockParent;

        public void SetText(TowerInfoSO infoSO, EntityStat statCompo)
        {
            foreach (Transform trm in blockParent)
            {
                Destroy(trm.gameObject);
            }

            levelText.text = $"{infoSO.level}레벨";

            if (infoSO.IsMaxLevel) return;
            var stats = infoSO.upgradeSoList[infoSO.level+1].upgradeStats;
            for (int i = 0; i < stats.Length; ++i)
            {
                UpgradeInfoBlockUI block = Instantiate(blockPrefab, blockParent);
                block.SetText(stats[i],statCompo.GetStat(stats[i].targetStat));
            }
        }
    }
}