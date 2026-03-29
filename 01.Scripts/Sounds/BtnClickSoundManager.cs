using System;
using UnityEngine;
using UnityEngine.UI;

namespace Works.JES._01.Scripts.Sounds
{
    public class BtnClickSoundManager : MonoBehaviour
    {
        [SerializeField] private SoundSO sound;

        private void Awake()
        {
            var btns = FindObjectsByType<BtnClickSound>(FindObjectsSortMode.None);

            foreach (var btn in btns)
            {
                btn.Init();
                
                btn.button.onClick.AddListener(() => SoundManager.Instance.PlaySound(sound,btn.transform.position));
            }
        }
    }
}