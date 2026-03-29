using System;
using UnityEngine;

namespace Works.JES._01.Scripts.Sounds
{
    public class BGMPlayer : MonoSingleton<BGMPlayer>
    {
        [SerializeField] private bool isOn = true;
        [SerializeField] private SoundSO soundSo;

        private SoundPlayer _bgmPlayer;
        private void Start()
        {
            if (isOn == false) return;
            _bgmPlayer = SoundManager.Instance.PlaySound(soundSo,Vector3.zero);
        }

        public void StopBGM()
        {
            _bgmPlayer?.StopSound();
        }

        public void PlayBGM()
        {
            _bgmPlayer?.ReplaySound();
        }
    }
}