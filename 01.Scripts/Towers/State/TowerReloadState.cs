using Code.Animators;
using Code.Entities;

namespace Works.JES._01.Scripts.Towers.State
{
    public class TowerReloadState : TowerState
    {
        private EntityAnimationTrigger _animationTrigger;
        public TowerReloadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _animationTrigger = entity.GetCompo<EntityAnimationTrigger>();
        }
        
        public override void Enter()
        {
            base.Enter();
            _animationTrigger.OnAnimationEnd += HandleAnimationEnd;
        }
        private void HandleAnimationEnd()
        {
            AnimationEndTrigger();
        }
        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
                _tower.ChangeState("IDLE");
        }
        public override void Exit()
        {
            _animationTrigger.OnAnimationEnd -= HandleAnimationEnd;
            base.Exit();
        }
    }
}