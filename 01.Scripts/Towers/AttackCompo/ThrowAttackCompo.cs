using GondrLib.ObjectPool.Runtime;
using UnityEngine;
using Works.JES._01.Scripts.Combats;

namespace Works.JES._01.Scripts.Towers.AttackCompo
{
    public class ThrowAttackCompo : TowerAttackCompo
    {
        [SerializeField] private Transform towerBulletTrm;
        [SerializeField] private PoolingItemSO bulletSo;
        [SerializeField] private PoolManagerSO poolManager;
        public override void Attack()
        {
            _tower.SetTarget();
            if (_tower.TargetTrm == null) return;
            
            RotationTower();         
            
            Bullet bullet = poolManager.Pop(bulletSo) as Bullet;
            
            bullet.transform.position = towerBulletTrm.position;
            bullet.transform.rotation = towerBulletTrm.rotation;
            
            bullet.Initialize(_entityStat,_tower.TargetTrm.position,gameSettingSO.onEffect);
            
            base.Attack();
        }
    }
}