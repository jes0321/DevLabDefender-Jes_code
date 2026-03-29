using System.Collections.Generic;
using Code.Combats;
using DG.Tweening;
using UnityEngine;
using Works.EJY._01.Scripts.Enemies;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Works.JES._01.Scripts.Towers.AttackCompo
{
    public class AngleAttackCompo : TowerAttackCompo
    {
        [SerializeField] private StatSo angleStat;
        [SerializeField] private StatSo rangeStat;
        [SerializeField] private StatSo damageStat;
        [SerializeField] private ParticleSystem attackEffect;
        
        private float _angle;
        private float _radius;
        private float _damage;

        private AttackTower _attackTower;
        public override void AfterInit()
        {
            base.AfterInit();

            StatSo stat = _entityStat.GetStat(angleStat);
            _angle = stat.Value;
            stat.OnValueChange += HandleAngleChange;
            
            stat = _entityStat.GetStat(rangeStat);
            _radius = stat.Value;
            stat.OnValueChange += HandleRangeChange;
            
            stat = _entityStat.GetStat(damageStat);
            _damage = stat.Value;
            stat.OnValueChange += HandleDamageChange;

            _attackTower = _tower as AttackTower;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            StatSo stat = _entityStat.GetStat(angleStat);
            stat.OnValueChange -= HandleAngleChange;
            
            stat = _entityStat.GetStat(rangeStat);
            stat.OnValueChange -= HandleRangeChange;
            
            stat = _entityStat.GetStat(damageStat);
            stat.OnValueChange -= HandleDamageChange;
        }
        public override void Attack()
        {
            if(gameSettingSO.onEffect)
                PlayEffect();
            
            List<Enemy> targets = _attackTower.enemies;
            Transform targetTrm = _attackTower.TargetTrm;

            // 부채꼴 안에 있는 적들을 저장할 새로운 리스트
            List<Enemy> enemiesInSector = new List<Enemy>();

            // 모든 적들을 순회하면서 체크
            foreach (Enemy enemy in targets)
            {
                // 타겟으로부터 적까지의 방향 벡터
                Vector3 directionToEnemy = enemy.transform.position - targetTrm.position;

                // 타겟으로부터 적까지의 거리
                float distanceToEnemy = directionToEnemy.magnitude;

                // 반경 내에 있는지 체크
                if (distanceToEnemy <= _radius)
                {
                    // targetTrm의 정면 방향과 적 방향 사이의 각도 계산
                    float angleToEnemy = Vector3.Angle(targetTrm.forward, directionToEnemy);

                    // 계산된 각도가 설정된 각도의 절반보다 작으면 부채꼴 안에 있음
                    if (angleToEnemy <= _angle / 2f)
                    {
                        enemiesInSector.Add(enemy);
                    }
                }
            }

            foreach (var enemy in enemiesInSector)
            {
                if (enemy.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(_damage);
                }
            }

            base.Attack();
        }

        private void PlayEffect()
        {
            attackEffect.Play();
            DOVirtual.DelayedCall(0.2f, () =>
            {
                attackEffect.Stop();
            });
        }

        private void HandleRangeChange(StatSo stat, float current, float previous)
        {
            _radius = current;
        }
        private void HandleDamageChange(StatSo stat, float current, float previous)
        {
            _damage = current;
        }
        private void HandleAngleChange(StatSo stat, float current, float previous)
        {
            _angle = current;
        }
    }
}