using System;
using Devlab.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;
using Works.JES._01.Scripts.Core.EventSystems;

namespace Works.JES._01.Scripts.Sounds
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [Inject] private PoolManagerMono _poolManager;
        [SerializeField] private PoolingItemSO playerSo;

        public SoundPlayer PlaySound(SoundSO sound,Vector3 pos)
        {
            SoundPlayer player = _poolManager.Pop<SoundPlayer>(playerSo);

            player.transform.position = pos;
            player.PlaySound(sound);

            return player;
        }
    }
}