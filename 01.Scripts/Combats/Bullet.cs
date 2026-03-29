using Code.Entities;
using Devlab.Dependencies;
using DG.Tweening;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;
using Works.JES._01.Scripts.Core.StatSystem;
using Works.JES._01.Scripts.Effects;
using Random = UnityEngine.Random;

namespace Works.JES._01.Scripts.Combats
{
    public class Bullet : MonoBehaviour,IPoolable
    {
        [SerializeField] private StatSo damageStat;
        [SerializeField] private StatSo attackRadiusStat;
        [SerializeField] private ParticlePlayer bombEffect;

        [SerializeField] private PoolingItemSO effectPoolingSo;
        [SerializeField] private PoolManagerSO poolManager;
        protected Rigidbody _rbCompo;
        private OverlapDamageCaster _damageCaster;

        private float _damage;
        private float _radius;
        public virtual void Initialize(EntityStat statCompo,Vector3 targetPosition,bool onEffect)
        {
            _rbCompo = GetComponent<Rigidbody>();

            _damage = statCompo.GetStat(damageStat).Value;
            _radius = statCompo.GetStat(attackRadiusStat).Value;
            
            _damageCaster = GetComponentInChildren<OverlapDamageCaster>();
            _damageCaster.Initialize();
            _damageCaster.SetRadius(_radius);

            transform.DOMove(targetPosition, 0.1f).OnComplete(() =>
            {
                if (onEffect)
                {
                    ParticlePlayer effect = poolManager.Pop(effectPoolingSo) as ParticlePlayer;
                    effect.PlayEffect(transform.position, Quaternion.identity,0.5f);
                }
                _damageCaster.CastDamage(_damage); 
                _myPool.Push(this);
            });
        }

        [field:SerializeField]public PoolingItemSO PoolingType { get; private set; }
        public GameObject GameObject => gameObject;
        private Pool _myPool;
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
        }
    }
}