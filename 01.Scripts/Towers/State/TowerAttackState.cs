using Code.Animators;
using Code.Entities;

namespace Works.JES._01.Scripts.Towers.State
{
    public class TowerAttackState : TowerState
    {
        private EntityAnimationTrigger _animationTrigger;
        public TowerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
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
                _tower.ChangeState("RELOAD");
        }

        public override void Exit()
        {
            _animationTrigger.OnAnimationEnd -= HandleAnimationEnd;
            base.Exit();
        }
    }
}