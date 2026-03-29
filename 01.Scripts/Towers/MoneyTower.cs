using Code.Entities;
using UnityEngine;
using Works.EJY._01.Scripts.Core;
using Works.JES._01.Scripts.Core.EventSystems;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Works.JES._01.Scripts.Towers
{
    public class MoneyTower : Tower
    {
        [SerializeField] private InGameSettingSO inGameSetting;
        [SerializeField] private StatSo moneyStat;
        [SerializeField] private GameEventChannelSO attackChannel;
        private float getMoney;
        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            EntityStat entityStat = GetCompo<EntityStat>();
            StatSo stat = entityStat.GetStat(moneyStat);
            stat.OnValueChange += HandleChangeMoney;
            getMoney = stat.Value;
            
            attackChannel.AddListener<WaveChangeEvent>(HandleWaveChange);
        }

        private void HandleWaveChange(WaveChangeEvent obj)
        {
            int money = inGameSetting.Money;
            inGameSetting.Money = money+(int)getMoney;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            EntityStat entityStat = GetCompo<EntityStat>();
            StatSo stat = entityStat.GetStat(moneyStat);
            stat.OnValueChange -= HandleChangeMoney;
            getMoney = stat.Value;
            
            attackChannel.RemoveListener<WaveChangeEvent>(HandleWaveChange);
        }
        private void HandleChangeMoney(StatSo stat, float current, float previous)
        {
            getMoney = current;
        }
    }
}