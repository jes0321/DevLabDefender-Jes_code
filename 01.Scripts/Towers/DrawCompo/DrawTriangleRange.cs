using System.Collections.Generic;
using Code.Entities;
using UnityEngine;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Works.JES._01.Scripts.Towers.DrawCompo
{
    public class DrawTriangleRange : DrawAttackRange
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private StatSo angleStat;
        private float _attackAngle;
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            
            lineRenderer.useWorldSpace = false;
            lineRenderer.loop = false;
            lineRenderer.enabled = false;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
        
        public override void AfterInit()
        {
            base.AfterInit();
            StatSo radiusStat = _entityStat.GetStat(angleStat);
            _attackAngle = radiusStat.Value;
            radiusStat.OnValueChange += HandleAngleChange;
            
            DrawInit();
        }

        private void HandleAngleChange(StatSo stat, float current, float previous)
        {
            _attackAngle = current;
        }
        protected override void DrawTargetDirection()
        {
            Vector3 direction;

            // 타겟이 있는 경우와 없는 경우 처리
            if (_target != null)
            {
                Vector3 targetPosition = _target.position;
                targetPosition.y = transform.position.y;
                Vector3 targetDirection = targetPosition - transform.position;
                direction = targetDirection.normalized;
            }
            else
            {
                direction = rotateInXZ ? topTrm.forward : topTrm.right;
            }

            List<Vector3> allPoints = new List<Vector3>();
            allPoints.Add(Vector3.zero); // 시작점

            // 부채꼴의 각도 계산
            float baseAngle = Mathf.Atan2(direction.z, direction.x);
            float halfAngle = (_attackAngle * 0.5f) * Mathf.Deg2Rad; // 각도의 절반

            // 부채꼴 그리기
            int segments = 30;
            for (int i = 0; i <= segments; i++)
            {
                float t = (float)i / segments;
                float currentAngle = baseAngle - halfAngle + (_attackAngle * Mathf.Deg2Rad * t);
        
                float x = Mathf.Cos(currentAngle) * radius;
                float z = Mathf.Sin(currentAngle) * radius;
        
                allPoints.Add(new Vector3(x, 0, z));
            }

            // 시작점으로 돌아와서 닫기
            allPoints.Add(Vector3.zero);

            lineRenderer.positionCount = allPoints.Count;
            lineRenderer.SetPositions(allPoints.ToArray());
        }
        public override void EnableCircle(bool enabled)
        {
            base.EnableCircle(enabled);
            lineRenderer.enabled = enabled;
        }
    }
}