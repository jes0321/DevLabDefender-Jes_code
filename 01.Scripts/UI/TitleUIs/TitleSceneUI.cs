using System;
using DevlabDefence;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Works.JES._01.Scripts.UI.TitleUIs
{
    public class TitleSceneUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private PlayerInputs playerInput;

        private bool isOn;
        private void Awake()
        {
            OnOffTitle(true);
            playerInput.OnCancelEvent += HandleCancel;
        }

        private void OnDestroy()
        {
            playerInput.OnCancelEvent -= HandleCancel;
        }

        private void HandleCancel()
        {
            OnOffTitle(!isOn);
        }

        public void GoLobbyBtn()
        {
            SceneManager.LoadScene("LobbyScene");
        }
        public void EndBtnClick()
        {
            Application.Quit();
        }

        public void OnOffTitle(bool value)
        {
            isOn = value;
            canvasGroup.alpha = value?1:0;
            canvasGroup.blocksRaycasts = value;
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
        }
    }
}