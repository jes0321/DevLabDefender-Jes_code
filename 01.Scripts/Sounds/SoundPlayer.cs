using System;
using DG.Tweening;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;
using UnityEngine.Audio;

namespace Works.JES._01.Scripts.Sounds
{
    public class SoundPlayer : MonoBehaviour, IPoolable
    {
        [SerializeField] private AudioMixerGroup bgm,sfx;
        [SerializeField] private AudioSource audioSource;

        private bool isPlay = false;
        public void PlaySound(SoundSO sound)
        {
            audioSource.outputAudioMixerGroup = sound.outputType == OutputType.BGM ? bgm : sfx;

            audioSource.resource = sound.audioResource;
            
            audioSource.pitch = 1 + sound.GetRandomPitch();
            audioSource.loop = sound.isLoop;
            audioSource.volume = sound.volume;

            audioSource.dopplerLevel = sound.is3D ? 1f : 0f;

            if (sound.isTime)
            {
                DOVirtual.DelayedCall(sound.playTime, () =>
                {
                    DestroySound();
                });
            }

            ReplaySound();
        }

        private void Update()
        {
            if(isPlay&&audioSource.isPlaying==false)
                DestroySound();
        }

        public void StopSound()
        {
            isPlay = false;
            audioSource.Stop();
        }

        public void ReplaySound()
        {
            isPlay = true;
            audioSource.Play();
        }

        private void DestroySound()
        {
            StopSound();
            _myPool.Push(this);
        }
        [field : SerializeField]public PoolingItemSO PoolingType { get; private set; }
        public GameObject GameObject => gameObject;
        private Pool _myPool;
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }
        public void ResetItem()
        {
        }
    }
}