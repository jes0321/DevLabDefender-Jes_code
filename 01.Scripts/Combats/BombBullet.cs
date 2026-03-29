using UnityEngine;

namespace Works.JES._01.Scripts.Combats
{
    public class BombBullet : Bullet
    {
        [SerializeField] private float rotationForce = 5f;
        private void FixedUpdate()
        {
            _rbCompo.AddTorque(Random.Range(-1f, 1f) * rotationForce,
                Random.Range(-1f, 1f) * rotationForce,
                Random.Range(-1f, 1f) * rotationForce,
                ForceMode.Impulse);
        }
    }
}