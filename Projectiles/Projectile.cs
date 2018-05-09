using System;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public ProjectileStats Stats;
        protected Rigidbody RigidBody;

        public EventHandler ProjectileLaunched;
        public EventHandler ProjectileDestroyed;

        public virtual void LaunchProjectile(Transform from, Vector3 direction, float power)
        {
            OnProjectileLaunched(EventArgs.Empty);
        }

        protected virtual void OnProjectileLaunched(EventArgs args)
        {
            if (ProjectileLaunched != null)
                ProjectileLaunched(this, args);
        }

        protected virtual void OnProjectileDestroyed(EventArgs args)
        {
            if (ProjectileDestroyed != null)
                ProjectileDestroyed(this, args);
        }

        protected virtual void Destroy()
        {
            OnProjectileDestroyed(EventArgs.Empty);
            Destroy(this.gameObject);
        }

        protected virtual void OnCollisionEnter(Collision collisionInfo)
        {
            Destroy();
        }

        protected virtual void OnTriggerEnter(Collider col)
        {
            Destroy();
        }
    }
}