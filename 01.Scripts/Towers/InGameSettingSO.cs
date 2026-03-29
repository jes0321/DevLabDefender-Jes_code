using System;
using UnityEngine;
using Works.EJY._01.Scripts.Core;
using Works.JES._01.Scripts.Core.EventSystems;
using Random = UnityEngine.Random;

namespace Works.JES._01.Scripts.Towers
{
    [CreateAssetMenu(fileName = "InGameMoneySO", menuName = "SO/System/Money")]
    public class InGameSettingSO : ScriptableObject
    {
        [SerializeField] private int m_money;
        [SerializeField] private GameEventChannelSO waveChannel;

        public bool onEffect;
        public Action OnMoneyChange;

        private void OnEnable()
        {
            waveChannel.AddListener<WaveChangeEvent>(HandleWaveChange);
            waveChannel.AddListener<EnemyDeadEvent>(HandleDeadChange);
        }

        private void OnDisable()
        {
            waveChannel.RemoveListener<WaveChangeEvent>(HandleWaveChange);
            waveChannel.RemoveListener<EnemyDeadEvent>(HandleDeadChange);
        }

        private void HandleDeadChange(EnemyDeadEvent obj)
        {
            Money += Random.Range(30, 41);
        }

        private void HandleWaveChange(WaveChangeEvent obj)
        {
            Money += Random.Range(200, 250);
        }

        public int Money
        {
            get => m_money;
            set
            {
                m_money = value;
                OnMoneyChange?.Invoke();
            }
        }
    }
}
