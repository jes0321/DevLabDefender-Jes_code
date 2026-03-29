using System;
using DevlabDefence;
using DevlabDefender.Chipmunk.Camera;
using UnityEngine;
using UnityEngine.EventSystems;
using Works.JES._01.Scripts.Core.EventSystems;

namespace Works.JES._01.Scripts.Towers
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputs playerInput;
        [SerializeField] private GameEventChannelSO upgradeChannel;
        [SerializeField] private LayerMask whatIsTower;
        private Tower _selectTower;

        private void Awake()
        {
            playerInput.onClick += HandleClick;
            upgradeChannel.AddListener<OffUpgradeUIEvent>(HandleOffUI);
        }

        private void OnDestroy()
        {
            playerInput.onClick -= HandleClick;
            upgradeChannel.RemoveListener<OffUpgradeUIEvent>(HandleOffUI);
        }

        private void HandleClick(bool isClick)
        {
            if(isClick==false) return;
            
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, Camera.main.farClipPlane, whatIsTower))
            {
                Tower tower = hit.collider.GetComponent<Tower>();
                _selectTower?.UnSelectTower();
                _selectTower = tower;
                tower.SelectTower();
            }   
            else if (UIPointerDetector.IsPointerInUI==false)
            {
                upgradeChannel.RaiseEvent(UpgradeEvents.OffUpgradeUIEvent);
            }
        }

        private void HandleOffUI(OffUpgradeUIEvent obj)
        {
            _selectTower?.UnSelectTower();
            _selectTower = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                upgradeChannel.RaiseEvent(UpgradeEvents.OffUpgradeUIEvent);
            }
        }
    }
}