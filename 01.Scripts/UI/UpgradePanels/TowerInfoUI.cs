using Code.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Works.JES._01.Scripts.Towers;

namespace Works.JES._01.Scripts.UI.UpgradePanels
{
    public class TowerInfoUI : MonoBehaviour
    {
        [SerializeField] private TowerStatBlockUI blockPrefab;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private RectTransform statParentTrm;
        [SerializeField] private Image towerImage;

        public void SetTowerInfo(TowerInfoSO infoSO,EntityStat statCompo)
        {
            foreach (Transform trm in statParentTrm)
            {
                Destroy(trm.gameObject);
            }

            nameText.text = infoSO.towerName;
            // towerImage.sprite = infoSO.towerIcon;

            var stats = statCompo.GetStatSOs();
            foreach (var stat in stats)
            {
                TowerStatBlockUI block = Instantiate(blockPrefab, statParentTrm);
                block.SetText(stat);
            }
        }
    }
}