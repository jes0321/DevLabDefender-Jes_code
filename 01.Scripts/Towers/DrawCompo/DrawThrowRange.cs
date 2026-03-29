using System.Collections.Generic;
using Code.Entities;
using UnityEngine;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Works.JES._01.Scripts.Towers.DrawCompo
{
    public class DrawThrowRange : DrawAttackRange
    {
        [SerializeField] private StatSo attackRadiusStat;
        [SerializeField] private LineRenderer targetLineRenderer; // 타겟 방향과 작은 원을 그리는 LineRenderer
        
        private float _attackRadius;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            
            // 새로운 LineRenderer 추가 및 설정
            targetLineRenderer.useWorldSpace = false;
            targetLineRenderer.loop = false;
            targetLineRenderer.enabled = false;
            targetLineRenderer.startWidth = 0.1f;
            targetLineRenderer.endWidth = 0.1f;
        }

        public override void AfterInit()
        {
            base.AfterInit();
            StatSo radiusStat = _entityStat.GetStat(attackRadiusStat);
            _attackRadius = radiusStat.Value;
            radiusStat.OnValueChange += HandleRadiusChange;
            
            DrawInit();
        }

        protected override void DrawTargetDirection()
        {
            Vector3 direction;
            float distance;

            // 타겟이 있는 경우와 없는 경우 처리
            if (_target != null)
            {
                Vector3 targetPosition = _target.position;
                targetPosition.y = transform.position.y;
                Vector3 targetDirection = targetPosition - transform.position;
                direction = targetDirection.normalized;
                distance = Mathf.Min(targetDirection.magnitude, radius);
            }
            else
            {
                direction = rotateInXZ ? topTrm.forward : topTrm.right;
                distance = radius;
            }

            // 원의 반지름만큼 거리에서 빼서 선이 원과 겹치지 않게 함
            float lineEndDistance = distance - _attackRadius;
            Vector3 circleCenter = new Vector3(direction.x * distance, 0, direction.z * distance);
            Vector3 lineEndPoint = new Vector3(direction.x * lineEndDistance, 0, direction.z * lineEndDistance);
            
            List<Vector3> allPoints = new List<Vector3>();

            // 1. 경로선 (시작점에서 원 바깥까지)
            allPoints.Add(new Vector3(0, 0, 0));
            allPoints.Add(lineEndPoint);

            // 2. 원 그리기 - 경로선 끝점에서 시작
            float startAngle = Mathf.Atan2(lineEndPoint.z - circleCenter.z, lineEndPoint.x - circleCenter.x);
            
            for (int i = 0; i <= segments; i++)
            {
                float t = (float)i / segments;
                float currentAngle = startAngle + t * 2 * Mathf.PI;
                
                float x = Mathf.Cos(currentAngle) * _attackRadius;
                float z = Mathf.Sin(currentAngle) * _attackRadius;
                
                allPoints.Add(circleCenter + new Vector3(x, 0, z));
            }

            targetLineRenderer.positionCount = allPoints.Count;
            targetLineRenderer.SetPositions(allPoints.ToArray());
        }

        private void HandleRadiusChange(StatSo stat, float current, float previous)
        {
            _attackRadius = current;
        }

        public override void EnableCircle(bool enabled)
        {
            base.EnableCircle(enabled);
            targetLineRenderer.enabled = enabled;
        }
    }
}