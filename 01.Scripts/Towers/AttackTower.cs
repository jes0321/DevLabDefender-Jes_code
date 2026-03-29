using System.Collections.Generic;
using Code.Entities;
using UnityEngine;
using Works.EJY._01.Scripts.Enemies;
using Works.JES._01.Scripts.Core.StatSystem;
using Works.JES._01.Scripts.Towers.AttackCompo;
using Works.JES._01.Scripts.Towers.DrawCompo;

namespace Works.JES._01.Scripts.Towers
{
    public class AttackTower : Tower
    {
        [SerializeField] private LayerMask whatIsTarget;
        [SerializeField] private StatSo rangeStat;

        private float _attackRange;
        private Collider[] _hitColliders;
        public List<Enemy> enemies = new List<Enemy>();
        private int _hitCount;

        private DrawAttackRange _drawCompo;
        public Transform TargetTrm { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            _towerAttackCompo = GetCompo<TowerAttackCompo>(true);
            
            StatSo stat = GetCompo<EntityStat>().GetStat(rangeStat);
            stat.OnValueChange += HandleRangeStatChange;
            _attackRange = stat.Value;

            _hitColliders = new Collider[100];

            _drawCompo = GetCompo<DrawAttackRange>(true);

            GetCompo<EntityAnimationTrigger>().OnAttackTrigger += HandleAttack;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GetCompo<EntityAnimationTrigger>().OnAttackTrigger -= HandleAttack;
            StatSo stat = GetCompo<EntityStat>().GetStat(rangeStat);
            stat.OnValueChange -= HandleRangeStatChange;
        }

        private void HandleAttack()
        {
            _towerAttackCompo.Attack();
        }

        private void HandleRangeStatChange(StatSo stat, float current, float previous)
        {
            _attackRange = current;
        }

        #region SetTarget_Session

        public void SetTarget()
        {
            if (enemies.Count <= 0)
            {
                TargetTrm = null;
            }
            else
            {
                switch (targetingType)
                {
                    case TargetingEnum.FIRST:
                        SetFirstTarget();
                        break;
                    case TargetingEnum.LAST:
                        SetLastTarget();
                        break;
                    case TargetingEnum.STRONG:
                        SetStrongTarget();
                        break;
                }
            }

            _drawCompo.SetTarget(TargetTrm);
        }

        private void SetStrongTarget()
        {
            Enemy strongEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (enemy == null) continue;
                if (strongEnemy == null || strongEnemy.GetCompo<EntityHealth>().CurrentHealth < enemy.GetCompo<EntityHealth>().CurrentHealth)
                {
                    strongEnemy = enemy;
                }
            }

            TargetTrm = strongEnemy?.transform;
        }

        private void SetLastTarget()
        {
            Enemy lastEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (enemy == null) continue;
                if (lastEnemy == null || lastEnemy.RemainDistance < enemy.RemainDistance)
                {
                    lastEnemy = enemy;
                }
            }

            TargetTrm = lastEnemy?.transform;
        }

        private void SetFirstTarget()
        {
            Enemy firstEnemy = null;
            foreach (Enemy enemy in enemies)
            {
                if (enemy == null) continue;
                if (firstEnemy == null || firstEnemy.RemainDistance > enemy.RemainDistance)
                {
                    firstEnemy = enemy;
                }
            }
            
            TargetTrm = firstEnemy?.transform;
        }

        #endregion

        public bool IsInTargetToRange()
        {
            _hitCount = Physics.OverlapSphereNonAlloc(transform.position, _attackRange, _hitColliders, whatIsTarget);
            enemies.Clear();
            for (int i = 0; i < _hitCount; i++)
            {
                if (_hitColliders[i].TryGetComponent(out Enemy enemy))
                    enemies.Add(enemy);
            }

            SetTarget();
            return enemies.Count > 0;
        }

        public override void SelectTower()
        {
            base.SelectTower();
            _drawCompo.EnableCircle(true);
        }

        public override void UnSelectTower()
        {
            base.UnSelectTower();
            _drawCompo.EnableCircle(false);
        }
    }
}