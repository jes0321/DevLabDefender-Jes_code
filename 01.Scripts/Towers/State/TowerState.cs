using Code.Animators;
using Code.Entities;
using Code.Entities.FSM;

namespace Works.JES._01.Scripts.Towers.State
{
    public class TowerState : EntityState
    {
        protected AttackTower _tower;
        public TowerState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _tower = entity as AttackTower;
        }
    }
}