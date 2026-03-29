using Code.Entities;
using UnityEngine;

namespace Code.Combats
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage);
    }
}