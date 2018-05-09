using Assets.Scripts.Characters;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Bullet : Projectile
    {
        void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
        }
        /// <summary>
        /// Handles constant speed of projectile
        /// </summary>
        void FixedUpdate ()
        {
            RigidBody.velocity = Vector3.ClampMagnitude (RigidBody.velocity, Stats.MaxSpeed);
        }

        public override void LaunchProjectile(Transform @from, Vector3 direction, float power)
        {
            base.LaunchProjectile(@from, direction, power);
            RigidBody.AddForce (direction * power * Time.deltaTime, ForceMode.Impulse);
        }

        protected override void OnCollisionEnter(Collision collisionInfo)
        {
            base.OnCollisionEnter(collisionInfo);
        }

        protected override void OnTriggerEnter(Collider col)
        {
            base.OnTriggerEnter(col);
            LayerMask colLayer = col.gameObject.layer;

            if (colLayer.Contains(Layers.Enemy))
            {
                var character = col.GetComponent(typeof(IWoundable));

                if (character != null)
                {
                    ((IWoundable) character).TakeDamage(Stats.Damage);
                }
            }
        }
    }
}