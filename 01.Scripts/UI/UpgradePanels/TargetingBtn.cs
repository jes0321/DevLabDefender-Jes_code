using System;
using Chipmunk.Library.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Works.JES._01.Scripts.Towers;

namespace Works.JES._01.Scripts.UI.UpgradePanels
{
    public class TargetingBtn : MonoBehaviour
    {
        private Tower _targetTower;
        [SerializeField] private TargetingEnum targetingEnum;
        [SerializeField] private TextMeshProUGUI targetingText;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(BtnClick);
            targetingText.text = targetingEnum.ToString();
        }
        private void BtnClick()
        {
            targetingEnum = targetingEnum.GetNext();
            targetingText.text = targetingEnum.ToString();
            _targetTower.targetingType = targetingEnum;
        }

        public void SetTower(Tower evtTower)
        {
            _targetTower = evtTower;
            targetingEnum =  evtTower.targetingType;
            targetingText.text = targetingEnum.ToString();
        }
    }
}