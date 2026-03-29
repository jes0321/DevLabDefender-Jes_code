using System.Collections.Generic;
using Code.Entities;
using Code.Entities.FSM;
using UnityEngine;
using Works.EJY._01.Scripts.Enemies;
using Works.JES._01.Scripts.Core.EventSystems;
using Works.JES._01.Scripts.Core.StatSystem;
using Works.JES._01.Scripts.Towers.AttackCompo;
using Works.JES._01.Scripts.Towers.DrawCompo;

namespace Works.JES._01.Scripts.Towers
{
    public enum TargetingEnum
    {
        FIRST,LAST,STRONG
    }
    public class Tower : Entity
    {
       
        [SerializeField] private GameEventChannelSO upgradeChannel;
        [field : SerializeField] public Transform towerTopTrm { get;private set; }
        [SerializeField] private StateListSO stateList;
        
        public TowerInfoSO towerInfo;
        
        private StateMachine _stateMachine;

        public TargetingEnum targetingType = TargetingEnum.FIRST;
        
        protected TowerAttackCompo _towerAttackCompo;
        protected override void Awake()
        {
            towerInfo = towerInfo.Clone() as TowerInfoSO;
            
            base.Awake();
            
            _stateMachine = new StateMachine(this,stateList);
            _stateMachine.ChangeState("IDLE");
            
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        private void HandleLevelUp(LevelUpEvent obj)
        {
            GetCompo<TowerLevelCompo>().LevelUP();
            upgradeChannel.RaiseEvent(UpgradeEvents.OnUpgradeUIEvent.SetTower(this,towerInfo, GetCompo<EntityStat>()));
        }
        
        public virtual void SelectTower()
        {
            upgradeChannel.AddListener<LevelUpEvent>(HandleLevelUp);
            upgradeChannel.RaiseEvent(UpgradeEvents.OnUpgradeUIEvent.SetTower(this,towerInfo, GetCompo<EntityStat>()));
        }

        public virtual void UnSelectTower()
        {
            upgradeChannel.RemoveListener<LevelUpEvent>(HandleLevelUp);
        }
        public void ChangeState(string stateName)
        {
            _stateMachine.ChangeState(stateName);
        }
        #region Trash
        protected override void HandleHit()
        {
        }
        protected override void HandleDead()
        {
        }
        #endregion
    }
}