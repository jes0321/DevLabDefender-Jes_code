using System;
using UnityEngine;
using Works.JES._01.Scripts.Core.EventSystems;

namespace Works.JES._01.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GameEventChannelSO uiChannel;

        private void Awake()
        {
            uiChannel.AddListener<OnOffEscEvent>(HandleOnEsc);
        }

        private void OnDestroy()
        {
            uiChannel.RemoveListener<OnOffEscEvent>(HandleOnEsc);
        }

        private void HandleOnEsc(OnOffEscEvent obj)
        {
            canvasGroup.alpha = obj.isOn ? 0 : 1;
            canvasGroup.interactable = !obj.isOn;
            canvasGroup.blocksRaycasts = !obj.isOn;
        }
    }
}