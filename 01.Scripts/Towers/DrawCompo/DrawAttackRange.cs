using System.Collections.Generic;
using Code.Entities;
using UnityEngine;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Works.JES._01.Scripts.Towers.DrawCompo
{
    public abstract class DrawAttackRange : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] protected Transform topTrm;
        [SerializeField] protected StatSo attackRangeStat;
        
        private bool _isDrawing = false;
        protected Transform _target; // 타겟의 Transform
        public float radius = 1f;
        public int segments = 128;
        public bool rotateInXZ = true;

        private LineRenderer _rangeLineRenderer; // 공격 범위를 그리는 LineRenderer
        private void DrawCircle()
        {
            Vector3[] points = new Vector3[segments];
            for (int i = 0; i < segments; i++)
            {
                float angle = 2 * Mathf.PI * i / segments;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;

                points[i] = rotateInXZ ? new Vector3(x, 0, y) : new Vector3(x, y, 0);
            }

            _rangeLineRenderer.positionCount = segments;
            _rangeLineRenderer.SetPositions(points);
        }
        protected Entity _entity;
        protected EntityStat _entityStat;

        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
        
            // 기존 LineRenderer 설정
            _rangeLineRenderer = GetComponent<LineRenderer>();
            _rangeLineRenderer.useWorldSpace = false;
            _rangeLineRenderer.loop = true;
            _rangeLineRenderer.enabled = false;
        }

        public virtual void AfterInit()
        {
            _entityStat = _entity.GetCompo<EntityStat>();
            
            StatSo rangeStat = _entityStat.GetStat(attackRangeStat);
            radius = rangeStat.Value;
            rangeStat.OnValueChange += HandleAttackRange;
        }

        protected void DrawInit()
        {
            DrawCircle();
            DrawTargetDirection();
        }

        private void HandleAttackRange(StatSo stat, float current, float previous)
        {
            radius = current;
            DrawCircle();
            DrawTargetDirection();
        }

        

        protected abstract void DrawTargetDirection();

        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
            DrawTargetDirection();
        }

        public virtual void EnableCircle(bool enabled)
        {
            _isDrawing = enabled;
            _rangeLineRenderer.enabled = enabled;
        }
    }
}