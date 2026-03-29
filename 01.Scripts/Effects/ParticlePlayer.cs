using DG.Tweening;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;

namespace Works.JES._01.Scripts.Effects
{
    public class ParticlePlayer : MonoBehaviour, IPoolable
    {
        [SerializeField] private ParticleSystem particleSystem;
        
        public void PlayEffect(Vector3 position, Quaternion rotation,float time=0)
        {
            transform.position = position;
            transform.rotation = rotation;

            if (time != 0)
            {
                DOVirtual.DelayedCall(time, () => StopEffect());
            }
            particleSystem.Play();
        }

        public void StopEffect()
        {
            particleSystem.Stop();
            _myPool.Push(this);
        }

        [field:SerializeField] public PoolingItemSO PoolingType { get; private set; }
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