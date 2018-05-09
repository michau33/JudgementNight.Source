using Assets.Scripts.Interfaces;
using Assets.Scripts.Projectiles;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Characters.Base
{
    public abstract class Character : MonoBehaviour, IWoundable
    {
        protected BaseCharacterStats Stats;
        public IController Controller { get; protected set; }
        public float CurrentHealth { get; private set; }


        void Start()
        {
            CurrentHealth = Stats.Health;
        }

        public virtual void IncreaseHealth(float health)
        {
            CurrentHealth += health;

            if (CurrentHealth >= Stats.Health)
            {
                CurrentHealth = Stats.Health;
            }
        }

        public virtual void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Destroy(this.gameObject);
        }
  
        void OnTriggerEnter(Collider col)
        {
            var projectileComponent = col.GetComponent(typeof(Projectile));

            if (projectileComponent != null)
            {
                var damage = ((Projectile) projectileComponent).Stats.Damage;
                TakeDamage(damage);
            }
        }
    }
}