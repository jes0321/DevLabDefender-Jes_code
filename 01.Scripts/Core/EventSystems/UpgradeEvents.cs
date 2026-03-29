using Code.Entities;
using Works.JES._01.Scripts.Towers;

namespace Works.JES._01.Scripts.Core.EventSystems
{
    public class UpgradeEvents
    {
        public static OnUpgradeUIEvent OnUpgradeUIEvent = new OnUpgradeUIEvent();
        public static OffUpgradeUIEvent OffUpgradeUIEvent = new OffUpgradeUIEvent();
        public static LevelUpEvent LevelUpEvent = new LevelUpEvent();
    }

    public class OnUpgradeUIEvent : GameEvent
    {
        public TowerInfoSO info;
        public EntityStat statCompo;
        public Tower tower;
        public OnUpgradeUIEvent SetTower(Tower entity,TowerInfoSO infoSO,EntityStat statCompo)
        {
            tower = entity;
            info = infoSO;
            this.statCompo = statCompo;
            return this;
        }
    }

    public class OffUpgradeUIEvent : GameEvent
    {
        
    }

    public class LevelUpEvent : GameEvent
    {
    }
}