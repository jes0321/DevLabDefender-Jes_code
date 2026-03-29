using Code.Animators;
using Code.Entities;
using Code.Entities.FSM;
using UnityEngine;
using Works.JES._01.Scripts.Towers.AttackCompo;

namespace Works.JES._01.Scripts.Towers.State
{
    public class TowerIdleState : TowerState
    {
        private TowerAttackCompo _attackCompo;
        private readonly float ROTATION_SPEED = 20f;
        public TowerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            
            _attackCompo = entity.GetCompo<TowerAttackCompo>(true);
        }

        public override void Update()
        {
            base.Update();
            
            if (_tower.IsInTargetToRange())
            {
                Vector3 direction =  _tower.TargetTrm.position-_tower.transform.position;
                direction.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
                Quaternion rotation = Quaternion.Lerp(_tower.towerTopTrm.rotation, targetRotation, 
                    Time.deltaTime * ROTATION_SPEED);
                _tower.towerTopTrm.rotation = rotation;
                if (_attackCompo.CanAttack())
                {
                    _tower.towerTopTrm.rotation = targetRotation;
                    _tower.ChangeState("ATTACK");
                }
            }
        }
        
    }
}