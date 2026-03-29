using System;
using DevlabDefence;
using EPOOutline;
using UnityEngine;

namespace Works.JES._01.Scripts.UI
{
    public class SelectMenuManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputs inputs;
        [SerializeField] private LayerMask whatIsMenu;
        [SerializeField] private SelectUI selectUI;
        [SerializeField] private RankingUI rankingUI;
        [SerializeField] private PlayerNameManager playerNameManager;

        private void Awake()
        {
            inputs.onClick += HandleClick;
            selectUI.CloseUI();
        }

        Outlinable selectedOutlinable = null;

        private void FixedUpdate()
        {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            if (Physics.Raycast(ray, out RaycastHit hit, Camera.main.farClipPlane, whatIsMenu))
            {
                if (hit.collider.TryGetComponent(out Outlinable outlinable))
                {
                    if (selectedOutlinable != null)
                        selectedOutlinable.enabled = false;
                    selectedOutlinable = outlinable;
                    outlinable.enabled = true;
                }
            }
            else
            {
                if (selectedOutlinable != null)
                    selectedOutlinable.enabled = false;
                selectedOutlinable = null;
            }
        }

        private void OnDestroy()
        {
            inputs.onClick -= HandleClick;
        }

        private void HandleClick(bool obj)
        {
            if (obj == false) return;
            if (selectUI.IsOpen) return;
            if (playerNameManager.IsOpen) return;
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, Camera.main.farClipPlane, whatIsMenu))
            {
                if (hit.collider.TryGetComponent(out SelectStageObject selectStageObject))
                {
                    selectStageObject.CameraPriority = 2;

                    if (selectStageObject.menuType == MenuType.Rank)
                    {
                        rankingUI.ShowRanking();
                        return;
                    }

                    selectUI.OpenUI();
                    selectUI.SetDesc(selectStageObject.Desc());
                    selectUI.SetYesBtn($"{selectStageObject.menuType}Scene");
                    selectUI.SetNoBtn(selectStageObject);
                }
            }
        }
    }
}