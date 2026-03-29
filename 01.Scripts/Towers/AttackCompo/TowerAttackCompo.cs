using System;
using Code.Entities;
using UnityEngine;
using Works.JES._01.Scripts.Combats;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Works.JES._01.Scripts.Towers.AttackCompo
{
    public class TowerAttackCompo : MonoBehaviour,IEntityComponent,IAfterInit
    {
        [SerializeField] private StatSo asStat;
        [SerializeField] protected InGameSettingSO gameSettingSO;
        private float _attackSpeed;
        private float _lastAtkTime=0;
        
        protected AttackTower _tower;
        protected EntityStat _entityStat;
        public virtual void Initialize(Entity entity)
        {
            _tower = entity as AttackTower;
        }
        public virtual void AfterInit()
        {
            _entityStat = _tower.GetCompo<EntityStat>();
            
            StatSo stat =_entityStat.GetStat(asStat);
            _attackSpeed = stat.Value;
            stat.OnValueChange += HandleSpeedChange;
        }

        protected virtual void OnDestroy()
        {
            StatSo stat =_entityStat.GetStat(asStat);
            stat.OnValueChange -= HandleSpeedChange;
        }

        public bool CanAttack()
        {
            return _attackSpeed+_lastAtkTime < Time.time;
        }

        protected void RotationTower()
        {
            Vector3 direction =  _tower.TargetTrm.position-_tower.transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            _tower.towerTopTrm.rotation = targetRotation;
        }
        
        public virtual void Attack()
        {
            _lastAtkTime = Time.time;
        }

        private void HandleSpeedChange(StatSo stat, float current, float previous)
        {
            _attackSpeed = current;
        }
    }
}