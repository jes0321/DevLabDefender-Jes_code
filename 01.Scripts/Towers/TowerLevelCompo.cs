using Code.Entities;
using UnityEngine;

namespace Works.JES._01.Scripts.Towers
{
    public class TowerLevelCompo : MonoBehaviour,IEntityComponent,IAfterInit
    {
        private Entity _entity;
        private TowerInfoSO _infoSO;
        private EntityStat _entityStat;
        [SerializeField] private MeshFilter changeMeshFilter;
        public void Initialize(Entity entity)
        {
            _entity = entity;
            var tower = entity as Tower;
            _infoSO = tower?.towerInfo;
        }
        public void AfterInit()
        {
            _entityStat = _entity.GetCompo<EntityStat>();
        }
        
        public void LevelUP()
        {
            _infoSO.Level++;
            var stats =_infoSO.upgradeSoList[_infoSO.level].upgradeStats;
            changeMeshFilter.mesh = _infoSO.upgradeSoList[_infoSO.level].changeMesh;
            foreach (var upgrade in stats)
            {
                _entityStat.SetBaseValue(upgrade.targetStat,upgrade.value);
            }
        }
    }
}