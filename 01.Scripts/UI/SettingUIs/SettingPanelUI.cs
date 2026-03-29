using System;
using DevlabDefence;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Works.JES._01.Scripts.Core.EventSystems;
using Works.JES._01.Scripts.Towers;

namespace Works.JES._01.Scripts.UI.SettingUIs
{
    public class SettingPanelUI : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private InGameSettingSO gameSettingSO;
        [SerializeField] private float fadeTime = 0.2f;
        [SerializeField] private GameEventChannelSO upgradeChannel;
        [SerializeField] private GameEventChannelSO uiChannel;

        [Header("UI")] [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Toggle toggle;

        private bool isOn = false;
        private bool canEsc = true;
        [SerializeField] private bool isTitle = false;

        private void Awake()
        {
            playerInputs.OnCancelEvent += HandleCancel;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            uiChannel.AddListener<BlockEscEvent>(HandleBlockEsc);
        }

        private void HandleBlockEsc(BlockEscEvent obj)
        {
            canEsc = obj.isOn;
        }

        private void OnDestroy()
        {
            uiChannel.RemoveListener<BlockEscEvent>(HandleBlockEsc);
            playerInputs.OnCancelEvent -= HandleCancel;
        }

        private void HandleCancel()
        {
            if (canEsc == false) return;
            if (isOn)
            {
                CloseSettingPanel();
            }
            else
            {
                OpenSettingPanel();
            }
        }

        public void LobbyScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("TitleScene");
        }

        public void OpenSettingPanel()
        {
            if (isTitle == false)
                Time.timeScale = 0;
            uiChannel.RaiseEvent(UIEvents.OnOffEscEvent.SetEvent(true));
            isOn = true;
            upgradeChannel.RaiseEvent(UpgradeEvents.OffUpgradeUIEvent);
            canvasGroup.DOFade(1, fadeTime).SetUpdate(true);
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;

            toggle.isOn = gameSettingSO.onEffect;
        }

        public void CloseSettingPanel()
        {
            if (isTitle == false)
                Time.timeScale = 1;
            uiChannel.RaiseEvent(UIEvents.OnOffEscEvent.SetEvent(false));
            isOn = false;
            canvasGroup.DOFade(0, fadeTime).SetUpdate(true);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        public void OnOffEffect(bool isOn)
        {
            gameSettingSO.onEffect = isOn;
        }
    }
}