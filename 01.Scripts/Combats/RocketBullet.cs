using System;
using Code.Entities;
using UnityEngine;

namespace Works.JES._01.Scripts.Combats
{
    public class RocketBullet : Bullet
    {
        public override void Initialize(EntityStat statCompo, Vector3 targetPosition, bool onEffect)
        {
            base.Initialize(statCompo, targetPosition, onEffect);
            Vector3 moveDir = targetPosition-transform.position;
            transform.forward = moveDir;
        }
    }
}